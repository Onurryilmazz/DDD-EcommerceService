using MediatR;
using ShoppingCartService.Application.Features.Commands.RequestModels;

namespace ShoppingCartService.Application.Features.Commands.AddShoppingCartVasItem;

public class AddShoppingCartVasItemCommand : IRequest<Dictionary<bool,string>>
{
    public VasItemRequestModel? VasItem { get; set; }
}