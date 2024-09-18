using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.NFT;

namespace NFT.UseCases.Nft.Queries;

public class GetNftByIdQuery : IRequest<NftDto>
{
    public Guid Id { get; set; }

    public GetNftByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetNftByIdQueryHandler : IRequestHandler<GetNftByIdQuery, NftDto>
{
    private readonly AppDbContext _appDbContext;

    public GetNftByIdQueryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<NftDto> Handle(GetNftByIdQuery request, CancellationToken cancellationToken)
    {
        return await _appDbContext.Nfts
            .Where(n => n.Id == request.Id)
            .Select(Nft => new NftDto {Id = Nft.Id, UserId = Nft.UserId, Hash = Nft.Hash, Price = Nft.Price})
            .FirstOrDefaultAsync(cancellationToken);
    }
}

public class GetNftByIdQueryValidator : AbstractValidator<GetNftByIdQuery>
{
    public GetNftByIdQueryValidator(IServiceProvider services)
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");

        RuleFor(x => x).Custom((query, context) =>
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var NftExist = dbContext.Nfts.Any(n => n.Id == query.Id);
            if (!NftExist)
            {
                context.AddFailure("Nft not found");
            }
        });
    }
}