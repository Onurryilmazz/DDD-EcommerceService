using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ShoppingCartService.Application.Features.Commands.RequestModels;
using ShoppingCartService.Application.Interfaces.Repositories;
using ShoppingCartService.Application.ResponseModel;
using ShoppingCartService.Domain.AggregateModels.ShoppingModels;
using ShoppingCartService.Domain.Models;
using ShoppingCartService.Infrastructure.Context;
using ShoppingCartService.Infrastructure.Context.Models;

namespace ShoppingCartService.Infrastructure.Repositories;

public class ShoppingCartReposityory : IGenericRepository<ShoppingCart>,IShoppingCartRepository
{
    private readonly DataContext _context;

    public ShoppingCartReposityory(DataContext context)
    {
        _context = context;
    }
    public async Task<Dictionary<bool,string>> AddItemAsync(ShoppingCartItem item)
    {
        try
        {
            var cart = SetShoppingCartValue().Result;
            var itemControl = new ShoppingCartItem().CreateItem(
                item.ItemId,
                item.Quantity,
                item.Price,
                item.CategoryId,
                item.SellerId
            );
            cart.AddItem(itemControl);
            
            await _context.ShoppingCartItems.AddAsync(new ShoppingCartItemModel
            {
                CategoryId = item.CategoryId,
                ShoppingCartItemId = item.ItemId,
                Price = item.Price,
                Quantity = item.Quantity,
                SellerId = item.SellerId,
                ShoppingCartId = cart.Id
            });

            if (item.VasItems.Count > 0)
            {
                foreach (var vasItem in item.VasItems)
                {
                    await _context.ShoppingCartVasItems.AddAsync(new ShoppingCartVasItemModel
                    {
                        Price = vasItem.Price,
                        Quantity = vasItem.Quantity,
                        ShoppingCartId = cart.Id,
                        ShoppingCartItemId = item.ItemId,
                        VasCategoryId = vasItem.VasCategoryId,
                        VasSellerId = vasItem.VasSellerId
                    });
                }
                
            }
             
            
            var updatedCart = await _context.ShoppingCarts.Where(x=>x.Id == cart.Id).FirstOrDefaultAsync();
            updatedCart.TotalAmount = cart.TotalAmount;
            updatedCart.TotalDiscount = cart.TotalDiscount;
            updatedCart.AppliedPromotionId = cart.AppliedPromotionId;

            _context.ShoppingCarts.Update(updatedCart);
            await _context.SaveChangesAsync();
            return new Dictionary<bool, string>{{true,"Item added to cart"}};
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Dictionary<bool, string>{{false,e.Message}};
        }
    }

    public async Task<Dictionary<bool, string>> AddVasItemAsync(VasItem vasItem)
    {
        try
        {
            var cart = SetShoppingCartValue().Result;
            var items = cart.Items.Where(x=>x.ItemId == vasItem.ItemId).ToList();
            int vasItemPrice = 0;
            
            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    vasItemPrice = item.AddVasItem(vasItem);
                    await _context.ShoppingCartVasItems.AddAsync(new ShoppingCartVasItemModel
                    {
                        ShoppingCartVasItemId = vasItem.Id,
                        Price = vasItem.Price,
                        Quantity = vasItem.Quantity,
                        ShoppingCartId = cart.Id,
                        ShoppingCartItemId = item.ItemId,
                        VasCategoryId = vasItem.VasCategoryId,
                        VasSellerId = vasItem.VasSellerId
                    });
                }

                await _context.SaveChangesAsync();
            }
            else
            {
                return new Dictionary<bool, string>{{false,"Item not found"}};
            }
            
            cart.UpdateTotalAmount(vasItemPrice);
            var updatedCart = await _context.ShoppingCarts.Where(x=>x.Id == cart.Id).FirstOrDefaultAsync();
            updatedCart.TotalAmount = cart.TotalAmount;
            updatedCart.TotalDiscount = cart.TotalDiscount;
            updatedCart.AppliedPromotionId = cart.AppliedPromotionId;

            _context.ShoppingCarts.Update(updatedCart);
            await _context.SaveChangesAsync();

