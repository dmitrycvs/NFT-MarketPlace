using MediatR;
using NFT.Shared.DataTransferObjects.NFT;
using NFT.Shared.DataTransferObjects.Pagination;

namespace NFT.Client.Services.NftServices;

public interface INftService
{
    Task<Guid> CreateNft(NftDto request);
    Task<Unit> DeleteNft(Guid id);
    Task<List<NftDto>> GetNotPaginatedNfts();
    Task<NftDto> GetNftById(Guid id);
    Task<PaginationResult<NftDto>> GetNfts(NftDto queryModel);
    Task<Unit> EditNft(NftDto request);
}