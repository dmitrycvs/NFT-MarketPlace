using NFT.Shared.DataTransferObjects.Users;
using System.Net.Http.Json;

namespace NFT.Client.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

       /* public async Task<List<GetUserDto>> GetNotPaginatedUsers()
        {
            var result = await _httpClient.GetAsync("api/user/list");
            return await result.Content.ReadFromJsonAsync<List<GetUserDto>>();
        }

        public async Task<GetUserDto> GetUserById(Guid id)
        {
            var result = await _httpClient.GetAsync($"api/user/{id}");
            return await result.Content.ReadFromJsonAsync<GetUserDto>();
        }

        public async Task<PaginationResult<GetUserDto>> GetUsers(UserListQueryDto queryModel)
        {
            var result = await _httpClient.PostAsJsonAsync("api/user/all", queryModel);
            //if (!await _snackbarNotification.IsNotSuccessfull(result)) return default;
            return await result.Content.ReadFromJsonAsync<PaginationResult<GetUserDto>>();
        }*/

        public async Task<Guid> CreateUser(UserDto request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/user/create", request);
            //if (!await _snackbarNotification.IsSuccessfull(result)) return default;
            return await result.Content.ReadFromJsonAsync<Guid>();
        }

       /* public async Task<Guid> RegisterUser(RegisterUserDto request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/user/register", request);
            //if (!await _snackbarNotification.IsSuccessfull(result)) return default;
            return await result.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task<Unit> EditUser(EditUserDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/user/edit", request);
            //if (!await _snackbarNotification.IsSuccessfull(result)) return default;
            return await result.Content.ReadFromJsonAsync<Unit>();
        }

        public async Task<Unit> ArchiveUser(Guid id)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/user/archive", id);
            return await result.Content.ReadFromJsonAsync<Unit>();
        }

        public async Task<Unit> DeleteUser(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"api/user/delete/{id}");
            //if (!await _snackbarNotification.IsSuccessfull(result)) return default;
            return await result.Content.ReadFromJsonAsync<Unit>();
        }

        public async Task<LoginResult> Login(UserLoginDto request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/user/login", request);
            return await result.Content.ReadFromJsonAsync<LoginResult>();
        }

        public async Task<string> Logout()
        {
            var result = await _httpClient.GetAsync("api/user/log-out");
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> EditUserPassword(ChangePasswordDto request)
        {
            if (request.CurrentPassword == request.NewPassword)
            {
                //_snackbar.Add("New password cannot be the same as the old password", Severity.Warning);
                return string.Empty;
            }
            var result = await _httpClient.PutAsJsonAsync($"api/user/password", request);
            if (result.IsSuccessStatusCode)
            {
                // _snackbar.Add("Password sucessfully reset!", Severity.Success);
                return await result.Content.ReadAsStringAsync();
            }

            // _snackbar.Add("There was an error: Old password does not match", Severity.Error);
            return string.Empty;
        }

        public async Task<GetUserDto> GetMyProfileDetails(Guid id)
        {
            var result = await _httpClient.GetAsync($"api/user/myDetails/{id}");

            return await result.Content.ReadFromJsonAsync<GetUserDto>();
        }

        public async Task<Unit> EditMyDetails(GetUserDto user)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/user/editMyDetails", user);
            // if (!await _snackbarNotification.IsSuccessfull(result)) return default;

            return await result.Content.ReadFromJsonAsync<Unit>();
        }*/
    }
}
