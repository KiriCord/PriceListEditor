using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceListEditor.Application.Interfaces;

namespace PriceListEditor.Application.PriceLists.Queries.GetPriceListDetails;

public class GetPriceListDetailsQuery : IRequest<DetailsListVm>
{
    public Guid Id { get; set; }

    public class GetPriceListDetailsQueryHandler 
        : IRequestHandler<GetPriceListDetailsQuery, DetailsListVm>
    {
        private readonly IPriceListEditorDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPriceListDetailsQueryHandler(IPriceListEditorDbContext dbContext, IMapper mapper) => 
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<DetailsListVm> Handle(GetPriceListDetailsQuery request, 
            CancellationToken cancellationToken)
        {
            var productsQuery = await _dbContext.Products
                .Where(product => product.PriceList.Id == request.Id)
                .ProjectTo<PriceListDetailsVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            

            return new DetailsListVm() { Products = productsQuery};
        }
    }

    public class GetPriceListDetailsQueryValidator : AbstractValidator<GetPriceListDetailsQuery>
    {
        public GetPriceListDetailsQueryValidator()
        {
            RuleFor(x=> x.Id).NotEqual(Guid.Empty);
        }
    }
}