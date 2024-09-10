using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.Users;

namespace NFT.UseCases.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public Guid Id { get; set; }

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly AppDbContext _appDbContext;

        public GetUserByIdQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.Users
                .Where(u => u.Id == request.Id)
                .Select(user => new UserDto { Id = user.Id, Login = user.Login })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }

    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator(IServiceProvider services)
        {
            RuleFor(d => d.Id).NotEmpty().WithMessage("User ID must not be empty");

            RuleFor(d => d).Custom((query, context) =>
            {
                using var scope = services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var userExists = dbContext.Users.Any(u => u.Id == query.Id);
                if (!userExists)
                {
                    context.AddFailure("Invalid user ID - the user does not exist.");
                }
            });
        }
    }
}
