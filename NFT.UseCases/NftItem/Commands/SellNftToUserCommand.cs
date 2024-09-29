using MediatR;
using NFT.Infrastructure;
using NFT.Core.Entities;
using Microsoft.EntityFrameworkCore;
using FluentValidation;


namespace NFT.UseCases.Nft.Commands;

public class SellNftToUserCommand : IRequest<Guid>
{
    public Guid SellerId { get; set; }
    public Guid BuyerId { get; set; }
    public Guid NftItemId { get; set; }
    public decimal DealPrice { get; set; }
}

public class SellNftToUserCommandHandler : IRequestHandler<SellNftToUserCommand, Guid>
{
    private readonly AppDbContext _dbContext;

    public SellNftToUserCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(SellNftToUserCommand request, CancellationToken cancellationToken)
    {
        var nftItem = await _dbContext.NftItems
            .FirstOrDefaultAsync(nft => nft.Id == request.NftItemId && nft.UserId == request.SellerId, cancellationToken);

        if (nftItem == null)
            throw new InvalidOperationException("NFT not found or not owned by the seller.");

        if (!nftItem.IsListed)
            throw new InvalidOperationException("NFT is not listed for sale.");

        nftItem.UserId = request.BuyerId;

        var historyLog = new HistoryLog
        {
            Id = Guid.NewGuid(),
            SellerId = request.SellerId,
            BuyerId = request.BuyerId,
            DateTime = DateTime.UtcNow, 
            DealPrice = request.DealPrice,
            NftItemId = request.NftItemId
        };
        _dbContext.HistoryLogs.Add(historyLog);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return historyLog.Id;
    }

}
public class SellNftToUserCommandValidator : AbstractValidator<SellNftToUserCommand>
{
    public SellNftToUserCommandValidator()
    {
        RuleFor(cmd => cmd.SellerId).NotEmpty().WithMessage("Seller ID is required.");
        RuleFor(cmd => cmd.BuyerId).NotEmpty().WithMessage("Buyer ID is required.");
        RuleFor(cmd => cmd.NftItemId).NotEmpty().WithMessage("NFT Item ID is required.");
        RuleFor(cmd => cmd.DealPrice).GreaterThan(0).WithMessage("Deal price must be greater than zero.");
    }
}


