using System.Net.Http.Json;
using MediatR;
using NFT.Shared.DataTransferObjects.Pagination;
using NFT.Shared.DataTransferObjects.Roles;

namespace NFT.Client.Services.RoleServices;

public class RoleService : IRoleService
{
    private readonly HttpClient _httpClient;

    public RoleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Guid> CreateRole(RoleDto request)
    {
        var result = await _httpClient.PostAsJsonAsync("api/role/create", request);
        return await result.Content.ReadFromJsonAsync<Guid>();
    }

    public async Task<Unit> EditRole(RoleDto request)
    {
        var result = await _httpClient.PutAsJsonAsync($"api/role/edit", request);
        return await result.Content.ReadFromJsonAsync<Unit>();
    }

    public async Task<Unit> DeleteRole(Guid id)
    {
        var result = await _httpClient.DeleteAsync($"api/role/{id}");
        return await result.Content.ReadFromJsonAsync<Unit>();
    }
    
    public async Task<List<RoleDto>> GetNotPaginatedRoles()
    {
        var result = await _httpClient.GetAsync("api/role/list");
        return await result.Content.ReadFromJsonAsync<List<RoleDto>>();
    }

    public async Task<RoleDto> GetRoleById(Guid id)
    {
        var result = await _httpClient.GetAsync($"api/role/{id}");
        return await result.Content.ReadFromJsonAsync<RoleDto>();
    }


    public async Task<PaginationResult<RoleDto>> GetRoles(RoleDto queryModel)
    {
        var result = await _httpClient.PostAsJsonAsync("api/role/all", queryModel);
        //if (!await _snackbarNotification.IsNotSuccessfull(result)) return default;
        return await result.Content.ReadFromJsonAsync<PaginationResult<RoleDto>>();
    }
}