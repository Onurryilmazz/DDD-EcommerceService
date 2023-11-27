using ShoppingCartService.Domain.Base;

namespace ShoppingCartService.Domain.AggregateModels.ShoppingModels;


public class ShoppingCartItemCategory: Enumeration
{
    public static ShoppingCartItemCategory DigitalItem = new(7889, nameof(DigitalItem),5);
    public static ShoppingCartItemCategory VasItem  = new(3242, nameof(VasItem),3);
    public static ShoppingCartItemCategory Furniture = new(1001, nameof(Furniture));
    public static ShoppingCartItemCategory Electronics = new(3004, nameof(Electronics));

    public ShoppingCartItemCategory(int id, string name, int maxQuantity = 10)
        : base(id, name,maxQuantity)
    {
    }
    
    private static IEnumerable<ShoppingCartItemCategory> List() =>
        new[] { DigitalItem,VasItem, Furniture, Electronics };
    
    public static ShoppingCartItemCategory FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new ShoppingCartException($"Possible values for ShoppingCartItemCategory: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }
    
    public static ShoppingCartItemCategory From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new ShoppingCartException($"Possible values for ShoppingCartItemCategory: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static int GetMaxQuantityWithId(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new ShoppingCartException($"Possible values for ShoppingCartItemCategory: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state.MaxQuantity;
    }
    
}