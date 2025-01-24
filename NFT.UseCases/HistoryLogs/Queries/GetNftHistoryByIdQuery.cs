using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.HistoryLogs;

namespace NFT.UseCases.HistoryLogs.Queries;

public class GetNftHistoryLogsByNftIdQuery : IRequest<List<HistoryLogDto>>
{
    public Guid NftItemId { get; set; }

    public GetNftHistoryLogsByNftIdQuery(Guid nftItemId)
    {
        NftItemId = nftItemId;
    }
}

public class GetNftHistoryLogsByNftIdQueryHandler : IRequestHandler<GetNftHistoryLogsByNftIdQuery, List<HistoryLogDto>>
{
    private readonly AppDbContext _appDbContext;

    public GetNftHistoryLogsByNftIdQueryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<HistoryLogDto>> Handle(GetNftHistoryLogsByNftIdQuery request, CancellationToken cancellationToken)
    {
        return await _appDbContext.HistoryLogs
            .Where(log => log.NftItemId == request.NftItemId)
            .Select(log => new HistoryLogDto
            {
                Id = log.Id,
                NftItemId = log.NftItemId,
                DateTime = log.DateTime,
                DealPrice = log.DealPrice,
                BuyerId = log.BuyerId,
                SellerId = log.SellerId
            })
            .ToListAsync(cancellationToken);
    }
}

public class GetNftHistoryLogsByNftIdQueryValidator : AbstractValidator<GetNftHistoryLogsByNftIdQuery>
{
    public GetNftHistoryLogsByNftIdQueryValidator(IServiceProvider services)
    {
        RuleFor(x => x.NftItemId).NotEmpty().WithMessage("NftItemId cannot be empty");

        RuleFor(x => x).Custom((query, context) =>
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var nftExists = dbContext.HistoryLogs.Any(log => log.NftItemId == query.NftItemId);
            if (!nftExists)
            {
                context.AddFailure("No history logs found for the given NftItemId");
            }
        });
    }
}
