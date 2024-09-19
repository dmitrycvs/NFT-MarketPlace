using System.Linq.Expressions;
using NFT.Shared.DataTransferObjects.NFT;
using NFT.Core.Entities;

namespace NFT.UseCases.Nft;

public class NftMapping
{
    public static Expression<Func<Core.Entities.NftItem, NftItemDto>> ToNftDto
    {
        get
        {
            return nft => new NftItemDto
            {
                Id = nft.Id,
                Hash = nft.Hash,
                Price = nft.Price,
                UserId = nft.UserId,
                IsListed = nft.IsListed
            };
        }
    }
        
}