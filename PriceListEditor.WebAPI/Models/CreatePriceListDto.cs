using AutoMapper;
using PriceListEditor.Application.Common.Mappings;
using PriceListEditor.Application.PriceLists.Commands.Create;

namespace PriceListEditor.WebAPI.Models;

public class CreatePriceListDto : IMapWith<CreatePriceListCommand>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreatePriceListDto, CreatePriceListCommand>()
            .ForMember(priceListCommand => priceListCommand.Id,
                opt =>
                    opt.MapFrom(priceListDto => priceListDto.Id))
            .ForMember(priceListCommand => priceListCommand.Name,
                opt =>
                    opt.MapFrom(priceListDto => priceListDto.Name));
    }
}