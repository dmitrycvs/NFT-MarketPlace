namespace NFT.Shared.DataTransferObjects.Collections;

public class CollectionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public decimal FloorPrice { get; set; }
    public decimal Volume { get; set; } //oborot
    public int Supply {  get; set; } //total number of nfts in deal
    public int NumberOfSale { get; set; }
    public decimal MarketCapital { get; set; }
    
    public string SocialLink { get; set; } = string.Empty;
}