using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.Roles;

namespace NFT.UseCases.Roles.Queries;

public class GetRoleByIdQuery : IRequest<RoleDto>
{
    public Guid Id { get; set; }

    public GetRoleByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
{
    private readonly AppDbContext _appDbContext;

    public GetRoleByIdQueryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        return await _appDbContext.Roles
            .Where(r => r.Id == request.Id)
            .Select(role => new RoleDto {Id = role.Id, RoleName = role.RoleName})
            .FirstOrDefaultAsync(cancellationToken);
    }
}

public class GetRoleByIdQueryValidator : AbstractValidator<GetRoleByIdQuery>
{
    public GetRoleByIdQueryValidator(IServiceProvider services)
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");

        RuleFor(x => x).Custom((query, context) =>
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var roleExist = dbContext.Roles.Any(r => r.Id == query.Id);
            if (!roleExist)
            {
                context.AddFailure("Role not found");
            }
        });
    }
}