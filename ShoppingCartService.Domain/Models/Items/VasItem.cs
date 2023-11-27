using ShoppingCartService.Domain.Base;

namespace ShoppingCartService.Domain.Models;

public class VasItem : BaseEntity
{
    public int ItemId { get; set; }
    public int VasCategoryId { get; set; }
    public int VasSellerId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
}