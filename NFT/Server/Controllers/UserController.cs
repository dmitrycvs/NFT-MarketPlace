using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NFT.Shared.DataTransferObjects.Pagination;
using NFT.Shared.DataTransferObjects.Users;
using NFT.UseCases.Users.Commands;
using NFT.UseCases.Users.Queries;


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

        //[Authorize(Policy = "ManageUser")]
        [HttpPut("edit")]
        public async Task<Unit> EditUser([FromBody] UserDto request)
        {
            return await _mediator.Send(new EditUserCommand(request));
        }

        //[Authorize(Policy = "DeleteUser")]
        [HttpDelete("delete/{id}")]
        public async Task<Unit> DeleteUser([FromRoute] Guid id)
        {
            return await _mediator.Send(new DeleteUserCommand { Id = id });
        }


       // [Authorize(Policy = "ViewUsers")]
        [HttpGet("{id}")]
        public async Task<UserDto> GetUserById([FromRoute] Guid id)
        {
            return await _mediator.Send(new GetUserByIdQuery(id));
        }


        // [Authorize(Policy = "ViewUsers")]
        // [HttpGet("list")]
        // public async Task<List<UserDto>> GetNotPaginatedUsers()
        // {
        //     return await _mediator.Send(new GetNotPaginatedUsersListQuery());
        // }
        //
        // // [Authorize(Policy = "ViewUsers")]
        // [HttpPost("all")]
        // public async Task<PaginationResult<UserDto>> GetUsers([FromBody] UserListQueryDto queryModel)
        // {
        //     var response = new GetUserListQuery(queryModel);
        //     return await _mediator.Send(response);
        // }

        /*

        [HttpPut("password")]
        public async Task<string> EditUserPassword([FromBody] ChangePasswordDto request)
        {
            return await _mediator.Send(new EditUserPasswordCommand(request));
        }

       

       
        //[HttpPost("register")]
        //public async Task<Guid> RegisterUser(RegisterUserDto request)
        //{
        //    return await _mediator.Send(new RegisterUserCommand(request));
        //}

      

        [Authorize(Policy = "ArhiveUser")]
        [HttpPut("archive")]
        public async Task<Unit> ArchiveUser([FromBody] Guid id)
        {
            return await _mediator.Send(new ArchiveUserCommand(id));

        }

       

        [HttpGet("myDetails/{id}")]
        public async Task<GetUserDto> GetMyProfileDetails([FromRoute] Guid id)
        {
            return await _mediator.Send(new GetMyProfileDetailsQuery(id));
        }

        [HttpPut("editMyDetails")]
        public async Task<Unit> EditMyDetails([FromBody] GetUserDto request)
        {
            return await _mediator.Send(new EditMyDetailsCommand(request));
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResult>> Login(UserLoginDto request)
        {
            var command = new LoginCommand(request);
            var result = await _mediator.Send(command);

            HttpContext.Response.Cookies.Append("token", result.Token, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                HttpOnly = false,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,
            });

            return result;
        }

        [HttpGet("log-out")]
        [HttpPost("log-out")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> LogOut()
        {
            //AuthHelper.RemoveUserKey();
            Response.Cookies.Delete("token");

            return Ok("");
        }
    }
         */

    }
}
