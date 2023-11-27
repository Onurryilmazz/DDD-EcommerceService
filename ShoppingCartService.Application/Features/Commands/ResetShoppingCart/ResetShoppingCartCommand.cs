using MediatR;
using ShoppingCartService.Application.Features.Commands.RequestModels;

namespace ShoppingCartService.Application.Features.Commands.ResetShoppingCart;

public class ResetShoppingCartCommand : IRequest<Dictionary<bool, string>>
{

}