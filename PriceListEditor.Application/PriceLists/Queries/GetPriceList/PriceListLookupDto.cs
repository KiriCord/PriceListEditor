using AutoMapper;
using PriceListEditor.Application.Common.Mappings;
using PriceListEditor.Domain;

namespace PriceListEditor.Application.PriceLists.Queries.GetPriceList;

public class PriceListLookupDto : IMapWith<PriceList>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PriceList, PriceListLookupDto>()
            .ForMember(priceListDto => priceListDto.Id,
                opt => opt.MapFrom(priceList => priceList.Id))
            .ForMember(priceListDto => priceListDto.Name,
                opt => opt.MapFrom(priceList => priceList.Name));

    }
}