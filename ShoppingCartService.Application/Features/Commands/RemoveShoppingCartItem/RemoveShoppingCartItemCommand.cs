using MediatR;
using ShoppingCartService.Application.Features.Commands.RequestModels;

namespace ShoppingCartService.Application.Features.Commands.RemoveShoppingCartItem;

public class RemoveShoppingCartItemCommand : IRequest<Dictionary<bool, string>>
{
    public int ShoppingCartItemId { get; set; }
}