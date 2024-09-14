using NFT.Core.Entities;

namespace NFT.Shared.DataTransferObjects.Inventories;

public class InventoryDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}