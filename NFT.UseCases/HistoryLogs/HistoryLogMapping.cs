using System.Linq.Expressions;
using NFT.Core.Entities;
using NFT.Shared.DataTransferObjects.HistoryLogs;

namespace NFT.UseCases.HistoryLogs;

public class HistoryLogMapping
{
    public static Expression<Func<HistoryLog, HistoryLogDto>> ToHistoryLogDto
    {
        get
        {
            return historyLog => new HistoryLogDto
            {
                Id = historyLog.Id,
                DateTime = historyLog.DateTime,
                DealPrice = historyLog.DealPrice,
                NftItemId = historyLog.NftItemId,
                BuyerId = historyLog.BuyerId,
                SellerId = historyLog.SellerId,
            };
        }
    }
}