using System.ComponentModel.DataAnnotations;

namespace NFT.Shared.DataTransferObjects.Roles;

public class RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
}