using FluentValidation;
using MediatR;
using NFT.Core.Entities;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.HistoryLogs;

namespace NFT.UseCases.HistoryLogs.Commands;

public class CreateHistoryLogCommand : IRequest<Guid>
{
    public Guid SellerId { get; set; }
    public Guid BuyerId { get; set; }
    public DateTime DateTime { get; set; }
    public decimal DealPrice { get; set; }
    public Guid NftItemId { get; set; }

    public CreateHistoryLogCommand(HistoryLogDto historyLogDto)
    {
        SellerId = historyLogDto.SellerId;
        BuyerId = historyLogDto.BuyerId;
        DateTime = historyLogDto.DateTime;
        DealPrice = historyLogDto.DealPrice;
        NftItemId = historyLogDto.NftItemId;
    }
}

class CreateHistoryLogCommandHandler : IRequestHandler<CreateHistoryLogCommand, Guid>
{
    private readonly AppDbContext _appDbContext;

    public CreateHistoryLogCommandHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Guid> Handle(CreateHistoryLogCommand request, CancellationToken cancellationToken)
    {
        var historyLogToAdd = new HistoryLog
        {
            Id = Guid.NewGuid(),
            SellerId = request.SellerId,
            BuyerId = request.BuyerId,
            DateTime = request.DateTime,
            DealPrice = request.DealPrice,
            NftItemId = request.NftItemId
        };
        _appDbContext.HistoryLogs.Add(historyLogToAdd);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        
        return historyLogToAdd.Id;
    }
}

public class CreateHistoryLogCommandValidator : AbstractValidator<CreateHistoryLogCommand>
{
    public CreateHistoryLogCommandValidator()
    {
        RuleFor(x => x.SellerId).NotEmpty().WithMessage("You must provide user seller id.");
        RuleFor(x => x.BuyerId).NotEmpty().WithMessage("You must provide user buyer.");
        RuleFor(x => x.DateTime).NotEmpty().WithMessage("You must provide date time.");
        RuleFor(x => x.DealPrice).NotEmpty().WithMessage("You must provide deal price.");
        RuleFor(x => x.NftItemId).NotEmpty().WithMessage("You must provide nft item id.");
    }
}
