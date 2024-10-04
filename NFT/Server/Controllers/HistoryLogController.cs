using MediatR;
using Microsoft.AspNetCore.Mvc;
using NFT.Shared.DataTransferObjects.HistoryLogs;
using NFT.Shared.DataTransferObjects.Pagination;
using NFT.UseCases.HistoryLogs.Commands;
using NFT.UseCases.HistoryLogs.Queries;

namespace NFT.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HistoryLogController : ControllerBase
{
    private readonly IMediator _mediator;

    public HistoryLogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<Guid> CreateHistoryLog(HistoryLogDto request)
    {
        return await _mediator.Send(new CreateHistoryLogCommand(request));
    }

    [HttpPut("edit")]
    public async Task<Unit> EditHistoryLog([FromBody] HistoryLogDto request)
    {
        return await _mediator.Send(new EditHistoryLogCommand(request));
    }

    [HttpDelete("delete/{id}")]
    public async Task<Unit> DeleteHistoryLog([FromRoute] Guid id)
    {
        return await _mediator.Send(new DeleteHistoryLogCommand { Id = id });
    }

    [HttpGet("{id}")]
    public async Task<HistoryLogDto> GetHistoryLogById([FromRoute] Guid id)
    {
        return await _mediator.Send(new GetHistoryLogByIdQuery(id));
    }
    
    [HttpGet("list")]
    public async Task<List<HistoryLogDto>> GetNotPaginatedHistoryLogs()
    {
        return await _mediator.Send(new GetNotPaginatedHistoryLogsListQuery());
    }

    [HttpPost("all")]
    public async Task<PaginationResult<HistoryLogDto>> GetHistoryLogs(PaginationParameter parameter)
    {
        var response = new GetHistoryLogListQuery(parameter);
        return await _mediator.Send(response);
    }
}