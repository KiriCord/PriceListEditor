using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceListEditor.Application.Interfaces;

namespace PriceListEditor.Application.PriceLists.Commands.Update;

public class UpdateProductCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid IdPriceList { get; set; }
    public string? ProductName { get; set; }
    public int ProductCode { get; set; }
    
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IPriceListEditorDbContext _dbContext;

        public UpdateProductCommandHandler(IPriceListEditorDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(product => 
                product.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new Exception();
            }

            entity.ProductName = request.ProductName;
            entity.ProductCode = request.ProductCode;

            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}