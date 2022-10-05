using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceListEditor.Application.Interfaces;
using PriceListEditor.Domain;

namespace PriceListEditor.Application.PriceLists.Commands.Update;

public class UpdatePriceListCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Product> Products { get; set; }

    public class UpdatePriceListCommandHandler : IRequestHandler<UpdatePriceListCommand>
    {
        private readonly IPriceListEditorDbContext _dbContext;

        public UpdatePriceListCommandHandler(IPriceListEditorDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdatePriceListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.PriceLists.FirstOrDefaultAsync(priceList => 
                priceList.Id == request.Id, cancellationToken);

            if (entity == null || entity.Id != request.Id)
            {
                throw new Exception();
            }

            entity.Name = request.Name;
            entity.Products = request.Products;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}