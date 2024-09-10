using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFT.Infrastructure;

namespace NFT.UseCases.Roles.Commands;

public class DeleteRoleCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Unit>
{
    private readonly AppDbContext _appDbcontext;

    public DeleteRoleCommandHandler(AppDbContext appDbcontext)
    {
        _appDbcontext = appDbcontext;
    }

    public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var roleToDelete = await _appDbcontext.Roles
            .Where(r => r.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (roleToDelete == null)
        {
            throw new InvalidOperationException("Role not found");
        }
        
        _appDbcontext.Roles.Remove(roleToDelete);
        await _appDbcontext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}

public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator(IServiceProvider services)
    {
        RuleFor(r => r.Id).NotEmpty().WithMessage("Id cannot be empty");

        RuleFor(r => r).Custom((command, context) =>
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var roleExists = dbContext.Roles.Any(r => r.Id == command.Id);
            if (!roleExists)
            {
                context.AddFailure("Invalid role ID - the role does not exist");
            }
        });
    }
}