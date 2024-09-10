using MediatR;
using Microsoft.EntityFrameworkCore;
using NFT.Shared.DataTransferObjects.Users;
using NFT.Infrastructure;

namespace SFRAdmin.UseCases.Users.Queries
{
    public class GetNotPaginatedUsersListQuery : IRequest<List<UserDto>>
    {
        public GetNotPaginatedUsersListQuery()
        {
        }
    }

    public class GetNotPaginatedUsersListQueryHandler : IRequestHandler<GetNotPaginatedUsersListQuery, List<UserDto>>
    {
        private readonly AppDbContext _appDbContext;

        public GetNotPaginatedUsersListQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<UserDto>> Handle(GetNotPaginatedUsersListQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.Users.Select(user => new UserDto
            {
                Id = user.Id,
                Login = user.Login
            }).ToListAsync(cancellationToken);
        }
    }
}
