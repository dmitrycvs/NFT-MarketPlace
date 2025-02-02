namespace NFT.Shared.DataTransferObjects.NftItem;

public class NftItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Hash { get; set; }
    public string? ImageBase64 { get; set; }

    public Guid UserId { get; set; }
    public decimal Price { get; set; }
    public bool IsListed { get; set; } = false;
    public Guid CollectionId { get; set; }

}