using MediatR;
using Microsoft.EntityFrameworkCore;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.Roles;

namespace NFT.UseCases.Roles.Queries;

public class GetNotPaginatedRolesListQuery : IRequest<List<RoleDto>>
{
    public GetNotPaginatedRolesListQuery()
    {
    }
}

public class GetNotPaginatedRolesListQueryHandler : IRequestHandler<GetNotPaginatedRolesListQuery, List<RoleDto>>
{
    private readonly AppDbContext _appDbContext;

    public GetNotPaginatedRolesListQueryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<RoleDto>> Handle(GetNotPaginatedRolesListQuery request, CancellationToken cancellationToken)
    {
        return await _appDbContext.Roles.Select(role => new RoleDto
        {
            Id = role.Id,
            Name = role.Name
        }).ToListAsync(cancellationToken);
    }
}