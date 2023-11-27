using ShoppingCartService.Domain.Base;

namespace ShoppingCartService.Domain.AggregateModels.ShoppingModels;

public class ShoppingCartPromotion : BaseEntity
{
    public string PromotionName { get; set; }
    public int PromotionId { get; set; }
}