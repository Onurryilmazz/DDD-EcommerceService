namespace ShoppingCartService.Application.ResponseModel;

public class ShoppingCartResponseModel
{
    public int Id  { get; set; }
    public double TotalAmount { get; set; }
    public int? AppliedPromotionId { get;  set; }
    public double TotalDiscount { get;  set; }
    public List<ShoppingCartItemResponseModel> Items {get; set;}
}