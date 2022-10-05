using AutoMapper;
using PriceListEditor.Application.Common.Mappings;
using PriceListEditor.Application.PriceLists.Commands.Update;

namespace PriceListEditor.WebAPI.Models;

public class UpdatePriceListDto : IMapWith<UpdatePriceListCommand>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdatePriceListDto, UpdatePriceListCommand>()
            .ForMember(priceListCommand => priceListCommand.Id,
                opt =>
                    opt.MapFrom(priceListDto => priceListDto.Id))
            .ForMember(priceListCommand => priceListCommand.Name,
                opt =>
                    opt.MapFrom(priceListDto => priceListDto.Name));
    }

}