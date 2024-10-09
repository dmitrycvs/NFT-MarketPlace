namespace NFT.Core.Entities;

public class NftItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Hash { get; set; }
    public Guid UserId { get; set; }
    public decimal Price { get; set; }
    public User User { get; set; }
    public bool IsListed { get; set; } = false;
    public decimal? AvgDealPrice { get; set; } //take from HistoryLog all DealPrices by NftItemId
                                              //and make arithmetical avg price
    public Guid CollectionId { get; set; }
    public Collection Collection { get; set; }
   
}