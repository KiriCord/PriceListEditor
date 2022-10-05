using AutoMapper;
using PriceListEditor.Application.Common.Mappings;
using PriceListEditor.Application.PriceLists.Commands.Create;

namespace PriceListEditor.WebAPI.Models;

public class CreateProductDto : IMapWith<CreateProductCommand>
{
    public Guid IdPriceList { get; set; }
    public string? ProductName { get; set; }
    public int ProductCode { get; set; }
    // public string ReturnUrl { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateProductDto, CreateProductCommand>()
            .ForMember(productCommand => productCommand.IdPriceList,
                opt =>
                    opt.MapFrom(productDto => productDto.IdPriceList))
            .ForMember(productCommand => productCommand.ProductName,
                opt =>
                    opt.MapFrom(productDto => productDto.ProductName))
            .ForMember(productCommand => productCommand.ProductCode,
                opt =>
                    opt.MapFrom(productDto => productDto.ProductCode));
    }
}