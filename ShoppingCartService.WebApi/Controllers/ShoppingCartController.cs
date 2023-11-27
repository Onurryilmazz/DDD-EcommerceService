using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartService.Application.Features.Commands.AddShoppingCartItem;
using ShoppingCartService.Application.Features.Commands.AddShoppingCartVasItem;
using ShoppingCartService.Application.Features.Commands.RemoveShoppingCartItem;
using ShoppingCartService.Application.Features.Commands.RequestModels;
using ShoppingCartService.Application.Features.Commands.ResetShoppingCart;
using ShoppingCartService.Application.Features.Queries;
using ShoppingCartService.Application.Features.Queries.GetShoppingCart;
using ShoppingCartService.Domain.AggregateModels.ShoppingModels;
using ShoppingCartService.WebApi.Models.ServiceResponseModel;

namespace ShoppingCartService.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShoppingCartController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Route("displayCart")]
    [HttpGet]
    public ServiceResponse<ShoppingCartViewModel> Get()
    {
        var response = new ServiceResponse<ShoppingCartViewModel>();
        var result = _mediator.Send(new GetShoppingCartQuery());
        response.message = result.Result;
        response.result = result.IsCompletedSuccessfully;
        
        return response;
    }
    
    [Route("addItem")]
    [HttpPost]
    public ServiceResponse<string> AddItem(AddShoppingCartItemCommand item)
    {
        var response = new ServiceResponse<string>();
        var result = _mediator.Send(item);
        response.message = result.Result.FirstOrDefault().Value;
        response.result = result.Result.FirstOrDefault().Key;
        return response;
    }

    [Route("removeItem")]
    [HttpPost]
    public ServiceResponse<string> RemoveShoppingCart(RemoveShoppingCartItemCommand item)
    {
        var response = new ServiceResponse<string>();
        var result = _mediator.Send(item);
        response.message = result.Result.FirstOrDefault().Value;
        response.result = result.Result.FirstOrDefault().Key;
        return response;
    }
    
    [Route("resetCart")]
    [HttpPost]
    public ServiceResponse<string> ResetShoppingCart()
    {
        var response = new ServiceResponse<string>();
        var result = _mediator.Send(new ResetShoppingCartCommand());
        response.message = result.Result.FirstOrDefault().Value;
        response.result = result.Result.FirstOrDefault().Key;
        return response;
    }
    
    [Route("addVasItemToItem")]
    [HttpPost]
    public ServiceResponse<string> AddVasItem(AddShoppingCartVasItemCommand vasItem)
    {
        var response = new ServiceResponse<string>();
        var result = _mediator.Send(vasItem);
        response.message = result.Result.FirstOrDefault().Value;
        response.result = result.Result.FirstOrDefault().Key;
        return response;
    }
    
}