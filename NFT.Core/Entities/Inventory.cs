namespace NFT.Core.Entities;

public class Inventory
{
    public Inventory()
    {
        User = new User(); 
    }
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid NftItemId { get; set; }
    public NftItem Nft { get; set; }
}