using NFT.Shared.DataTransferObjects.Inventories;

namespace NFT.Shared.DataTransferObjects.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public List<InventoryDto> Inventories { get; set; } = new List<InventoryDto>();  

        //public bool IsAuthenticated { get; set; } //pentru viitor

    }
}
