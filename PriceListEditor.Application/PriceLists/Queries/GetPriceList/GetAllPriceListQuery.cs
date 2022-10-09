using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceListEditor.Application.Interfaces;

namespace PriceListEditor.Application.PriceLists.Queries.GetPriceList;

public class GetAllPriceListQuery : IRequest<PriceListVm>
{
    public class GetAllPriceListQueryHandler : IRequestHandler<GetAllPriceListQuery, PriceListVm>
    {
        private readonly IPriceListEditorDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllPriceListQueryHandler(IPriceListEditorDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<PriceListVm> Handle(GetAllPriceListQuery request,
            CancellationToken cancellationToken)
        {
            var priceList = await _dbContext.PriceLists
                .ProjectTo<PriceListLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (priceList == null)
            {
                return null;
            }

            return new PriceListVm {PriceLists = priceList};
        }
    }

    public class GetAllPriceListQueryValidator : AbstractValidator<GetAllPriceListQuery>
    {
        public GetAllPriceListQueryValidator()
        {
        }
    }
}