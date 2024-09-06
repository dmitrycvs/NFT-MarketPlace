using MediatR;
using Microsoft.AspNetCore.Mvc;
using NFT.Shared.DataTransferObjects.Users;
using NFT.UseCases.Users.Commands;



namespace NFT.Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

       

        //[Authorize(Policy = "ManageUser")]
        [HttpPost("create")]
        public async Task<Guid> CreateUser(UserDto request)
        {
            return await _mediator.Send(new CreateUserCommand(request));
        }

        
        
    }
}
