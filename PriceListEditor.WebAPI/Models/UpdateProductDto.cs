using AutoMapper;
using PriceListEditor.Application.Common.Mappings;
using PriceListEditor.Application.PriceLists.Commands.Update;
using PriceListEditor.Domain;

namespace PriceListEditor.WebAPI.Models;

public class UpdateProductDto : IMapWith<UpdateProductCommand>
{
    public Guid Id { get; set; }
    public string? ProductName { get; set; }
    public int ProductCode { get; set; }
    public List<Column> Columns { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateProductDto, UpdateProductCommand>()
            .ForMember(productCommand => productCommand.Id,
                opt =>
                    opt.MapFrom(priceListDto => priceListDto.Id))
            .ForMember(productCommand => productCommand.ProductName,
                opt =>
                    opt.MapFrom(priceListDto => priceListDto.ProductName))
            .ForMember(productCommand => productCommand.ProductCode,
                opt =>
                    opt.MapFrom(productCommand => productCommand.ProductCode))
            .ForMember(productCommand => productCommand.Columns,
            opt =>
                opt.MapFrom(productCommand => productCommand.Columns));
    }
}