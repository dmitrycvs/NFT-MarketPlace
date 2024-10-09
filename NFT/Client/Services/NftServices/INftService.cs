using MediatR;
using NFT.Shared.DataTransferObjects.HistoryLogs;
using NFT.Shared.DataTransferObjects.NftItem;
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
    Task<Guid> SellNft(HistoryLogDto request); 
    Task<List<NftItemDto>> GetAllNftByUserId(Guid userId);
}