using AutoMapper;
using PriceListEditor.Application.Common.Mappings;
using PriceListEditor.Domain;

namespace PriceListEditor.Application.PriceLists.Queries.GetPriceListDetails;

public class PriceListDetailsVm : IMapWith<Product>
{
    public Guid Id { get; set; }
    public string? ProductName { get; set; }
    public int ProductCode { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, PriceListDetailsVm>()
            .ForMember(priceListVm => priceListVm.Id,
                opt => opt.MapFrom(priceList => priceList.Id))
            .ForMember(priceListVm => priceListVm.ProductName,
                opt => opt.MapFrom(priceList => priceList.ProductName))
            .ForMember(priceListVm => priceListVm.ProductCode,
                opt => opt.MapFrom(priceList => priceList.ProductCode));
    }
}