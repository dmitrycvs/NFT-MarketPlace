using MediatR;
using Microsoft.AspNetCore.Mvc;
using NFT.Shared.DataTransferObjects.Collections;
using NFT.Shared.DataTransferObjects.Pagination;
using NFT.UseCases.Collections.Commands;
using NFT.UseCases.Collections.Queries;

namespace NFT.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CollectionController : ControllerBase
{
    private readonly IMediator _mediator;

    public CollectionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<Guid> CreateCollection(CollectionDto request)
    {
        return await _mediator.Send(new CreateCollectionCommand(request));
    }

    [HttpPut("edit")]
    public async Task<Unit> EditCollection([FromBody] CollectionDto request)
    {
        return await _mediator.Send(new EditCollectionCommand(request));
    }

    [HttpDelete("delete/{id}")]
    public async Task<Unit> DeleteCollection([FromRoute] Guid id)
    {
        return await _mediator.Send(new DeleteCollectionCommand { Id = id });
    }

    [HttpGet("{id}")]
    public async Task<CollectionDto> GetCollectionById([FromRoute] Guid id)
    {
        return await _mediator.Send(new GetCollectionByIdQuery(id));
    }
    
    [HttpGet("list")]
    public async Task<List<CollectionDto>> GetNotPaginatedCollections()
    {
        return await _mediator.Send(new GetNotPaginatedCollectionsListQuery());
    }

    [HttpPost("all")]
    public async Task<PaginationResult<CollectionDto>> GetCollections(PaginationParameter parameter)
    {
        var response = new GetCollectionListQuery(parameter);
        return await _mediator.Send(response);
    }
}