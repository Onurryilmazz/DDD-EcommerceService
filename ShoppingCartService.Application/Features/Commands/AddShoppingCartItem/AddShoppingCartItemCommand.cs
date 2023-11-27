using MediatR;
using ShoppingCartService.Application.Features.Commands.RequestModels;

namespace ShoppingCartService.Application.Features.Commands.AddShoppingCartItem;

public class AddShoppingCartItemCommand : IRequest<Dictionary<bool,string>>
{ 
    public ShoppingCartItemRequestModel? Item { get; set; }
}






