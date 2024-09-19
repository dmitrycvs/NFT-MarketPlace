
namespace NFT.Core.Entities;

public class HistoryLog
{
   
    public Guid Id { get; set; }
    public Guid? UserSellerId { get; set; }
    public User Seller { get; set; }
    public Guid? UserBuyerId { get; set; }
    public User Buyer { get; set; }

    public DateTime DateTime { get; set; }
    public decimal DealPrice { get; set; }
    public Guid NftItemId { get; set; }
    public NftItem Nft { get; set; }

}