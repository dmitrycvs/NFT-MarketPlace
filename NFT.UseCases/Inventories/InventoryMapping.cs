using System.Linq.Expressions;
using NFT.Core.Entities;
using NFT.Shared.DataTransferObjects.Inventories;

namespace NFT.UseCases.Inventories;

public class InventoryMapping
{
    public static Expression<Func<Inventory, InventoryDto>> ToInventoryDto
    {
        get
        {
            return inventory => new InventoryDto
            {
                Id = inventory.Id,
                UserId = inventory.UserId,
                NftItemId = inventory.NftItemId,
            };
        }
    }
}