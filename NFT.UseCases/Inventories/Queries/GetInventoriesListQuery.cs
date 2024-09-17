using MediatR;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.Inventories;
using NFT.Shared.DataTransferObjects.Pagination;


namespace NFT.UseCases.Inventories.Queries
{
    public class GetInventoryListQuery : IRequest<PaginationResult<InventoryDto>>
    {
        public GetInventoryListQuery(PaginationParameter paginationParameter)
        {
            PaginationParameter = paginationParameter;
        }
        public PaginationParameter PaginationParameter { get; set; }
    }

    public class GetInventoryListQueryHandler : IRequestHandler<GetInventoryListQuery, PaginationResult<InventoryDto>>
    {
        private readonly IPaginationService _paginationService;

        private readonly AppDbContext _appDbContext;

        public GetInventoryListQueryHandler(IPaginationService paginationService, AppDbContext appDbContext)
        {
            _paginationService = paginationService;
            _appDbContext = appDbContext;
        }
        public async Task<PaginationResult<InventoryDto>> Handle(GetInventoryListQuery request, CancellationToken cancellationToken)
        {
            var query = request.PaginationParameter;

            var inventories = _appDbContext.Inventories.AsQueryable();

            return await _paginationService.PaginateAsync(inventories, query, InventoryMapping.ToInventoryDto);
         
        }
    }
}