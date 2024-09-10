using FluentValidation;
using MediatR;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NFT.UseCases.Users.Commands
{
    public class EditUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;

        public EditUserCommand(UserDto user)
        {
            Id = user.Id;
            Login = user.Login;
        }
    }

    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public EditUserCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var userToEdit = await _appDbContext.Users
                .Where(u => u.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (userToEdit == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            if (!string.IsNullOrEmpty(request.Login))
            {
                userToEdit.Login = request.Login;
            }

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator(IServiceProvider services)
        {
            RuleFor(u => u.Id).NotEmpty().WithMessage("User ID must not be empty");

            RuleFor(u => u.Login)
                .NotEmpty().WithMessage("Login must not be empty");

            RuleFor(u => u).Custom((command, context) =>
            {
                using var scope = services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var userExists = dbContext.Users.Any(u => u.Id == command.Id);
                if (!userExists)
                {
                    context.AddFailure("The user does not exist.");
                }
            });
        }
    }
}
