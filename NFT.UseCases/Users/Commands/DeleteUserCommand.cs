using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFT.Infrastructure;

namespace NFT.UseCases.Users.Commands
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteUserCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await _appDbContext.Users
                .Where(u => u.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (userToDelete == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            _appDbContext.Users.Remove(userToDelete);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator(IServiceProvider services)
        {
            RuleFor(u => u.Id).NotEmpty().WithMessage("User ID must not be empty");

            RuleFor(u => u).Custom((command, context) =>
            {
                using var scope = services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var userExists = dbContext.Users.Any(u => u.Id == command.Id);
                if (!userExists)
                {
                    context.AddFailure("Invalid user ID - the user does not exist.");
                }
            });
        }
    }
}
