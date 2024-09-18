using MediatR;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.NFT;
using NFT.Shared.DataTransferObjects.Pagination;

namespace NFT.UseCases.Nft.Queries;

public class GetNftsListQuery : IRequest<PaginationResult<NftDto>>
{
    public GetNftsListQuery(PaginationParameter paginationParameter)
    {
        PaginationParameter = paginationParameter;
    }
    public PaginationParameter PaginationParameter { get; set; }
}

public class GetNftListQueryHandler : IRequestHandler<GetNftsListQuery, PaginationResult<NftDto>>
{
    private readonly IPaginationService _paginationService;

    private readonly AppDbContext _appDbContext;

    public GetNftListQueryHandler(IPaginationService paginationService, AppDbContext appDbContext)
    {
        _paginationService = paginationService;
        _appDbContext = appDbContext;
    }
    public async Task<PaginationResult<NftDto>> Handle(GetNftsListQuery request, CancellationToken cancellationToken)
    {
        var query = request.PaginationParameter;

        var nfts = _appDbContext.Nfts.AsQueryable();

        return await _paginationService.PaginateAsync(nfts, query, NftMapping.ToNftDto);
         
    }
}