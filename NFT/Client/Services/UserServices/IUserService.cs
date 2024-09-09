using MediatR;
using NFT.Shared.DataTransferObjects.Pagination;
using NFT.Shared.DataTransferObjects.Users;

namespace NFT.Client.Services.UserServices
{
    public interface IUserService
    {
        Task<Guid> CreateUser(UserDto request);
        Task<Unit> DeleteUser(Guid id);
        Task<Unit> EditUser(UserDto request);
        Task<List<UserDto>> GetNotPaginatedUsers();
        Task<UserDto> GetUserById(Guid id);
       Task<PaginationResult<UserDto>> GetUsers(UserDto queryModel);

        
        
        /*
        
        Task<GetUserDto> GetMyProfileDetails(Guid id);
        Task<Unit> EditMyDetails(GetUserDto user);
        */

        // Task<Guid> RegisterUser(RegisterUserDto request);
        // Task<Unit> ArchiveUser(Guid id);
        /* 
         Task<string> EditUserPassword(ChangePasswordDto user);
         Task<LoginResult> Login(UserLoginDto request);
         Task<string> Logout();*/
    }
}
