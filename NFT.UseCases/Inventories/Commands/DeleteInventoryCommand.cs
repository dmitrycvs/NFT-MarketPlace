using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFT.Infrastructure;

namespace NFT.UseCases.Inventories.Commands;

public class DeleteInventoryCommand : IRequest<Unit>
{
    public Guid Id { get; set; }   
}

public class DeleteInventoryCommandHandler : IRequestHandler<DeleteInventoryCommand, Unit>
{
    private readonly AppDbContext _appDbContext;

    public DeleteInventoryCommandHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Unit> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventoryToDelete = await _appDbContext.Inventories
            .Where(i => i.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (inventoryToDelete == null)
        {
            throw new InvalidOperationException("Inventory not found");
        }
        
        _appDbContext.Inventories.Remove(inventoryToDelete);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}

public class DeleteInventoryCommandValidator : AbstractValidator<DeleteInventoryCommand>
{
    public DeleteInventoryCommandValidator(IServiceProvider services)
    {
        RuleFor(i => i.Id).NotEmpty().WithMessage("Id cannot be empty");

        RuleFor(i => i).Custom((command, context) =>
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var inventoryExists = dbContext.Inventories.Any(i => i.Id == command.Id);
            if (!inventoryExists)
            {
                context.AddFailure("Invalid inventory ID - the inventory does not exist");
            }
        });
    }
}