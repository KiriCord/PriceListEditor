using FluentValidation;
using MediatR;
using PriceListEditor.Application.Interfaces;

namespace PriceListEditor.Application.PriceLists.Commands.Delete;

public class DeletePriceListCommand : IRequest
{
    public Guid Id { get; set; }

    public class DeletePriceListCommandHandler : IRequestHandler<DeletePriceListCommand>
    {
        private readonly IPriceListEditorDbContext _dbContext;

        public DeletePriceListCommandHandler(IPriceListEditorDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeletePriceListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.PriceLists
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new Exception();
            }

            _dbContext.PriceLists.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }

    public class DeletePriceListCommandValidator : AbstractValidator<DeletePriceListCommand>
    {
        public DeletePriceListCommandValidator()
        {
            RuleFor(deletePriceListCommand => deletePriceListCommand.Id).NotEqual(Guid.Empty);
        }
    }
}