using MediatR;
using Microsoft.EntityFrameworkCore;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.Inventories;

namespace NFT.UseCases.Inventories.Queries;

public class GetNotPaginatedInventoriesListQuery : IRequest<List<InventoryDto>>
{
    public GetNotPaginatedInventoriesListQuery()
    {
    }
}

public class GetNotPaginatedInventoriesQueryHandler : IRequestHandler<GetNotPaginatedInventoriesListQuery, List<InventoryDto>>
{
    private readonly AppDbContext _appDbContext;

    public GetNotPaginatedInventoriesQueryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<InventoryDto>> Handle(GetNotPaginatedInventoriesListQuery request,
        CancellationToken cancellationToken)
    {
        return await _appDbContext.Inventories.Select(inventory => new InventoryDto
        {
            Id = inventory.Id,
            UserId = inventory.UserId,
            User = inventory.User
        }).ToListAsync(cancellationToken);
    }
}