namespace ShoppingCartService.Application.Features.Queries;

public class ShoppingCartViewModel
{
    public double TotalAmount { get; set; }
    public int? AppliedPromotionId { get; set; }
    public double TotalDiscount { get; set; }
    public List<ShoppingCartItemViewModel>? Items { get; set; }
}

public class ShoppingCartItemViewModel
{
    public int ItemId { get;  set; }
    public int Quantity { get;  set; }
    public int Price { get;  set; }
    public int CategoryId { get;  set; }
    public int SellerId { get;  set; }
    public List<VasItemViewModel>? VasItems { get; set; }
}

public class VasItemViewModel
{
    public int  Id { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public int CategoryId { get; set; }
    public int SellerId { get; set; }
}

