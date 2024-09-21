using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFT.Infrastructure;

namespace NFT.UseCases.Collections.Commands;

public class DeleteCollectionCommand : IRequest<Unit>
{
    public Guid Id { get; set; }   
}

public class DeleteCollectionCommandHandler : IRequestHandler<DeleteCollectionCommand, Unit>
{
    private readonly AppDbContext _appDbContext;

    public DeleteCollectionCommandHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Unit> Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
    {
        var CollectionToDelete = await _appDbContext.Collections
            .Where(i => i.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (CollectionToDelete == null)
        {
            throw new InvalidOperationException("Collection not found");
        }
        
        _appDbContext.Collections.Remove(CollectionToDelete);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}

public class DeleteCollectionCommandValidator : AbstractValidator<DeleteCollectionCommand>
{
    public DeleteCollectionCommandValidator(IServiceProvider services)
    {
        RuleFor(i => i.Id).NotEmpty().WithMessage("Id cannot be empty");

        RuleFor(i => i).Custom((command, context) =>
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var CollectionExists = dbContext.Collections.Any(i => i.Id == command.Id);
            if (!CollectionExists)
            {
                context.AddFailure("Invalid Collection ID - the Collection does not exist");
            }
        });
    }
}