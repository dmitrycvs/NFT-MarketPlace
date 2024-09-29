/*using System.Net.Http.Json;
using MediatR;
using NFT.Shared.DataTransferObjects.Inventories;
using NFT.Shared.DataTransferObjects.Pagination;

namespace NFT.Client.Services.InventoryServices;

public class InventoryService : IInventoryService
{
    private readonly HttpClient _httpClient;

    public InventoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Guid> CreateInventory(InventoryDto request)
    {
        var result = await _httpClient.PostAsJsonAsync("api/inventory/create", request);
        return await result.Content.ReadFromJsonAsync<Guid>();
    }

    public async Task<Unit> DeleteInventory(Guid id)
    {
        var result = await _httpClient.DeleteAsync($"api/inventory/{id}");
        return await result.Content.ReadFromJsonAsync<Unit>();
    }
    
    public async Task<List<InventoryDto>> GetNotPaginatedInventories()
    {
        var result = await _httpClient.GetAsync("api/inventory/list");
        return await result.Content.ReadFromJsonAsync<List<InventoryDto>>();
    }

    public async Task<InventoryDto> GetInventoryById(Guid id)
    {
        var result = await _httpClient.GetAsync($"api/inventory/{id}");
        return await result.Content.ReadFromJsonAsync<InventoryDto>();
    }


    public async Task<PaginationResult<InventoryDto>> GetInventories(InventoryDto queryModel)
    {
        var result = await _httpClient.PostAsJsonAsync("api/inventory/all", queryModel);
        //if (!await _snackbarNotification.IsNotSuccessfull(result)) return default;
        return await result.Content.ReadFromJsonAsync<PaginationResult<InventoryDto>>();
    }
}*/
//Temporarly disabled commands and queries regarding Inventory