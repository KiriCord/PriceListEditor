using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceListEditor.Application.Interfaces;
using PriceListEditor.Domain;

namespace PriceListEditor.Application.PriceLists.Commands.Create;

public class CreateProductCommand : IRequest<Guid>
{
    public Guid PriceListId { get; set; }
    public string ProductName { get; set; }
    public int ProductCode { get; set; }
    public List<Column> Columns { get; set; } 

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IPriceListEditorDbContext _dbContext;

        public CreateProductCommandHandler(IPriceListEditorDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.PriceLists.FirstOrDefaultAsync(x => x.Id == request.PriceListId);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                PriceList = entity,
                PriceListId = request.PriceListId,
                ProductName = request.ProductName,
                ProductCode = request.ProductCode
            };
            
            await _dbContext.Products.AddAsync(product, cancellationToken);
            
            foreach (var col in request.Columns)
            {
                var column = new Column()
                {
                    Name = col.Name,
                    Value = col.Value,
                    Type = col.Type
                };
                product.Columns.Add(column);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(createProductCommand => createProductCommand.PriceListId).NotEqual(Guid.Empty);
            RuleFor(createProductCommand => createProductCommand.ProductName).NotEmpty().MaximumLength(250);
            RuleFor(createProductCommand => createProductCommand.ProductCode).NotEmpty();
        }
    }
}