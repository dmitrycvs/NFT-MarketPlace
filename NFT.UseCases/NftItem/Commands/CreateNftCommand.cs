using FluentValidation;
using MediatR;
using NFT.Infrastructure;
using NFT.Shared.DataTransferObjects.NFT;

namespace NFT.UseCases.Nft.Commands;

public class CreateNftCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string Name { get; set; } 
    public string Hash { get; set; }
    public decimal Price { get; set; }
    public bool IsListed { get; set; }
    public Guid CollectionId { get; set; }


    public CreateNftCommand(NftItemDto nftItemDto)
    {
        UserId = nftItemDto.UserId;
        Name = nftItemDto.Name;
        Hash = nftItemDto.Hash;
        Price = nftItemDto.Price;
        IsListed = nftItemDto.IsListed;
        CollectionId = nftItemDto.CollectionId;
    }
}

public class CreateNftCommandHandler : IRequestHandler<CreateNftCommand, Guid>
{
    private readonly AppDbContext _appDbContext;

    public CreateNftCommandHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Guid> Handle(CreateNftCommand request, CancellationToken cancellationToken)
    {
        var nftToAdd = new Core.Entities.NftItem
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Hash = request.Hash,
            Price = request.Price,
            IsListed = request.IsListed,
            CollectionId = request.CollectionId,
        };
        _appDbContext.NftItems.Add(nftToAdd);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return nftToAdd.Id;
    }
}

public class CreateNftCommandValidator : AbstractValidator<CreateNftCommand>
{
    public CreateNftCommandValidator()
    {
        RuleFor(n => n.UserId).NotEmpty().WithMessage("UserId cannot be empty");
        RuleFor(n => n.Hash).NotEmpty().WithMessage("Hash cannot be empty");
        RuleFor(n => n.Price).NotEmpty().WithMessage("Price cannot be empty");
        RuleFor(n => n.CollectionId).NotEmpty().WithMessage("CollectionId cannot be empty");

    }
}