using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.Inventories;

namespace NFT.UseCases.Inventories.Queries;

public class GetInventoryByIdQuery : IRequest<InventoryDto>
{
    public Guid Id { get; set; }

    public GetInventoryByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetInventoryByIdQueryHandler : IRequestHandler<GetInventoryByIdQuery, InventoryDto>
{
    private readonly AppDbContext _appDbContext;

    public GetInventoryByIdQueryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<InventoryDto> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return await _appDbContext.Inventories
            .Where(i => i.Id == request.Id)
            .Select(inventory => new InventoryDto {Id = inventory.Id, UserId = inventory.UserId }) //TO DO
            .FirstOrDefaultAsync(cancellationToken);
#pragma warning restore CS8603 // Possible null reference return.
    }
}

public class GetInventoryByIdQueryValidator : AbstractValidator<GetInventoryByIdQuery>
{
    public GetInventoryByIdQueryValidator(IServiceProvider services)
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");

        RuleFor(x => x).Custom((query, context) =>
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var inventoryExist = dbContext.Inventories.Any(i => i.Id == query.Id);
            if (!inventoryExist)
            {
                context.AddFailure("Inventory not found");
            }
        });
    }
}