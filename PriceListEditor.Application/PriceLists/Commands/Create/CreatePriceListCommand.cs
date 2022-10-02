using MediatR;
using PriceListEditor.Application.Interfaces;
using PriceListEditor.Domain;

namespace PriceListEditor.Application.PriceLists.Commands.Create;

public class CreatePriceListCommand : IRequest<Guid>
{
    public string? Name { get; set; }
    public ICollection<Product> Products;

    public CreatePriceListCommand() =>
        Products = new List<Product>();

    public class CreatePriceListCommandHandler : IRequestHandler<CreatePriceListCommand, Guid>
    {
        private readonly IPriceListEditorDbContext _dbContext;

        public CreatePriceListCommandHandler(IPriceListEditorDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreatePriceListCommand request, 
            CancellationToken cancellationToken)
        {
            //var priceCollection = request.Products;
            
            var priceList = new PriceList
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Products = request.Products
            };
            
            await _dbContext.PriceLists.AddAsync(priceList, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return priceList.Id;
        }
    }
}