namespace NFT.Shared.DataTransferObjects.NFT;

public class NftItemDto
{
    public Guid Id { get; set; }
    public string Hash { get; set; }
    public Guid UserId { get; set; }
    public decimal Price { get; set; }
    public bool IsListed { get; set; } = false;
}