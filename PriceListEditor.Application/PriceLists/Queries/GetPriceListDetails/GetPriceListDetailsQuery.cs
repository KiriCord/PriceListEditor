using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceListEditor.Application.Interfaces;

namespace PriceListEditor.Application.PriceLists.Queries.GetPriceListDetails;

public class GetPriceListDetailsQuery : IRequest<PriceListDetailsVm>
{
    public Guid Id { get; set; }
    public Guid IdPriceList { get; set; }

    public class GetPriceListDetailsQueryHandler 
        : IRequestHandler<GetPriceListDetailsQuery, PriceListDetailsVm>
    {
        private readonly IPriceListEditorDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPriceListDetailsQueryHandler(IPriceListEditorDbContext dbContext, IMapper mapper) => 
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<PriceListDetailsVm> Handle(GetPriceListDetailsQuery request, 
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Products.
                FirstOrDefaultAsync(product => 
                    product.Id == request.Id, cancellationToken);

            if (entity == null || entity.IdPriceList != request.IdPriceList)
            {
                throw new Exception();
            }

            return _mapper.Map<PriceListDetailsVm>(entity);
        }
    }
}