using FluentValidation;
using MediatR;
using PriceListEditor.Application.Interfaces;
using PriceListEditor.Domain;

namespace PriceListEditor.Application.PriceLists.Commands.Create;

public class CreateProductCommand : IRequest<Guid>
{
    public Guid IdPriceList { get; set; }
    public string? ProductName { get; set; }
    public int ProductCode { get; set; }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IPriceListEditorDbContext _dbContext;

        public CreateProductCommandHandler(IPriceListEditorDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                IdPriceList = request.IdPriceList,
                ProductName = request.ProductName,
                ProductCode = request.ProductCode
            };

            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(createProductCommand => createProductCommand.IdPriceList).NotEqual(Guid.Empty);
            RuleFor(createProductCommand => createProductCommand.ProductName).NotEmpty().MaximumLength(250);
            RuleFor(createProductCommand => createProductCommand.ProductCode).NotEmpty();
        }
    }
}