using ShoppingCartService.Domain.Base;

namespace ShoppingCartService.Domain.AggregateModels.SellerModels;

public class Seller : BaseEntity
{
    public string Name { get; set; }
}