using System.Net.Http.Json;
using MediatR;
using NFT.Shared.DataTransferObjects.NFT;
using NFT.Shared.DataTransferObjects.Pagination;

namespace NFT.Client.Services.NftServices;

public class NftService : INftService
{
    private readonly HttpClient _httpClient;

    public NftService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Guid> CreateNft(NftDto request)
    {
        var result = await _httpClient.PostAsJsonAsync("api/Nft/create", request);
        return await result.Content.ReadFromJsonAsync<Guid>();
    }

    public async Task<Unit> DeleteNft(Guid id)
    {
        var result = await _httpClient.DeleteAsync($"api/Nft/{id}");
        return await result.Content.ReadFromJsonAsync<Unit>();
    }
    
    public async Task<List<NftDto>> GetNotPaginatedNfts()
    {
        var result = await _httpClient.GetAsync("api/Nft/list");
        return await result.Content.ReadFromJsonAsync<List<NftDto>>();
    }

    public async Task<NftDto> GetNftById(Guid id)
    {
        var result = await _httpClient.GetAsync($"api/Nft/{id}");
        return await result.Content.ReadFromJsonAsync<NftDto>();
    }


    public async Task<PaginationResult<NftDto>> GetNfts(NftDto queryModel)
    {
        var result = await _httpClient.PostAsJsonAsync("api/Nft/all", queryModel);
        //if (!await _snackbarNotification.IsNotSuccessfull(result)) return default;
        return await result.Content.ReadFromJsonAsync<PaginationResult<NftDto>>();
    }
    
    
    public async Task<Unit> EditNft(NftDto request)
    {
        var result = await _httpClient.PutAsJsonAsync($"api/Nft/edit", request);
        //if (!await _snackbarNotification.IsSuccessfull(result)) return default;
        return await result.Content.ReadFromJsonAsync<Unit>();
    }
}