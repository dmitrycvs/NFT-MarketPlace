using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFT.Infrastructure;

namespace NFT.UseCases.HistoryLogs.Commands;

public class DeleteHistoryLogCommand : IRequest<Unit>
{
    public Guid Id { get; set; }   
}

public class DeleteHistoryLogCommandHandler : IRequestHandler<DeleteHistoryLogCommand, Unit>
{
    private readonly AppDbContext _appDbContext;

    public DeleteHistoryLogCommandHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Unit> Handle(DeleteHistoryLogCommand request, CancellationToken cancellationToken)
    {
        var historyLogToDelete = await _appDbContext.HistoryLogs
            .Where(i => i.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (historyLogToDelete == null)
        {
            throw new InvalidOperationException("HistoryLog not found");
        }
        
        _appDbContext.HistoryLogs.Remove(historyLogToDelete);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}

public class DeleteHistoryLogCommandValidator : AbstractValidator<DeleteHistoryLogCommand>
{
    public DeleteHistoryLogCommandValidator(IServiceProvider services)
    {
        RuleFor(i => i.Id).NotEmpty().WithMessage("Id cannot be empty");

        RuleFor(i => i).Custom((command, context) =>
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var HistoryLogExists = dbContext.HistoryLogs.Any(i => i.Id == command.Id);
            if (!HistoryLogExists)
            {
                context.AddFailure("Invalid HistoryLog ID - the HistoryLog does not exist");
            }
        });
    }
}