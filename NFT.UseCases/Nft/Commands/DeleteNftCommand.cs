using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFT.Infrastructure;

namespace NFT.UseCases.Nft.Commands;

public class DeleteNftCommand : IRequest<Unit>
{
    public Guid Id { get; set; }   
}

public class DeleteNftCommandHandler : IRequestHandler<DeleteNftCommand, Unit>
{
    private readonly AppDbContext _appDbContext;

    public DeleteNftCommandHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Unit> Handle(DeleteNftCommand request, CancellationToken cancellationToken)
    {
        var NftToDelete = await _appDbContext.Nfts
            .Where(n => n.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (NftToDelete == null)
        {
            throw new InvalidOperationException("Nft not found");
        }
        
        _appDbContext.Nfts.Remove(NftToDelete);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}

public class DeleteNftCommandValidator : AbstractValidator<DeleteNftCommand>
{
    public DeleteNftCommandValidator(IServiceProvider services)
    {
        RuleFor(i => i.Id).NotEmpty().WithMessage("Id cannot be empty");

        RuleFor(i => i).Custom((command, context) =>
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var NftExists = dbContext.Nfts.Any(n => n.Id == command.Id);
            if (!NftExists)
            {
                context.AddFailure("Invalid Nft ID - the Nft does not exist");
            }
        });
    }
}