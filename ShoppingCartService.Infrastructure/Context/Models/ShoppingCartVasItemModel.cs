using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartService.Infrastructure.Context.Models;

[Table("ShoppingCartVasItems")]
public class ShoppingCartVasItemModel
{
    public int Id { get; set; }
    public int ShoppingCartId { get; set; }
    public int ShoppingCartItemId { get; set; }
    public int ShoppingCartVasItemId { get; set; }
    public int VasCategoryId { get; set; }
    public int VasSellerId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
}