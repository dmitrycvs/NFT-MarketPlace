using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.NftItem;

namespace NFT.UseCases.NftItem.Commands;

public class EditNftCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Hash { get; set; }
        public Guid UserId { get; set; }
        public decimal Price { get; set; }
        public bool IsListed { get; set; } = false;

        public EditNftCommand(NftItemDto nftItemDto)
        {
            Id = nftItemDto.Id;
            Hash = nftItemDto.Hash;
            UserId = nftItemDto.UserId;
            Price = nftItemDto.Price;
            IsListed = nftItemDto.IsListed;
        }
    }

    public class EditNftCommandHandler : IRequestHandler<EditNftCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public EditNftCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(EditNftCommand request, CancellationToken cancellationToken)
        {
            var nftToEdit = await _appDbContext.NftItems
                .Where(n => n.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (nftToEdit == null)
            {
                throw new InvalidOperationException("Nft not found.");
            }

            if (!string.IsNullOrEmpty(request.Hash))
            {
                nftToEdit.Hash = request.Hash;
            }
            
            if (request.UserId != Guid.Empty)
            {
                nftToEdit.UserId = request.UserId;
            }
            
            if (request.Price != 0)
            {
                nftToEdit.Price = request.Price;
            }

            nftToEdit.IsListed = request.IsListed;

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class EditNftCommandValidator : AbstractValidator<EditNftCommand>
    {
        public EditNftCommandValidator(IServiceProvider services)
        {
            RuleFor(n => n.Id).NotEmpty().WithMessage("Nft ID must not be empty");

            RuleFor(n => n.UserId)
                .NotEmpty().WithMessage("User Id must not be empty");
            RuleFor(n => n.Price).NotEmpty().WithMessage("Price must not be empty");
            RuleFor(n => n.Hash).NotEmpty().WithMessage("Hash must not be empty");

            RuleFor(n => n).Custom((command, context) =>
            {
                using var scope = services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var nftExists = dbContext.NftItems.Any(n => n.Id == command.Id);
                if (!nftExists)
                {
                    context.AddFailure("The nft does not exist.");
                }
            });
        }
    }