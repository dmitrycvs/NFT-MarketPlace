using System.ComponentModel.DataAnnotations;

namespace NFT.Shared.DataTransferObjects.Roles;

public class RoleDto
{
    public Guid Id { get; set; }
    [MaxLength(15)]
    public string RoleName { get; set; } = "User";
}