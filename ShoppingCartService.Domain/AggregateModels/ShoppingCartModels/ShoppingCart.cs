using System.Security.AccessControl;
using ShoppingCartService.Domain.Base;
using ShoppingCartService.Domain.Interfaces;

namespace ShoppingCartService.Domain.AggregateModels.ShoppingModels;

public class ShoppingCart : BaseEntity, IAggregateRoot
{
    # region Property
    private readonly List<ShoppingCartItem> _shoppingCartItems;
    private double _TotalPrice;
    public double TotalAmount { get; private set; }
    public int? AppliedPromotionId { get; private set; }
    public double TotalDiscount { get; private set; }
    public List<ShoppingCartItem> Items  => _shoppingCartItems;
    #endregion

    public ShoppingCart()
    {
        _shoppingCartItems = new List<ShoppingCartItem>();
        _TotalPrice = 0;
        AppliedPromotionId = null;
        TotalDiscount = 0;
        TotalAmount = 0;
    }

    public void AddItem(ShoppingCartItem item)
    {
        var uniqueItemsCount = _shoppingCartItems.Select(x => x.ItemId).Distinct().Count();
        
        if (uniqueItemsCount >= 10)
        {
            throw new ShoppingCartException("Cannot add more than 10 unique items to cart");
        }
        
        if (_shoppingCartItems.Count >= 30)
        {
            throw new ShoppingCartException("Cannot add more than 30 items to cart");
        }

        var totalAmountAddItem = TotalAmount + (item.Price * item.Quantity) + item.VasItems.Select(x => x.Price * x.Quantity).Sum();
        var itemListAfterAdd = new List<ShoppingCartItem>(_shoppingCartItems);
        itemListAfterAdd.Add(item);
        var discountInfo = CalculatePromotion(totalAmountAddItem, itemListAfterAdd);
        
        if(totalAmountAddItem - discountInfo.FirstOrDefault().Value > 500000)
        {
            throw new ShoppingCartException(
                message: "Cannot add more items to cart. Total amount cannot exceed 500000");
        }
        
        
        TotalDiscount = discountInfo.FirstOrDefault().Value;
        AppliedPromotionId = discountInfo.FirstOrDefault().Key;
        _shoppingCartItems.Add(item);
        _TotalPrice = _shoppingCartItems.Sum(x => x.Price * x.Quantity) + _shoppingCartItems.Select(x => x.VasItems.Select(y => y.Price * y.Quantity).Sum()).Sum();
        TotalAmount = _TotalPrice - TotalDiscount;
        
    }

    public void RemoveItem(int itemId)
    {
        var item = _shoppingCartItems.FirstOrDefault(x => x.ItemId == itemId);
        
        if (!_shoppingCartItems.Any(x => x.ItemId == item.ItemId))
        {
            throw new ShoppingCartException(message: "Item is not in Shopping Cart");
        }
        
        _shoppingCartItems.Remove(item);
        _TotalPrice -= (item.Price * item.Quantity) + (item.VasItems.Select(x => x.Price * x.Quantity).Sum());
        var discountInfo = CalculatePromotion(_TotalPrice, _shoppingCartItems);
        TotalDiscount = discountInfo.FirstOrDefault().Value;
        AppliedPromotionId = discountInfo.FirstOrDefault().Key;
        TotalAmount = _TotalPrice - TotalDiscount;
    }
    
    public Dictionary<int,double> CalculatePromotion(double totalPrice, List<ShoppingCartItem> items)
    {

        var categoryPromotionDiscount = 0.0;
        
        // Seller id , discount
        var promotionAndDiscount = new Dictionary<int, double>();

        #region Seller Promotion

        var sellerCount = items.Select(x => x.SellerId).Distinct().Count();;
        if (sellerCount == 1)
        {
            promotionAndDiscount.Add(9909, totalPrice * 0.1);
        }

        #endregion

        #region Category Promotion

        foreach (var item in items.Where(x => x.CategoryId==3003).ToList())
        {
            categoryPromotionDiscount += (item.Price * item.Quantity) * 0.05;
        }
        
        promotionAndDiscount.Add(5675, categoryPromotionDiscount);

        #endregion

        #region Total Price Promotion

        if (totalPrice >= 500 && totalPrice < 5000)
        {
            promotionAndDiscount.Add(1232, 250);
        }
        else if (totalPrice >= 5000 && totalPrice < 10000)
        {
            promotionAndDiscount.Add(1232, 500);
        }
        else if (totalPrice >= 10000 && totalPrice < 50000)
        {
            promotionAndDiscount.Add(1232, 1000);
        }
        else if (totalPrice >= 50000)
        {
            promotionAndDiscount.Add(1232, 2000);
        }

        #endregion
        
        var maxDiscountPromotion = promotionAndDiscount.OrderByDescending(x => x.Value).FirstOrDefault();
        return new Dictionary<int, double> { { maxDiscountPromotion.Key, maxDiscountPromotion.Value } };
    }

    public void UpdateTotalAmount(int amount)
    {
        _TotalPrice += amount;
        var discountInfo = CalculatePromotion(_TotalPrice, _shoppingCartItems);
        TotalDiscount = discountInfo.FirstOrDefault().Value;
        AppliedPromotionId = discountInfo.FirstOrDefault().Key;
        TotalAmount = _TotalPrice - TotalDiscount;
    }

}