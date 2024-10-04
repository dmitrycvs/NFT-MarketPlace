using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.Collections;

namespace NFT.UseCases.Collections.Commands;

public class EditCollectionCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public decimal FloorPrice { get; set; }
        public decimal Volume { get; set; } //oborot
        public int Supply {  get; set; } //total number of nfts in deal
        public int NumberOfSale { get; set; }
        public decimal MarketCapital { get; set; }
        public string SocialLink { get; set; } = string.Empty;

        public EditCollectionCommand(CollectionDto CollectionDto)
        {
            Id = CollectionDto.Id;
            Name = CollectionDto.Name;
            DateTime = CollectionDto.DateTime;
            FloorPrice = CollectionDto.FloorPrice;
            Volume = CollectionDto.Volume;
            Supply = CollectionDto.Supply;
            NumberOfSale = CollectionDto.NumberOfSale;
            MarketCapital = CollectionDto.MarketCapital;
            SocialLink = CollectionDto.SocialLink;
        }
    }

    public class EditCollectionCommandHandler : IRequestHandler<EditCollectionCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public EditCollectionCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(EditCollectionCommand request, CancellationToken cancellationToken)
        {
            var CollectionToEdit = await _appDbContext.Collections
                .Where(n => n.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (CollectionToEdit == null)
            {
                throw new InvalidOperationException("Collection not found.");
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                CollectionToEdit.Name = request.Name;
            }

            if (request.DateTime != null)
            {
                CollectionToEdit.DateTime = request.DateTime;
            }

            if (request.FloorPrice != null)
            {
                CollectionToEdit.FloorPrice = request.FloorPrice;
            }

            if (request.Volume != null)
            {
                CollectionToEdit.Volume = request.Volume;
            }

            if (request.Supply != null)
            {
                CollectionToEdit.Supply = request.Supply;
            }

            if (request.NumberOfSale != null)
            {
                CollectionToEdit.NumberOfSale = request.NumberOfSale;
            }

            if (request.MarketCapital != null)
            {
                CollectionToEdit.MarketCapital = request.MarketCapital;
            }

            if (!string.IsNullOrEmpty(request.SocialLink))
            {
                CollectionToEdit.SocialLink = request.SocialLink;
            }

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class EditCollectionCommandValidator : AbstractValidator<EditCollectionCommand>
    {
        public EditCollectionCommandValidator(IServiceProvider services)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.DateTime).NotEmpty().WithMessage("DateTime is required");
            RuleFor(x => x.FloorPrice).NotEmpty().WithMessage("FloorPrice is required");
            RuleFor(x => x.Volume).NotEmpty().WithMessage("Volume is required");
            RuleFor(x => x.Supply).NotEmpty().WithMessage("Supply is required");
            RuleFor(x => x.NumberOfSale).NotEmpty().WithMessage("NumberOfSale is required");
            RuleFor(x => x.MarketCapital).NotEmpty().WithMessage("MarketCapital is required");
            RuleFor(x => x.SocialLink).NotEmpty().WithMessage("SocialLink is required");
            RuleFor(n => n).Custom((command, context) =>
            {
                using var scope = services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var CollectionExists = dbContext.Collections.Any(n => n.Id == command.Id);
                if (!CollectionExists)
                {
                    context.AddFailure("The Collection does not exist.");
                }
            });
        }
    }