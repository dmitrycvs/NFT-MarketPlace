using FluentValidation;
using MediatR;
using NFT.Core.Entities;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.Collections;

namespace NFT.UseCases.Collections.Commands;

public class CreateCollectionCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public DateTime DateTime { get; set; }
    public decimal FloorPrice { get; set; }
    public decimal Volume { get; set; } //oborot
    public int Supply {  get; set; } //total number of nfts in deal
    public int NumberOfSale { get; set; }
    public decimal MarketCapital { get; set; }
    
    public string SocialLink { get; set; } = string.Empty;

    public CreateCollectionCommand(CollectionDto CollectionDto)
    {
        Name = CollectionDto.Name;
        DateTime = CollectionDto.DateTime;
        FloorPrice = CollectionDto.FloorPrice;
        Volume = CollectionDto.Volume;
        Supply = CollectionDto.Supply;
        NumberOfSale = CollectionDto.NumberOfSale;
        MarketCapital = CollectionDto.MarketCapital;
        SocialLink = CollectionDto.SocialLink;
    }
}

class CreateCollectionCommandHandler : IRequestHandler<CreateCollectionCommand, Guid>
{
    private readonly AppDbContext _appDbContext;

    public CreateCollectionCommandHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Guid> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
    {
        var CollectionToAdd = new Collection
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            DateTime = request.DateTime,
            FloorPrice = request.FloorPrice,
            Volume = request.Volume,
            Supply = request.Supply,
            NumberOfSale = request.NumberOfSale,
            MarketCapital = request.MarketCapital,
            SocialLink = request.SocialLink
        };
        _appDbContext.Collections.Add(CollectionToAdd);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        
        return CollectionToAdd.Id;
    }
}

public class CreateCollectionCommandValidator : AbstractValidator<CreateCollectionCommand>
{
    public CreateCollectionCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.DateTime).NotEmpty().WithMessage("DateTime is required");
        RuleFor(x => x.FloorPrice).NotEmpty().WithMessage("FloorPrice is required");
        RuleFor(x => x.Volume).NotEmpty().WithMessage("Volume is required");
        RuleFor(x => x.Supply).NotEmpty().WithMessage("Supply is required");
        RuleFor(x => x.NumberOfSale).NotEmpty().WithMessage("NumberOfSale is required");
        RuleFor(x => x.MarketCapital).NotEmpty().WithMessage("MarketCapital is required");
        RuleFor(x => x.SocialLink).NotEmpty().WithMessage("SocialLink is required");
    }
}