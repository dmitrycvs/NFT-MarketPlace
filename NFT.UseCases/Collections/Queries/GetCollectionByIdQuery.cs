using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.Collections;

namespace NFT.UseCases.Collections.Queries;

public class GetCollectionByIdQuery : IRequest<CollectionDto>
{
    public Guid Id { get; set; }

    public GetCollectionByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetCollectionByIdQueryHandler : IRequestHandler<GetCollectionByIdQuery, CollectionDto>
{
    private readonly AppDbContext _appDbContext;

    public GetCollectionByIdQueryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<CollectionDto> Handle(GetCollectionByIdQuery request, CancellationToken cancellationToken)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return await _appDbContext.Collections
            .Where(i => i.Id == request.Id)
            .Select(Collection => new CollectionDto
            {
                Id = Collection.Id, 
                Name = Collection.Name, 
                DateTime = Collection.DateTime, 
                Supply = Collection.Supply, 
                FloorPrice = Collection.FloorPrice, 
                MarketCapital = Collection.MarketCapital, 
                NumberOfSale = Collection.NumberOfSale, 
                SocialLink = Collection.SocialLink, 
                Volume = Collection.Volume
            })
            .FirstOrDefaultAsync(cancellationToken);
#pragma warning restore CS8603 // Possible null reference return.
    }
}

public class GetCollectionByIdQueryValidator : AbstractValidator<GetCollectionByIdQuery>
{
    public GetCollectionByIdQueryValidator(IServiceProvider services)
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");

        RuleFor(x => x).Custom((query, context) =>
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var CollectionExist = dbContext.Collections.Any(i => i.Id == query.Id);
            if (!CollectionExist)
            {
                context.AddFailure("Collection not found");
            }
        });
    }
}