using ShoppingCartService.Application.Features.Commands.AddShoppingCartItem;
using ShoppingCartService.Application.Features.Commands.RequestModels;
using ShoppingCartService.Application.ResponseModel;
using ShoppingCartService.Domain.AggregateModels.ShoppingModels;
using ShoppingCartService.Domain.Models;

namespace ShoppingCartService.Application.Interfaces.Repositories;

public interface IShoppingCartRepository : IGenericRepository<ShoppingCartService.Domain.AggregateModels.ShoppingModels.ShoppingCart>
{
    public Task<Dictionary<bool,string>> AddItemAsync(ShoppingCartItem item);
    public Task<Dictionary<bool,string>> AddVasItemAsync(VasItem vasItem);
    public  Task<Dictionary<bool, string>> RemoveItemAsync(int itemId);
    public Task<Dictionary<bool, string>> ResetCartAsync();
    public Task<ShoppingCartResponseModel>  GetCart ();
    
    
}