using AutoMapper;
using PriceListEditor.Application.Common.Mappings;
using PriceListEditor.Application.PriceLists.Commands.Create;

namespace PriceListEditor.WebAPI.Models;

public class CreatePriceListDto : IMapWith<CreatePriceListCommand>
{
    public string? Name { get; set; }
    public ICollection<CreateProductDto> Products;

    public CreatePriceListDto() =>
        Products = new List<CreateProductDto>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreatePriceListDto, CreatePriceListCommand>()
            .ForMember(priceListCommand => priceListCommand.Name,
                opt =>
                    opt.MapFrom(priceListDto => priceListDto.Name))
            .ForMember(priceListCommand => priceListCommand.Products,
                opt =>
                    opt.MapFrom(priceListDto => priceListDto.Products));
    }
}