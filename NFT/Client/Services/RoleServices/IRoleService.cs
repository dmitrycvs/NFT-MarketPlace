using MediatR;
using NFT.Shared.DataTransferObjects.Roles;

namespace NFT.Client.Services.RoleServices;

public interface IRoleService
{
    Task<Guid> CreateRole(RoleDto request);
    Task<Unit> DeleteRole(Guid id);
    Task<Unit> EditRole(RoleDto request);
    //Task<List<RoleDto>> GetNotPaginatedRoles();
    Task<RoleDto> GetRoleById(Guid id);
    //Task<PaginationResult<RoleDto>> GetRoles(RoleDto queryModel);
}