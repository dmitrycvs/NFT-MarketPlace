using System.Linq.Expressions;

namespace NFT.Shared.DataTransferObjects.Pagination
{
    public interface IPaginationService
    {
        Task<PaginationResult<DestinationT>> PaginateEnumerableAsync<TSource, DestinationT>(IEnumerable<TSource> source, PaginationParameter request);

        Task<PaginationResult<DestinationT>> PaginateAsync<TSource, DestinationT>(IQueryable<TSource> source, PaginationParameter request, Expression<Func<TSource, DestinationT>> MappingRule);
    }
}
