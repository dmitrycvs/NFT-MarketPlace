using FluentValidation;
using MediatR;
using NFT.Core.Entities;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.Inventories;

namespace NFT.UseCases.Inventories.Commands;

public class CreateInventoryCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public CreateInventoryCommand(InventoryDto inventoryDto)
    {
        UserId = inventoryDto.UserId;
        User = inventoryDto.User;
    }
}

public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, Guid>
{
    private readonly AppDbContext _appDbContext;

    public CreateInventoryCommandHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Guid> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventoryToAdd = new Inventory
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            User = request.User
        };
        _appDbContext.Inventories.Add(inventoryToAdd);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return inventoryToAdd.Id;
    }
}

public class CreateInventoryCommandValidator : AbstractValidator<CreateInventoryCommand>
{
    public CreateInventoryCommandValidator()
    {
        RuleFor(i => i.UserId).NotEmpty().WithMessage("UserId cannot be empty");
        RuleFor(i => i.User).NotEmpty().WithMessage("User cannot be empty");
    }
}