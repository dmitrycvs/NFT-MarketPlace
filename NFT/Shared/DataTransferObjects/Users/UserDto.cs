using NFT.Core.Entities;
using NFT.Shared.DataTransferObjects.Inventories;

namespace NFT.Shared.DataTransferObjects.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;

        //public bool IsAuthenticated { get; set; } //pentru viitor

    }
}
