using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PriceListEditor.Application.PriceLists.Queries.GetPriceList;

namespace PriceListEditor.WebAPI.Controllers;

public class PriceListController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public PriceListController(IMapper mapper, IMediator mediator) => 
        (_mediator, _mapper) = (mediator, mapper);

    public async Task<IActionResult> GetAll()
    {
        //return View(await _mediator.Send(new GetAllPriceListQuery()));
    }
}