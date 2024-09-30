using MediatR;
using Microsoft.EntityFrameworkCore;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.NftItem;

namespace NFT.UseCases.NftItem.Queries;

public class GetAllNftByUserIdQuery : IRequest<List<NftItemDto>>
{
    public Guid UserId { get; set; }
    
    public GetAllNftByUserIdQuery(Guid userId)
    {
        UserId = userId;
    }
}

public class GetAllNftByUserIdQueryHandler : IRequestHandler<GetAllNftByUserIdQuery, List<NftItemDto>>
{
    private readonly AppDbContext _appDbContext;

    public GetAllNftByUserIdQueryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<NftItemDto>> Handle(GetAllNftByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _appDbContext.NftItems.Where(Nft => Nft.UserId == request.UserId).Select(Nft => new NftItemDto
        {
            Id = Nft.Id,
            Name = Nft.Name,
            UserId = Nft.UserId,
            Hash = Nft.Hash,
            Price = Nft.Price,
            IsListed = Nft.IsListed,
            CollectionId = Nft.CollectionId,
        }).ToListAsync(cancellationToken);
    }
}