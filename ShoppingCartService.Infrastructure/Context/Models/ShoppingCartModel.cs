    using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartService.Infrastructure.Context.Models;

[Table("ShoppingCarts")]   
public class ShoppingCartModel
{
    public int Id { get; set; }
    public double TotalAmount { get; set; }
    public int? AppliedPromotionId { get; set; }
    public double TotalDiscount { get; set; }
}