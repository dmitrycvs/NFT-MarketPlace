using System.Linq.Expressions;
using NFT.Core.Entities;
using NFT.Shared.DataTransferObjects.Roles;

namespace NFT.UseCases.Roles;

public class RoleMapping
{
    public static Expression<Func<Role, RoleDto>> ToRoleDto
    {
        get
        {
            return role => new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}