using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.HistoryLogs;

namespace NFT.UseCases.HistoryLogs.Commands;

public class EditHistoryLogCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public Guid SellerId { get; set; }
        public Guid BuyerId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal DealPrice { get; set; }
        public Guid NftItemId { get; set; }


        public EditHistoryLogCommand(HistoryLogDto historyLogDto)
        {
            Id = historyLogDto.Id;
            SellerId = historyLogDto.SellerId;
            BuyerId = historyLogDto.BuyerId;
            DateTime = historyLogDto.DateTime;
            DealPrice = historyLogDto.DealPrice;
            NftItemId = historyLogDto.NftItemId;
        }
    }

    public class EditHistoryLogCommandHandler : IRequestHandler<EditHistoryLogCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public EditHistoryLogCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(EditHistoryLogCommand request, CancellationToken cancellationToken)
        {
            var historyLogToEdit = await _appDbContext.HistoryLogs
                .Where(n => n.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (historyLogToEdit == null)
            {
                throw new InvalidOperationException("HistoryLog not found.");
            }

            if (request.SellerId != Guid.Empty)
            {
                historyLogToEdit.SellerId = request.SellerId;
            }

            if (request.BuyerId != Guid.Empty)
            {
                historyLogToEdit.BuyerId = request.BuyerId;
            }

            if (request.DateTime != null)
            {
                historyLogToEdit.DateTime = request.DateTime;
            }

            if (request.NftItemId != Guid.Empty)
            {
                historyLogToEdit.NftItemId = request.NftItemId;
            }

            if (request.DealPrice != null)
            {
                historyLogToEdit.DealPrice = request.DealPrice;
            }


            await _appDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class EditHistoryLogCommandValidator : AbstractValidator<EditHistoryLogCommand>
    {
        public EditHistoryLogCommandValidator(IServiceProvider services)
        {
            RuleFor(x => x.SellerId).NotEmpty().WithMessage("You must provide user seller id.");
            RuleFor(x => x.BuyerId).NotEmpty().WithMessage("You must provide user buyer.");
            RuleFor(x => x.DateTime).NotEmpty().WithMessage("You must provide date time.");
            RuleFor(x => x.DealPrice).NotEmpty().WithMessage("You must provide deal price.");
            RuleFor(x => x.NftItemId).NotEmpty().WithMessage("You must provide nft item id.");

            RuleFor(n => n).Custom((command, context) =>
            {
                using var scope = services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var historyLogExists = dbContext.HistoryLogs.Any(n => n.Id == command.Id);
                if (!historyLogExists)
                {
                    context.AddFailure("The historyLog does not exist.");
                }
            });
        }
    }