/*using MediatR;
using NFT.Shared.DataTransferObjects.Inventories;
using NFT.Shared.DataTransferObjects.Pagination;

namespace NFT.Client.Services.InventoryServices;

public interface IInventoryService
{
    Task<Guid> CreateInventory(InventoryDto request);
    Task<Unit> DeleteInventory(Guid id);
    Task<List<InventoryDto>> GetNotPaginatedInventories();
    Task<InventoryDto> GetInventoryById(Guid id);
    Task<PaginationResult<InventoryDto>> GetInventories(InventoryDto queryModel);
}*/
//Temporarly disabled commands and queries regarding Inventory