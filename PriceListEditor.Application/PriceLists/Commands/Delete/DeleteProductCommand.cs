using FluentValidation;
using MediatR;
using PriceListEditor.Application.Interfaces;

namespace PriceListEditor.Application.PriceLists.Commands.Delete;

public class DeleteProductCommand : IRequest
{
    public Guid Id { get; set; }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IPriceListEditorDbContext _dbContext;

        public DeleteProductCommandHandler(IPriceListEditorDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Products
                .FindAsync(new object[] { request.Id }, cancellationToken);
            

            if (entity == null)
            {
                throw new Exception();
            }

            _dbContext.Products.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(deleteProductCommand => deleteProductCommand.Id).NotEqual(Guid.Empty);
        }
    }
}