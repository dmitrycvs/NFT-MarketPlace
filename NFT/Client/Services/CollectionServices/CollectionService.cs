using System.Net.Http.Json;
using MediatR;
using NFT.Shared.DataTransferObjects.Collections;
using NFT.Shared.DataTransferObjects.Pagination;

namespace NFT.Client.Services.CollectionServices;

public class CollectionService : ICollectionService
{
    private readonly HttpClient _httpClient;

    public CollectionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Guid> CreateCollection(CollectionDto request)
    {
        var result = await _httpClient.PostAsJsonAsync("api/Collection/create", request);
        return await result.Content.ReadFromJsonAsync<Guid>();
    }

    public async Task<Unit> EditCollection(CollectionDto request)
    {
        var result = await _httpClient.PutAsJsonAsync($"api/Collection/edit", request);
        return await result.Content.ReadFromJsonAsync<Unit>();
    }

    public async Task<Unit> DeleteCollection(Guid id)
    {
        var result = await _httpClient.DeleteAsync($"api/Collection/{id}");
        return await result.Content.ReadFromJsonAsync<Unit>();
    }
    
    public async Task<List<CollectionDto>> GetNotPaginatedCollections()
    {
        var result = await _httpClient.GetAsync("api/Collection/list");
        return await result.Content.ReadFromJsonAsync<List<CollectionDto>>();
    }

    public async Task<CollectionDto> GetCollectionById(Guid id)
    {
        var result = await _httpClient.GetAsync($"api/Collection/{id}");
        return await result.Content.ReadFromJsonAsync<CollectionDto>();
    }


    public async Task<PaginationResult<CollectionDto>> GetCollections(CollectionDto queryModel)
    {
        var result = await _httpClient.PostAsJsonAsync("api/Collection/all", queryModel);
        //if (!await _snackbarNotification.IsNotSuccessfull(result)) return default;
        return await result.Content.ReadFromJsonAsync<PaginationResult<CollectionDto>>();
    }
}