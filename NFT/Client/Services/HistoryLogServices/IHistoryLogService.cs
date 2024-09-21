using MediatR;
using NFT.Shared.DataTransferObjects.HistoryLogs;
using NFT.Shared.DataTransferObjects.Pagination;

namespace NFT.Client.Services.HistoryLogServices;

public interface IHistoryLogService
{
    Task<Guid> CreateHistoryLog(HistoryLogDto request);
    Task<Unit> DeleteHistoryLog(Guid id);
    Task<Unit> EditHistoryLog(HistoryLogDto request);
    Task<List<HistoryLogDto>> GetNotPaginatedHistoryLogs();
    Task<HistoryLogDto> GetHistoryLogById(Guid id);
    Task<PaginationResult<HistoryLogDto>> GetHistoryLogs(HistoryLogDto queryModel);
}