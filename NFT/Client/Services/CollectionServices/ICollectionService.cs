using MediatR;
using NFT.Shared.DataTransferObjects.Collections;
using NFT.Shared.DataTransferObjects.Pagination;

namespace NFT.Client.Services.CollectionServices;

public interface ICollectionService
{
    Task<Guid> CreateCollection(CollectionDto request);
    Task<Unit> DeleteCollection(Guid id);
    Task<Unit> EditCollection(CollectionDto request);
    Task<List<CollectionDto>> GetNotPaginatedCollections();
    Task<CollectionDto> GetCollectionById(Guid id);
    Task<PaginationResult<CollectionDto>> GetCollections(CollectionDto queryModel);
}