namespace NFT.Shared.DataTransferObjects.NftItem
{
    public class SellNftDto
    {
        public Guid SellerId { get; set; }
        public Guid BuyerId { get; set; }
        public Guid NftItemId { get; set; }
        public decimal DealPrice { get; set; }
    }

}
