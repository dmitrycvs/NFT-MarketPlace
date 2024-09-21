using System.Net.Http.Json;
using MediatR;
using NFT.Shared.DataTransferObjects.HistoryLogs;
using NFT.Shared.DataTransferObjects.Pagination;

namespace NFT.Client.Services.HistoryLogServices;

public class HistoryLogService : IHistoryLogService
{
    private readonly HttpClient _httpClient;

    public HistoryLogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Guid> CreateHistoryLog(HistoryLogDto request)
    {
        var result = await _httpClient.PostAsJsonAsync("api/HistoryLog/create", request);
        return await result.Content.ReadFromJsonAsync<Guid>();
    }

    public async Task<Unit> EditHistoryLog(HistoryLogDto request)
    {
        var result = await _httpClient.PutAsJsonAsync($"api/HistoryLog/edit", request);
        return await result.Content.ReadFromJsonAsync<Unit>();
    }

    public async Task<Unit> DeleteHistoryLog(Guid id)
    {
        var result = await _httpClient.DeleteAsync($"api/HistoryLog/{id}");
        return await result.Content.ReadFromJsonAsync<Unit>();
    }
    
    public async Task<List<HistoryLogDto>> GetNotPaginatedHistoryLogs()
    {
        var result = await _httpClient.GetAsync("api/HistoryLog/list");
        return await result.Content.ReadFromJsonAsync<List<HistoryLogDto>>();
    }

    public async Task<HistoryLogDto> GetHistoryLogById(Guid id)
    {
        var result = await _httpClient.GetAsync($"api/HistoryLog/{id}");
        return await result.Content.ReadFromJsonAsync<HistoryLogDto>();
    }


    public async Task<PaginationResult<HistoryLogDto>> GetHistoryLogs(HistoryLogDto queryModel)
    {
        var result = await _httpClient.PostAsJsonAsync("api/HistoryLog/all", queryModel);
        //if (!await _snackbarNotification.IsNotSuccessfull(result)) return default;
        return await result.Content.ReadFromJsonAsync<PaginationResult<HistoryLogDto>>();
    }
}