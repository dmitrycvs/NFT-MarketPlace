using MediatR;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.Pagination;
using NFT.Shared.DataTransferObjects.Roles;


namespace NFT.UseCases.Roles.Queries
{
    public class GetRoleListQuery : IRequest<PaginationResult<RoleDto>>
    {
        public GetRoleListQuery(PaginationParameter paginationParameter)
        {
            PaginationParameter = paginationParameter;
        }
        public PaginationParameter PaginationParameter { get; set; }
    }

    public class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, PaginationResult<RoleDto>>
    {
        private readonly IPaginationService _paginationService;

        private readonly AppDbContext _appDbContext;

        public GetRoleListQueryHandler(IPaginationService paginationService, AppDbContext appDbContext)
        {
            _paginationService = paginationService;
            _appDbContext = appDbContext;
        }
        public async Task<PaginationResult<RoleDto>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var query = request.PaginationParameter;

            var footballFields = _appDbContext.Roles.AsQueryable();

            return await _paginationService.PaginateAsync(footballFields, query, RoleMapping.ToRoleDto);
         
        }
    }
}