using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartService.Infrastructure.Context.Models;

[Table("ShoppingCartItems")]
public class ShoppingCartItemModel
{
    public int Id { get; set; }
    public int ShoppingCartId { get; set; }
    public int ShoppingCartItemId { get; set; }
    public int Quantity { get;  set; }
    public int Price { get;  set; }
    public int CategoryId { get;  set; }
    public int SellerId { get;  set; }
}