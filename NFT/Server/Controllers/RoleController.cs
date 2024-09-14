using MediatR;
using Microsoft.AspNetCore.Mvc;
using NFT.Shared.DataTransferObjects.Pagination;
using NFT.Shared.DataTransferObjects.Roles;
using NFT.UseCases.Roles.Commands;
using NFT.UseCases.Roles.Queries;

namespace NFT.Server.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<Guid> CreateRole(RoleDto request)
    {
        return await _mediator.Send(new CreateRoleCommand(request));
    }

    [HttpPut("edit")]
    public async Task<Unit> EditRole([FromBody] RoleDto request)
    {
        return await _mediator.Send(new EditRoleCommand(request));
    }

    [HttpDelete("delete/{id}")]
    public async Task<Unit> DeleteRole([FromRoute] Guid id)
    {
        return await _mediator.Send(new DeleteRoleCommand { Id = id });
    }

    [HttpGet("{id}")]
    public async Task<RoleDto> GetRoleById([FromRoute] Guid id)
    {
        return await _mediator.Send(new GetRoleByIdQuery(id));
    }
    
    [HttpGet("list")]
    public async Task<List<RoleDto>> GetNotPaginatedRoles()
    {
        return await _mediator.Send(new GetNotPaginatedRolesListQuery());
    }

    [HttpPost("all")]
    public async Task<PaginationResult<RoleDto>> GetRoles(PaginationParameter parameter)
    {
        var response = new GetRoleListQuery(parameter);
        return await _mediator.Send(response);
    }
}