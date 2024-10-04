using MediatR;
using Microsoft.EntityFrameworkCore;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.Collections;

namespace NFT.UseCases.Collections.Queries;

public class GetNotPaginatedCollectionsListQuery : IRequest<List<CollectionDto>>
{
    public GetNotPaginatedCollectionsListQuery()
    {
    }
}

public class GetNotPaginatedCollectionsQueryHandler : IRequestHandler<GetNotPaginatedCollectionsListQuery, List<CollectionDto>>
{
    private readonly AppDbContext _appDbContext;

    public GetNotPaginatedCollectionsQueryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<CollectionDto>> Handle(GetNotPaginatedCollectionsListQuery request,
        CancellationToken cancellationToken)
    {
        return await _appDbContext.Collections.Select(Collection => new CollectionDto
        {
            Id = Collection.Id, 
            Name = Collection.Name, 
            DateTime = Collection.DateTime, 
            Supply = Collection.Supply, 
            FloorPrice = Collection.FloorPrice, 
            MarketCapital = Collection.MarketCapital, 
            NumberOfSale = Collection.NumberOfSale, 
            SocialLink = Collection.SocialLink, 
            Volume = Collection.Volume
        }).ToListAsync(cancellationToken);
    }
}