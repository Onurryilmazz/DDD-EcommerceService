using ShoppingCartService.Domain.Base;

namespace ShoppingCartService.Domain.Models;

public class Item : BaseEntity
{
    public int CategoryId { get; set; }
    public int SellerId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
}