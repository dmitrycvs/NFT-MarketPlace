using NFT.Core.Entities;
using NFT.Shared.DataTransferObjects.NFT;

namespace NFT.Shared.DataTransferObjects.Inventories;

public class InventoryDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    //public List<NftItemDto>? NftItems { get; set; } = new List<NftItemDto>();

}