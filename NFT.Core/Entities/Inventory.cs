namespace NFT.Core.Entities;

public class Inventory
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}