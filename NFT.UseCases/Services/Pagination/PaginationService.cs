using Microsoft.EntityFrameworkCore;
using NFT.Shared.DataTransferObjects.Pagination;
using System.Linq.Expressions;

namespace NFT.UseCases.Services.Pagination
{
    public class PaginationService : IPaginationService
    {
        public async Task<PaginationResult<DestinationT>> PaginateAsync<TSource, DestinationT>(IQueryable<TSource> source, PaginationParameter request, Expression<Func<TSource, DestinationT>> MappingRule)
        {
            var response = new PaginationResult<DestinationT>();

            response.TotalItems = source.Count();
            var count = source.Count();
            var items = await source
                                .Skip((request.PageNumber - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .Select(MappingRule)
                                .ToListAsync();

            response.Items = items;
            return response;
        }

        public Task<PaginationResult<DestinationT>> PaginateEnumerableAsync<TSource, DestinationT>(IEnumerable<TSource> source, PaginationParameter request)
        {
            throw new NotImplementedException();
        }
    }
}