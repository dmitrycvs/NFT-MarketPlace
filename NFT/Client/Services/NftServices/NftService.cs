using System.Net.Http.Json;
using MediatR;
using NFT.Shared.DataTransferObjects.HistoryLogs;
using NFT.Shared.DataTransferObjects.NftItem;
using NFT.Shared.DataTransferObjects.Pagination;

namespace NFT.Client.Services.NftServices;

public class NftService : INftService
{
    private readonly HttpClient _httpClient;

    public NftService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Guid> CreateNft(NftItemDto request)
    {
        var result = await _httpClient.PostAsJsonAsync("api/Nft/create", request);
        return await result.Content.ReadFromJsonAsync<Guid>();
    }

    public async Task<Unit> DeleteNft(Guid id)
    {
        var result = await _httpClient.DeleteAsync($"api/Nft/{id}");
        return await result.Content.ReadFromJsonAsync<Unit>();
    }
    
    public async Task<List<NftItemDto>> GetNotPaginatedNfts()
    {
        var result = await _httpClient.GetAsync("api/Nft/list");
        return await result.Content.ReadFromJsonAsync<List<NftItemDto>>();
    }

    public async Task<NftItemDto> GetNftById(Guid id)
    {
        var result = await _httpClient.GetAsync($"api/Nft/{id}");
        return await result.Content.ReadFromJsonAsync<NftItemDto>();
    }


    public async Task<PaginationResult<NftItemDto>> GetNfts(NftItemDto queryModel)
    {
        var result = await _httpClient.PostAsJsonAsync("api/Nft/all", queryModel);
        //if (!await _snackbarNotification.IsNotSuccessfull(result)) return default;
        return await result.Content.ReadFromJsonAsync<PaginationResult<NftItemDto>>();
    }
    
    
    public async Task<Unit> EditNft(NftItemDto request)
    {
        var result = await _httpClient.PutAsJsonAsync($"api/Nft/edit", request);
        //if (!await _snackbarNotification.IsSuccessfull(result)) return default;
        return await result.Content.ReadFromJsonAsync<Unit>();
    }
    public async Task<Guid> SellNft(HistoryLogDto request)
    {
        var result = await _httpClient.PostAsJsonAsync("api/Nft/sell", request);
        return await result.Content.ReadFromJsonAsync<Guid>();
    }

    public async Task<List<NftItemDto>> GetAllNftByUserId(Guid userId)
    {
        var result = await _httpClient.GetAsync($"api/Nft/listByUserId/{userId}");
        return await result.Content.ReadFromJsonAsync<List<NftItemDto>>();
    }
}