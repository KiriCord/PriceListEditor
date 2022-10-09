using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceListEditor.Application.Interfaces;

namespace PriceListEditor.Application.PriceLists.Queries.GetProductById;

public class GetProductByIdQueries : IRequest<ProductListByIdVm>
{
    public Guid Id { get; set; }

    public class GetProductByIdQueriesHandler 
        : IRequestHandler<GetProductByIdQueries, ProductListByIdVm>
    {
        private readonly IPriceListEditorDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public GetProductByIdQueriesHandler(IPriceListEditorDbContext dbContext, IMapper mapper) => 
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<ProductListByIdVm> Handle(GetProductByIdQueries request,
            CancellationToken cancellationToken)
        {
            var productQuery = await _dbContext.Products
                .Where(product => product.Id == request.Id)
                .ProjectTo<ProductVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ProductListByIdVm { Products = productQuery };
        }
    }
    
    public class GetProductByIdQueriesValidator : AbstractValidator<GetProductByIdQueries>
    {
        public GetProductByIdQueriesValidator()
        {
            RuleFor(x=> x.Id).NotEqual(Guid.Empty);
        }
    }
}