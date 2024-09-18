using MediatR;
using Microsoft.EntityFrameworkCore;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.NFT;

namespace NFT.UseCases.Nft.Queries;

public class GetNotPaginatedNftsListQuery : IRequest<List<NftDto>>
{
    public GetNotPaginatedNftsListQuery()
    {
    }
}

public class GetNotPaginatedNftsQueryHandler : IRequestHandler<GetNotPaginatedNftsListQuery, List<NftDto>>
{
    private readonly AppDbContext _appDbContext;

    public GetNotPaginatedNftsQueryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<NftDto>> Handle(GetNotPaginatedNftsListQuery request,
        CancellationToken cancellationToken)
    {
        return await _appDbContext.Nfts.Select(Nft => new NftDto
        {
            Id = Nft.Id,
            UserId = Nft.UserId,
            Hash = Nft.Hash,
            Price = Nft.Price
        }).ToListAsync(cancellationToken);
    }
}