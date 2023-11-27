using ShoppingCartService.Domain.Base;
using ShoppingCartService.Domain.Interfaces;
using ShoppingCartService.Domain.Models;

namespace ShoppingCartService.Domain.AggregateModels.ShoppingModels;

public class ShoppingCartItem : BaseEntity 
{
    private readonly List<VasItem> _vasItems;

    public int ItemId { get; private set; }
    public int Quantity { get;private  set; }
    public int Price { get; private set; }
    public int CategoryId { get; private set; }
    public int SellerId { get; private set; }
    public List<VasItem>? VasItems => _vasItems;
    
    
    
    public ShoppingCartItem()
    {
        _vasItems = new List<VasItem>();
    }

    public ShoppingCartItem CreateItem(int itemId, int quantity, int price, int categoryId, int sellerId)
    {

        if (quantity >= ShoppingCartItemCategory.GetMaxQuantityWithId(categoryId))
        {
            throw new ShoppingCartException(
                message: $"Cannot add more than {ShoppingCartItemCategory.GetMaxQuantityWithId(categoryId)} items of category {categoryId} to cart");
        }
        
        ItemId = itemId;
        Quantity = quantity;
        Price = price;
        CategoryId = categoryId;
        SellerId = sellerId;
        
        return this;
    }
    
    public int AddVasItem(VasItem vasItem)
    {
        if (_vasItems.Count >= 3)
        {
            throw new ShoppingCartException("Cannot add more than 3 vas items to cart");
        }

        if (vasItem.VasSellerId != 5003)
        {
            throw new ShoppingCartException(message: "Cannot add vas item from seller other than 5003");
        }
        
        // category ıd must equal to 1 or 2
        if (CategoryId != ShoppingCartItemCategory.Electronics.Id && CategoryId != ShoppingCartItemCategory.Furniture.Id)
        {
            throw new ShoppingCartException(message: "Cannot add vas item to cart if category is not electronics or furniture");
        }

        _vasItems.Add(vasItem);
        return _vasItems.Select(x => x.Quantity * x.Price).Sum();

    }
    
}