namespace NFT.Core.Entities;

public class Nft
{
    public Nft()
    {
        User = new User();
    }
    public Guid Id { get; set; }
    public string Hash { get; set; }
    public Guid UserId { get; set; }
    public string Price { get; set; }
    public User User { get; set; }
}