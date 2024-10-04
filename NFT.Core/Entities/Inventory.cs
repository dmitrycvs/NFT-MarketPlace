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
    //public List<NftItem>? NftItems { get; set; } = new List<NftItem>(); //tak kak odin inventory, soderjit mnogo nft's

}