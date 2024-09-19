using MediatR;
using NFT.Shared.DataTransferObjects.NFT;
using NFT.Shared.DataTransferObjects.Pagination;

namespace NFT.Client.Services.NftServices;

public interface INftService
{
    Task<Guid> CreateNft(NftItemDto request);
    Task<Unit> DeleteNft(Guid id);
    Task<List<NftItemDto>> GetNotPaginatedNfts();
    Task<NftItemDto> GetNftById(Guid id);
    Task<PaginationResult<NftItemDto>> GetNfts(NftItemDto queryModel);
    Task<Unit> EditNft(NftItemDto request);
}