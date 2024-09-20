using NFT.Core.Entities;

namespace NFT.Shared.DataTransferObjects.HistoryLogs;

public class HistoryLogDto
{

    public Guid Id { get; set; }
    public Guid SellerId { get; set; }
    public Guid BuyerId { get; set; }
    public DateTime DateTime { get; set; }
    public decimal DealPrice { get; set; }
    public Guid NftItemId { get; set; }

}