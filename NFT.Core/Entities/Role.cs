namespace NFT.Core.Entities;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    //public List<UserRolePermission> UserRolePermissions { get; set; } pentru viitor

}