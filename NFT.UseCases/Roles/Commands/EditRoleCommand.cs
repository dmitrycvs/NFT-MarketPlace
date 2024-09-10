using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.Roles;

namespace NFT.UseCases.Roles.Commands;

public class EditRoleCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string RoleName { get; set; }

    public EditRoleCommand(RoleDto roleDto)
    {
        Id = roleDto.Id;
        RoleName = roleDto.RoleName;
    }
}

public class EditRoleCommandHandler : IRequestHandler<EditRoleCommand, Unit>
{
    private readonly AppDbContext _appDbContext;

    public EditRoleCommandHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Unit> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var roleToEdit = await _appDbContext.Roles
            .Where(r => r.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (roleToEdit == null)
        {
            throw new InvalidOperationException("Role not found");
        }

        if (!string.IsNullOrEmpty(request.RoleName))
        {
            roleToEdit.RoleName = request.RoleName;
        }
        
        await _appDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}

public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
{
    public EditRoleCommandValidator(IServiceProvider services)
    {
        RuleFor(r => r.Id).NotEmpty().WithMessage("Role ID must not be empty");
        
        RuleFor(r => r.RoleName).NotEmpty().WithMessage("Role Name must not be empty");

        RuleFor(r => r).Custom((command, context) =>
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var roleExist = dbContext.Roles.Any(r => r.Id == command.Id);
            if (!roleExist)
            {
                context.AddFailure("Role not found");
            }
        });
    }
}