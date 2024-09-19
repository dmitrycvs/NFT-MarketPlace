namespace NFT.Core.Entities;

public class NftItem
{
    public NftItem()
    {
        User = new User();
    }
    public Guid Id { get; set; }
    public string Hash { get; set; }
    public Guid UserId { get; set; }
    public decimal Price { get; set; }
    public User User { get; set; }
    public bool IsListed { get; set; } = false;
}