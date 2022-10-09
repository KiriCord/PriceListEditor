using AutoMapper;
using PriceListEditor.Application.Common.Mappings;
using PriceListEditor.Domain;

namespace PriceListEditor.Application.PriceLists.Queries.GetProductById;

public class ProductVm : IMapWith<Product>
{
    public Guid Id { get; set; }
    public Guid PriceListId { get; set; }
    public string? ProductName { get; set; }
    public int ProductCode { get; set; }
    public List<Column> Columns { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductVm>()
            .ForMember(priceListVm => priceListVm.Id,
                opt => opt.MapFrom(product => product.Id))
            .ForMember(priceListVm => priceListVm.PriceListId,
                opt => opt.MapFrom(product => product.PriceList.Id))
            .ForMember(priceListVm => priceListVm.ProductName,
                opt => opt.MapFrom(product => product.ProductName))
            .ForMember(priceListVm => priceListVm.ProductCode,
                opt => opt.MapFrom(product => product.ProductCode));
    }
}