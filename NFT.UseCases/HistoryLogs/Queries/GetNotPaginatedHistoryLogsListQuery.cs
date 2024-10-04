using MediatR;
using Microsoft.EntityFrameworkCore;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.HistoryLogs;

namespace NFT.UseCases.HistoryLogs.Queries;

public class GetNotPaginatedHistoryLogsListQuery : IRequest<List<HistoryLogDto>>
{
    public GetNotPaginatedHistoryLogsListQuery()
    {
    }
}

public class GetNotPaginatedHistoryLogsQueryHandler : IRequestHandler<GetNotPaginatedHistoryLogsListQuery, List<HistoryLogDto>>
{
    private readonly AppDbContext _appDbContext;

    public GetNotPaginatedHistoryLogsQueryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<HistoryLogDto>> Handle(GetNotPaginatedHistoryLogsListQuery request,
        CancellationToken cancellationToken)
    {
        return await _appDbContext.HistoryLogs.Select(HistoryLog => new HistoryLogDto
        {
            Id = HistoryLog.Id,
            BuyerId = HistoryLog.BuyerId,
            SellerId = HistoryLog.SellerId,
            DateTime = HistoryLog.DateTime,
            DealPrice = HistoryLog.DealPrice,
            NftItemId = HistoryLog.NftItemId
        }).ToListAsync(cancellationToken);
    }
}