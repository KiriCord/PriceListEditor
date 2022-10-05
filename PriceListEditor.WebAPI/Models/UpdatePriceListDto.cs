using AutoMapper;
using PriceListEditor.Application.Common.Mappings;
using PriceListEditor.Application.PriceLists.Commands.Update;

namespace PriceListEditor.WebAPI.Models;

public class UpdatePriceListDto : IMapWith<UpdatePriceListCommand>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public ICollection<UpdateProductDto> Products;

    public UpdatePriceListDto() =>
        Products = new List<UpdateProductDto>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdatePriceListDto, UpdatePriceListCommand>()
            .ForMember(priceListCommand => priceListCommand.Id,
                opt =>
                    opt.MapFrom(priceListDtro => priceListDtro.Id))
            .ForMember(priceListCommand => priceListCommand.Name,
                opt =>
                    opt.MapFrom(priceListDtro => priceListDtro.Name))
            .ForMember(priceListCommand => priceListCommand.Products,
                opt =>
                    opt.MapFrom(priceListDtro => priceListDtro.Products));
    }

}