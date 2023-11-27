namespace ShoppingCartService.Application.Features.Commands.RequestModels;

public class ShoppingCartRequestModel
{
    public double TotalAmount { get; set; } = 0;
    public int? AppliedPromotionId { get;  set; }
    public double TotalDiscount { get; set; } = 0;
    public List<ShoppingCartItemRequestModel>? Items { get; set; }
}

public class ShoppingCartItemRequestModel
{
    public int ItemId { get;  set; }
    public int Quantity { get;  set; }
    public int Price { get;  set; }
    public int CategoryId { get;  set; }
    public int SellerId { get;  set; }
}

public class VasItemRequestModel
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public int VasCategoryId { get; set; }
    public int VasSellerId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
}