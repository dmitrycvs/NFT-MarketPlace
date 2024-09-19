using MediatR;
using Microsoft.EntityFrameworkCore;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.NFT;

namespace NFT.UseCases.Nft.Queries;

public class GetNotPaginatedNftsListQuery : IRequest<List<NftItemDto>>
{
    public GetNotPaginatedNftsListQuery()
    {
    }
}

public class GetNotPaginatedNftsQueryHandler : IRequestHandler<GetNotPaginatedNftsListQuery, List<NftItemDto>>
{
    private readonly AppDbContext _appDbContext;

    public GetNotPaginatedNftsQueryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<NftItemDto>> Handle(GetNotPaginatedNftsListQuery request,
        CancellationToken cancellationToken)
    {
        return await _appDbContext.NftItems.Select(Nft => new NftItemDto
        {
            Id = Nft.Id,
            UserId = Nft.UserId,
            Hash = Nft.Hash,
            Price = Nft.Price,
            IsListed = Nft.IsListed,
            CollectionId = Nft.CollectionId,
        }).ToListAsync(cancellationToken);
    }
}