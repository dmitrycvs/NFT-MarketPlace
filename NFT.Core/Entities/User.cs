namespace NFT.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    }
}
