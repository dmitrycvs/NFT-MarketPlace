using NFT.Shared.DataTransferObjects.Users;

namespace NFT.Client.Services.UserServices
{
    public interface IUserService
    {
        Task<Guid> CreateUser(UserDto request);
        /* Task<PaginationResult<GetUserDto>> GetUsers(UserListQueryDto queryModel);
         Task<List<GetUserDto>> GetNotPaginatedUsers();
         Task<GetUserDto> GetMyProfileDetails(Guid id);
         Task<Unit> EditMyDetails(GetUserDto user);
         Task<GetUserDto> GetUserById(Guid id);*/

        /* Task<Guid> RegisterUser(RegisterUserDto request);
         Task<Unit> EditUser(EditUserDto request);*/
        /* Task<Unit> ArchiveUser(Guid id);
         Task<Unit> DeleteUser(Guid id);
         Task<string> EditUserPassword(ChangePasswordDto user);
         Task<LoginResult> Login(UserLoginDto request);
         Task<string> Logout();*/
    }
}
