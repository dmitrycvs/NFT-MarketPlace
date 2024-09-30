using MediatR;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.NftItem;
using NFT.Shared.DataTransferObjects.Pagination;

namespace NFT.UseCases.Nft.Queries;

public class GetNftsListQuery : IRequest<PaginationResult<NftItemDto>>
{
    public GetNftsListQuery(PaginationParameter paginationParameter)
    {
        PaginationParameter = paginationParameter;
    }
    public PaginationParameter PaginationParameter { get; set; }
}

public class GetNftListQueryHandler : IRequestHandler<GetNftsListQuery, PaginationResult<NftItemDto>>
{
    private readonly IPaginationService _paginationService;

    private readonly AppDbContext _appDbContext;

    public GetNftListQueryHandler(IPaginationService paginationService, AppDbContext appDbContext)
    {
        _paginationService = paginationService;
        _appDbContext = appDbContext;
    }
    public async Task<PaginationResult<NftItemDto>> Handle(GetNftsListQuery request, CancellationToken cancellationToken)
    {
        var query = request.PaginationParameter;

        var nfts = _appDbContext.NftItems.AsQueryable();

        return await _paginationService.PaginateAsync(nfts, query, NftMapping.ToNftDto);
         
    }
}