using System.ComponentModel.DataAnnotations;

namespace NFT.Core.Entities;

public class Role
{
    public Guid Id { get; set; }
    [MaxLength(15)]
    public string RoleName { get; set; } = "User";
}