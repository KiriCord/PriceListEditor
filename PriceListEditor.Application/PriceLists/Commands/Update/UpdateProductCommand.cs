using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoreLinq.Extensions;
using PriceListEditor.Application.Interfaces;
using PriceListEditor.Domain;

namespace PriceListEditor.Application.PriceLists.Commands.Update;

public class UpdateProductCommand : IRequest
{
    public Guid Id { get; set; }
    public string? ProductName { get; set; }
    public int ProductCode { get; set; }
    public List<Column> Columns { get; set; }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IPriceListEditorDbContext _dbContext;

        public UpdateProductCommandHandler(IPriceListEditorDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Products.Include(c => c.Columns).FirstOrDefaultAsync(product => 
                product.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new Exception();
            }

            entity.ProductName = request.ProductName;
            entity.ProductCode = request.ProductCode;
            foreach (var (index, ent) in entity.Columns.Index())
            {
                ent.Value = request.Columns[index].Value;
            }
            
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(updProductCommand => updProductCommand.Id).NotEqual(Guid.Empty);
            RuleFor(updProductCommand => updProductCommand.ProductName).NotEmpty().MaximumLength(250);
            RuleFor(updProductCommand => updProductCommand.ProductCode).NotEmpty();
            RuleFor(updProductCommand => updProductCommand.Columns).NotNull();
        }
    }
}