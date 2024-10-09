using MediatR;
using Microsoft.AspNetCore.Mvc;
using NFT.Shared.DataTransferObjects.HistoryLogs;
using NFT.Shared.DataTransferObjects.NftItem;
using NFT.Shared.DataTransferObjects.Pagination;
using NFT.UseCases.Nft.Commands;
using NFT.UseCases.Nft.Queries;
using NFT.UseCases.NftItem.Commands;
using NFT.UseCases.NftItem.Queries;

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
    public async Task<Guid> CreateNft(NftItemDto request)
    {
        return await _mediator.Send(new CreateNftCommand(request));
    }

    [HttpPut("edit")]
    public async Task<Unit> EditNft([FromBody] NftItemDto request)
    {
        return await _mediator.Send(new EditNftCommand(request));
    }

    [HttpDelete("delete/{id}")]
    public async Task<Unit> DeleteNft([FromRoute] Guid id)
    {
        return await _mediator.Send(new DeleteNftCommand { Id = id });
    }

    [HttpGet("{id}")]
    public async Task<NftItemDto> GetNftById([FromRoute] Guid id)
    {
        return await _mediator.Send(new GetNftByIdQuery(id));
    }
    
    [HttpGet("list")]
    public async Task<List<NftItemDto>> GetNotPaginatedNfts()
    {
        return await _mediator.Send(new GetNotPaginatedNftsListQuery());
    }

    [HttpPost("all")]
    public async Task<PaginationResult<NftItemDto>> GetNfts(PaginationParameter parameter)
    {
        var response = new GetNftsListQuery(parameter);
        return await _mediator.Send(response);
    }

    [HttpPost("sell")]
    public async Task<Guid> SellNft([FromBody] HistoryLogDto request)
    {
        var command = new SellNftToUserCommand
        {
            SellerId = request.SellerId,
            BuyerId = request.BuyerId,
            NftItemId = request.NftItemId,
            DealPrice = request.DealPrice
        };
        return await _mediator.Send(command);
    }

    [HttpGet("listByUserId/{userId}")]
    public async Task<List<NftItemDto>> GetNftsByUserId([FromRoute] Guid userId)
    {
        return await _mediator.Send(new GetAllNftByUserIdQuery(userId));
    }
    

}