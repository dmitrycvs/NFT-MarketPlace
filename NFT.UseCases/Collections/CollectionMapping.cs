using System.Linq.Expressions;
using NFT.Core.Entities;
using NFT.Shared.DataTransferObjects.Collections;

namespace NFT.UseCases.Collections;

public class CollectionMapping
{
    public static Expression<Func<Collection, CollectionDto>> ToCollectionDto
    {
        get
        {
            return collection => new CollectionDto
            {
                Id = collection.Id,
                Name = collection.Name,
                DateTime = collection.DateTime,
                FloorPrice = collection.FloorPrice,
                Volume = collection.Volume,
                Supply = collection.Supply,
                NumberOfSale = collection.NumberOfSale,
                MarketCapital = collection.MarketCapital,
                SocialLink = collection.SocialLink
            };
        }
    }
}