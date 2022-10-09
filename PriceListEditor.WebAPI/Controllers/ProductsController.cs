﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PriceListEditor.Application.PriceLists.Commands.Create;
using PriceListEditor.Application.PriceLists.Commands.Delete;
using PriceListEditor.Application.PriceLists.Commands.Update;
using PriceListEditor.Application.PriceLists.Queries.GetPriceListDetails;
using PriceListEditor.Application.PriceLists.Queries.GetProductById;
using PriceListEditor.WebAPI.Models;

namespace PriceListEditor.WebAPI.Controllers;

public class ProductsController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public ProductsController(IMapper mapper, IMediator mediator) => 
        (_mediator, _mapper) = (mediator, mapper);
    
    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        ViewData["IdPriceList"] = id;
        return View(await _mediator.Send(new GetPriceListDetailsQuery { Id = id }));
    }

    [HttpGet]
    public async Task<IActionResult> Create(Guid id)
    {
        ViewData["IdPriceList"] = id;
        return View(await _mediator.Send(new GetPriceListDetailsQuery {Id = id}));
    }
 

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto model)
    {
        var command = _mapper.Map<CreateProductCommand>(model);
        command.PriceListId = model.PriceListId;
        await _mediator.Send(command);
        return RedirectToAction("All", "PriceList");
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        ViewData["ProductId"] = id;
        return View(await _mediator.Send(new GetProductByIdQueries() {Id = id}));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateProductDto model)
    {
        var command = _mapper.Map<UpdateProductCommand>(model);
        await _mediator.Send(command);
        return RedirectToAction("All", "PriceList");
    }
    
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand() {Id = id});
        return RedirectToAction("All", "PriceList");
    }
}