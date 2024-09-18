using MediatR;
using Microsoft.AspNetCore.Mvc;
using NFT.Shared.DataTransferObjects.NFT;
using NFT.Shared.DataTransferObjects.Pagination;
using NFT.UseCases.Nft.Commands;
using NFT.UseCases.Nft.Queries;

namespace NFT.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NftController : ControllerBase
{
    private readonly IMediator _mediator;

    public NftController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<Guid> CreateNft(NftDto request)
    {
        return await _mediator.Send(new CreateNftCommand(request));
    }

    [HttpPut("edit")]
    public async Task<Unit> EditNft([FromBody] NftDto request)
    {
        return await _mediator.Send(new EditNftCommand(request));
    }

    [HttpDelete("delete/{id}")]
    public async Task<Unit> DeleteNft([FromRoute] Guid id)
    {
        return await _mediator.Send(new DeleteNftCommand { Id = id });
    }

    [HttpGet("{id}")]
    public async Task<NftDto> GetNftById([FromRoute] Guid id)
    {
        return await _mediator.Send(new GetNftByIdQuery(id));
    }
    
    [HttpGet("list")]
    public async Task<List<NftDto>> GetNotPaginatedNfts()
    {
        return await _mediator.Send(new GetNotPaginatedNftsListQuery());
    }

    [HttpPost("all")]
    public async Task<PaginationResult<NftDto>> GetNfts(PaginationParameter parameter)
    {
        var response = new GetNftsListQuery(parameter);
        return await _mediator.Send(response);
    }
}