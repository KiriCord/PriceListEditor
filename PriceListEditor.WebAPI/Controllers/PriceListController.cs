using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PriceListEditor.Application.PriceLists.Commands.Create;
using PriceListEditor.Application.PriceLists.Commands.Delete;
using PriceListEditor.Application.PriceLists.Commands.Update;
using PriceListEditor.Application.PriceLists.Queries.GetPriceList;
using PriceListEditor.Application.PriceLists.Queries.GetPriceListDetails;
using PriceListEditor.WebAPI.Models;

namespace PriceListEditor.WebAPI.Controllers;

public class PriceListController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public PriceListController(IMapper mapper, IMediator mediator) => 
        (_mediator, _mapper) = (mediator, mapper);

    [HttpGet]
    public async Task<IActionResult> All()
    {
        return View(await _mediator.Send(new GetAllPriceListQuery()));
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreatePriceListDto model)
    {
        var command = _mapper.Map<CreatePriceListCommand>(model);
        await _mediator.Send(command);
        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public IActionResult Edit() => View();

    [HttpPost]
    public async Task<IActionResult> Edit(UpdatePriceListDto model)
    {
        var command = _mapper.Map<UpdatePriceListCommand>(model);
        await _mediator.Send(command);
        return RedirectToAction(nameof(All));
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeletePriceListCommand {Id = id});
        return RedirectToAction(nameof(All));
    }
}