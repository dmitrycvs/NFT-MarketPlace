using FluentValidation;
using MediatR;
using NFT.Infrastructure;  
using NFT.Core.Entities;  
using NFT.Shared.DataTransferObjects.Users;  


namespace NFT.UseCases.Users.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Login { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; }

        public CreateUserCommand(UserDto userDto)
        {
            Login = userDto.Login;
            //IsAuthenticated = userDto.IsAuthenticated;
        }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly AppDbContext _appDbContext;

        public CreateUserCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userToAdd = new User
            {
                Id = Guid.NewGuid(),
                Login = request.Login,
                //IsAuthenticated = request.IsAuthenticated
            };

            _appDbContext.Users.Add(userToAdd);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return userToAdd.Id;
        }
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Login).NotEmpty().WithMessage("Login is required.");
            RuleFor(x => x.IsAuthenticated).NotNull().WithMessage("Authentication status is required.");
        }
    }
}
