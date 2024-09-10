using MediatR;
using NFT.Shared.DataTransferObjects.Users;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.Pagination;

namespace NFT.UseCases.Users.Queries
{
    public class GetUserListQuery : IRequest<PaginationResult<UserDto>>
    {
        public GetUserListQuery(UserListQueryDto queryParameters)
        {
            QueryParameters = queryParameters;
        }

        public UserListQueryDto QueryParameters { get; set; }
    }

    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, PaginationResult<UserDto>>
    {
        private readonly IPaginationService _paginationService;
        private readonly AppDbContext _appDbContext;

        public GetUserListQueryHandler(IPaginationService paginationService, AppDbContext appDbContext)
        {
            _paginationService = paginationService;
            _appDbContext = appDbContext;
        }

        public async Task<PaginationResult<UserDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var users = _appDbContext.Users.AsQueryable();

            users = users.OrderBy(x => x.Login); 

            return await _paginationService.PaginateAsync(users, request.QueryParameters.PaginationParameter, user => new UserDto
            {
                Id = user.Id,
                Login = user.Login
            });
        }
    }
}
