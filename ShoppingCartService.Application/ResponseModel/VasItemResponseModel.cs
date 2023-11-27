namespace ShoppingCartService.Application.ResponseModel;

public class VasItemResponseModel
{
    public int  Id { get; set; }
    public int ItemId { get; set; }
    public int VasCategoryId { get; set; }
    public int VasSellerId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
}