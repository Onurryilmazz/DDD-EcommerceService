using MediatR;
using ShoppingCartService.Application.Features.Commands.RequestModels;

namespace ShoppingCartService.Application.Features.Queries.GetShoppingCart;

public class GetShoppingCartQuery : IRequest<ShoppingCartViewModel>
{
    public ShoppingCartRequestModel ShoppingCart { get; set; }
}