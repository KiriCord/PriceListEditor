using FluentValidation;
using MediatR;
using PriceListEditor.Application.Interfaces;
using PriceListEditor.Domain;

namespace PriceListEditor.Application.PriceLists.Commands.Create;

public class CreatePriceListCommand : IRequest<Guid>
{
    public string? Name { get; set; }

    public class CreatePriceListCommandHandler : IRequestHandler<CreatePriceListCommand, Guid>
    {
        private readonly IPriceListEditorDbContext _dbContext;

        public CreatePriceListCommandHandler(IPriceListEditorDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreatePriceListCommand request, 
            CancellationToken cancellationToken)
        {
            var priceList = new PriceList
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
            };
            
            await _dbContext.PriceLists.AddAsync(priceList, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return priceList.Id;
        }
    }

    public class CreatePriceListCommandValidator : AbstractValidator<CreatePriceListCommand>
    {
        public CreatePriceListCommandValidator()
        {
            RuleFor(createPriceListCommand => createPriceListCommand.Name).NotEmpty().MaximumLength(250);
        }
    }
}