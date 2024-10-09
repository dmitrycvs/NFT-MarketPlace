using System.Linq.Expressions;
using NFT.Core.Entities;
using NFT.Shared.DataTransferObjects.Inventories;
using NFT.Shared.DataTransferObjects.NftItem;

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
               /* NftItems = inventory.NftItems != null
                        ? inventory.NftItems.Select(nftItem => new NftItemDto
                        {
                            Id = nftItem.Id,
                            Name = nftItem.Name,
                            Hash = nftItem.Hash,
                            Price = nftItem.Price,
                            IsListed = nftItem.IsListed,
                            CollectionId = nftItem.CollectionId
                        }).ToList()
                        : new List<NftItemDto>() */
            };
        }
    }
}