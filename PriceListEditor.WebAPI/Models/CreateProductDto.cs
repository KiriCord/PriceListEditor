using AutoMapper;
using PriceListEditor.Application.Common.Mappings;
using PriceListEditor.Application.PriceLists.Commands.Create;
using PriceListEditor.Domain;

namespace PriceListEditor.WebAPI.Models;

public class CreateProductDto : IMapWith<CreateProductCommand>
{
    public Guid PriceListId { get; set; }
    public string ProductName { get; set; }
    public int ProductCode { get; set; }
    public List<Column> Columns { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateProductDto, CreateProductCommand>()
            .ForMember(productCommand => productCommand.PriceListId,
                opt =>
                    opt.MapFrom(productDto => productDto.PriceListId))
            .ForMember(productCommand => productCommand.ProductName,
                opt =>
                    opt.MapFrom(productDto => productDto.ProductName))
            .ForMember(productCommand => productCommand.ProductCode,
                opt =>
                    opt.MapFrom(productDto => productDto.ProductCode))
            .ForMember(productCommand => productCommand.Columns,
            opt =>
                opt.MapFrom(productDto => productDto.Columns));
    }
}