using NFT.Core.Entities;
using NFT.Shared.DataTransferObjects.Inventories;
using NFT.Shared.DataTransferObjects.Users;
using System.Linq.Expressions;

namespace NFT.UseCases.Users
{
    public static class UserMapping
    {
        public static Expression<Func<User, UserDto>> ToUserDto
        {
            get
            {
                return user => new UserDto
                {
                    Id = user.Id,
                    Login = user.Login,
                    Inventories = user.Inventories.Select(inventory => new InventoryDto
                    {
                        Id = inventory.Id,
                    }).ToList()
                    //IsAuthenticated = user.IsAuthenticated //pentru viitor

                };
            }
        }
    }
}
