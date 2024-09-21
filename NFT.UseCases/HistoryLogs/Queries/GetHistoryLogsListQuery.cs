using MediatR;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.HistoryLogs;
using NFT.Shared.DataTransferObjects.Pagination;

namespace NFT.UseCases.HistoryLogs.Queries;

public class GetHistoryLogListQuery : IRequest<PaginationResult<HistoryLogDto>>
{
    public GetHistoryLogListQuery(PaginationParameter paginationParameter)
    {
        PaginationParameter = paginationParameter;
    }
    public PaginationParameter PaginationParameter { get; set; }
}

public class GetHistoryLogListQueryHandler : IRequestHandler<GetHistoryLogListQuery, PaginationResult<HistoryLogDto>>
{
    private readonly IPaginationService _paginationService;

    private readonly AppDbContext _appDbContext;

    public GetHistoryLogListQueryHandler(IPaginationService paginationService, AppDbContext appDbContext)
    {
        _paginationService = paginationService;
        _appDbContext = appDbContext;
    }
    public async Task<PaginationResult<HistoryLogDto>> Handle(GetHistoryLogListQuery request, CancellationToken cancellationToken)
    {
        var query = request.PaginationParameter;

        var HistoryLogs = _appDbContext.HistoryLogs.AsQueryable();

        return await _paginationService.PaginateAsync(HistoryLogs, query, HistoryLogMapping.ToHistoryLogDto);
         
    }
}