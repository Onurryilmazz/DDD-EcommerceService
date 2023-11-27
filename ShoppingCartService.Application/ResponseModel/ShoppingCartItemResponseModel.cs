namespace ShoppingCartService.Application.ResponseModel;

public class ShoppingCartItemResponseModel
{
    public int ItemId { get;  set; }
    public int Quantity { get;  set; }
    public int Price { get;  set; }
    public int CategoryId { get;  set; }
    public int SellerId { get; set; }
    public List<VasItemResponseModel>? VasItems { get; set; }
}