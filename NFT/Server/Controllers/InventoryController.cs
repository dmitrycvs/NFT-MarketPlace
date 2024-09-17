using MediatR;
using Microsoft.AspNetCore.Mvc;
using NFT.Shared.DataTransferObjects.Inventories;
using NFT.Shared.DataTransferObjects.Pagination;
using NFT.UseCases.Inventories.Commands;
using NFT.UseCases.Inventories.Queries;

namespace NFT.Server.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<Guid> CreateInventory(InventoryDto request)
    {
        return await _mediator.Send(new CreateInventoryCommand(request));
    }

    [HttpDelete("delete/{id}")]
    public async Task<Unit> DeleteInventory([FromRoute] Guid id)
    {
        return await _mediator.Send(new DeleteInventoryCommand { Id = id });
    }

    [HttpGet("{id}")]
    public async Task<InventoryDto> GetInventoryById([FromRoute] Guid id)
    {
        return await _mediator.Send(new GetInventoryByIdQuery(id));
    }
    
    [HttpGet("list")]
    public async Task<List<InventoryDto>> GetNotPaginatedInventories()
    {
        return await _mediator.Send(new GetNotPaginatedInventoriesListQuery());
    }

    [HttpPost("all")]
    public async Task<PaginationResult<InventoryDto>> GetInventories(PaginationParameter parameter)
    {
        var response = new GetInventoryListQuery(parameter);
        return await _mediator.Send(response);
    }
}