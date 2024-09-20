using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.HistoryLogs;

namespace NFT.UseCases.HistoryLogs.Queries;

public class GetHistoryLogByIdQuery : IRequest<HistoryLogDto>
{
    public Guid Id { get; set; }

    public GetHistoryLogByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetHistoryLogByIdQueryHandler : IRequestHandler<GetHistoryLogByIdQuery, HistoryLogDto>
{
    private readonly AppDbContext _appDbContext;

    public GetHistoryLogByIdQueryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<HistoryLogDto> Handle(GetHistoryLogByIdQuery request, CancellationToken cancellationToken)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return await _appDbContext.HistoryLogs
            .Where(i => i.Id == request.Id)
            .Select(HistoryLog => new HistoryLogDto
            {
                Id = HistoryLog.Id, 
                NftItemId = HistoryLog.NftItemId, 
                DateTime = HistoryLog.DateTime, 
                DealPrice = HistoryLog.DealPrice, 
                BuyerId = HistoryLog.BuyerId, 
                SellerId = HistoryLog.SellerId
            })
            .FirstOrDefaultAsync(cancellationToken);
#pragma warning restore CS8603 // Possible null reference return.
    }
}

public class GetHistoryLogByIdQueryValidator : AbstractValidator<GetHistoryLogByIdQuery>
{
    public GetHistoryLogByIdQueryValidator(IServiceProvider services)
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");

        RuleFor(x => x).Custom((query, context) =>
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var HistoryLogExist = dbContext.HistoryLogs.Any(i => i.Id == query.Id);
            if (!HistoryLogExist)
            {
                context.AddFailure("HistoryLog not found");
            }
        });
    }
}