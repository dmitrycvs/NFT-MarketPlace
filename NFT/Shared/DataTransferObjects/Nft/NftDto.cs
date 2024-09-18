namespace NFT.Shared.DataTransferObjects.NFT;

public class NftDto
{
    public Guid Id { get; set; }
    public string Hash { get; set; }
    public Guid UserId { get; set; }
    public string Price { get; set; }
}