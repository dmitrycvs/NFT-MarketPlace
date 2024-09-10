using FluentValidation;
using MediatR;
using NFT.Core.Entities;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.Roles;

namespace NFT.UseCases.Roles.Commands;

public class CreateRoleCommand : IRequest<Guid>
{
    public string RoleName { get; set; }

    public CreateRoleCommand(RoleDto roleDto)
    {
        RoleName = roleDto.RoleName;
    }
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Guid>
{
    private readonly AppDbContext _appDbContext;

    public CreateRoleCommandHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var roleToAdd = new Role
        {
            Id = Guid.NewGuid(),
            RoleName = request.RoleName
        };
        
        _appDbContext.Roles.Add(roleToAdd);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        
        return roleToAdd.Id;
    }
}

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.RoleName).NotEmpty().WithMessage("Role cannot be empty");
    }
}