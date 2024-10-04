using MediatR;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.Collections;
using NFT.Shared.DataTransferObjects.Pagination;

namespace NFT.UseCases.Collections.Queries;

public class GetCollectionListQuery : IRequest<PaginationResult<CollectionDto>>
{
    public GetCollectionListQuery(PaginationParameter paginationParameter)
    {
        PaginationParameter = paginationParameter;
    }
    public PaginationParameter PaginationParameter { get; set; }
}

public class GetCollectionListQueryHandler : IRequestHandler<GetCollectionListQuery, PaginationResult<CollectionDto>>
{
    private readonly IPaginationService _paginationService;

    private readonly AppDbContext _appDbContext;

    public GetCollectionListQueryHandler(IPaginationService paginationService, AppDbContext appDbContext)
    {
        _paginationService = paginationService;
        _appDbContext = appDbContext;
    }
    public async Task<PaginationResult<CollectionDto>> Handle(GetCollectionListQuery request, CancellationToken cancellationToken)
    {
        var query = request.PaginationParameter;

        var Collections = _appDbContext.Collections.AsQueryable();

        return await _paginationService.PaginateAsync(Collections, query, CollectionMapping.ToCollectionDto);
         
    }
}