            return new Dictionary<bool, string> { { true,"Vas Item Added to Item" } };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Dictionary<bool, string>{{false,e.Message}};
        }
        
    }

    public async Task<Dictionary<bool, string>> RemoveItemAsync(int itemId)
    {
        try
        {
            var cart = SetShoppingCartValue().Result;
            cart.RemoveItem(itemId);
            
            _context.ShoppingCartItems.Where(x=>x.ShoppingCartId == cart.Id && x.ShoppingCartItemId == itemId)
                .ToList().ForEach(x=> _context.ShoppingCartItems.Remove(x));

            _context.ShoppingCartVasItems.Where(x => x.ShoppingCartId == cart.Id && x.ShoppingCartItemId == itemId)
                .ToList().ForEach(x=> _context.ShoppingCartVasItems.Remove(x));
            
            var updatedCart = _context.ShoppingCarts.Where(x=>x.Id == cart.Id).FirstOrDefault();
            updatedCart.TotalAmount = cart.TotalAmount;
            updatedCart.TotalDiscount = cart.TotalDiscount;
            updatedCart.AppliedPromotionId = cart.AppliedPromotionId;
            
            _context.ShoppingCarts.Update(updatedCart);

            await _context.SaveChangesAsync();
            
            return new Dictionary<bool, string> { { true,"Item Removed" } };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return  new Dictionary<bool, string> { { false,e.Message } };;
        }
        
    }

    public async Task<Dictionary<bool, string>> ResetCartAsync()    
    {
        try
        {
            var _cart = await _context.ShoppingCarts.FirstOrDefaultAsync();
            var cartItems =  _context.ShoppingCartItems.Where(x => x.ShoppingCartId == _cart.Id);
            _context.ShoppingCartItems.RemoveRange(cartItems);
            
            var cartVasItems = _context.ShoppingCartVasItems.Where(x => x.ShoppingCartId == _cart.Id);
            _context.ShoppingCartVasItems.RemoveRange(cartVasItems);

            
            _cart.TotalAmount = 0;
            _cart.TotalDiscount = 0;
            _cart.AppliedPromotionId = null;
            _context.ShoppingCarts.Update(_cart);
         
            await _context.SaveChangesAsync();
            return new Dictionary<bool, string> { { true,"Cart Reset succesfully completed." } };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
             return new Dictionary<bool, string> { { false,e.Message } };;
        }
        
    }

    public async Task<ShoppingCartResponseModel> GetCart()
    {
        var cart = await _context.ShoppingCarts.FirstOrDefaultAsync();
        var items = _context.ShoppingCartItems.Where(x => x.ShoppingCartId == cart.Id).ToList();
        
        var responseItems = new List<ShoppingCartItemResponseModel>();

        foreach (var item in items)
        {
            var vasItems = _context.ShoppingCartVasItems.Where(x => x.ShoppingCartItemId == item.Id
            && x.ShoppingCartId == cart.Id).ToList();
            
            var responseVasItems = new List<VasItemResponseModel>();
            
            foreach (var vasItem in vasItems)
            {
                var responseVasItem = new VasItemResponseModel()
                {
                    ItemId = vasItem.ShoppingCartVasItemId,
                    VasCategoryId = vasItem.VasCategoryId,
                    VasSellerId = vasItem.VasSellerId,
                    Quantity = vasItem.Quantity,
                    Price = vasItem.Price
                };
                
                responseVasItems.Add(responseVasItem);
            }
            
            var responseItem = new ShoppingCartItemResponseModel()
            {
                ItemId = item.ShoppingCartItemId,
                Quantity = item.Quantity,
                Price = item.Price,
                CategoryId = item.CategoryId,
                SellerId = item.SellerId,
                VasItems = responseVasItems ?? null
            };
            
            responseItems.Add(responseItem);
            
        }
        
        var shoppingCartResponseModel = new ShoppingCartResponseModel()
        {
            Id = cart.Id,
            TotalAmount = cart.TotalAmount,
            AppliedPromotionId = cart.AppliedPromotionId,
            TotalDiscount = cart.TotalDiscount,
            Items = responseItems
        };
        
        return shoppingCartResponseModel;
    }

    public async Task<ShoppingCart> SetShoppingCartValue()
    {
        var cart = await _context.ShoppingCarts.FirstOrDefaultAsync();

        if (cart is null)
        {
            await _context.ShoppingCarts.AddAsync(
                new ShoppingCartModel());

            await _context.SaveChangesAsync();
            
            return new ShoppingCart();
        }
        
        var items = await _context.ShoppingCartItems.Where(x => x.ShoppingCartId == cart.Id).ToListAsync();
        var vasItems = await _context.ShoppingCartVasItems.Where(x => x.ShoppingCartId == cart.Id).ToListAsync();
        
        var shoppingCart = new ShoppingCart();

        foreach (var item in items)
        {
            var cartItem = new ShoppingCartItem();
            cartItem.CreateItem(
                item.ShoppingCartItemId,
                item.Quantity,
                item.Price,
                item.CategoryId,
                item.SellerId
                );

            var selectedVasItem = vasItems.Where(x => x.ShoppingCartItemId == item.ShoppingCartItemId).ToList();    
            
            foreach (var vasItem in selectedVasItem)
            {
                cartItem.AddVasItem(new VasItem
                {
                    Id = vasItem.ShoppingCartVasItemId,
                    ItemId = vasItem.ShoppingCartItemId,
                    Price = vasItem.Price,
                    Quantity = vasItem.Quantity,
                    VasCategoryId = vasItem.VasCategoryId,
                    VasSellerId = vasItem.VasSellerId,
                });
            }
            
            shoppingCart.AddItem(cartItem);

        }
        shoppingCart.Id = cart.Id;
        return shoppingCart;
    }
}
