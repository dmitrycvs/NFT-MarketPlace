namespace NFT.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public Guid? UserRoleId { get; set; }
        public Role UserRole { get; set; }
        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    }
}
