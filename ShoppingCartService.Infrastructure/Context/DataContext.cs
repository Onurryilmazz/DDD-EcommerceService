using Microsoft.EntityFrameworkCore;
using ShoppingCartService.Domain.AggregateModels.SellerModels;
using ShoppingCartService.Domain.AggregateModels.ShoppingModels;
using ShoppingCartService.Domain.Models;
using ShoppingCartService.Infrastructure.Context.Models;
using ShoppingCartItem = ShoppingCartService.Domain.AggregateModels.ShoppingModels.ShoppingCartItem;

namespace ShoppingCartService.Infrastructure.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<ShoppingCartModel> ShoppingCarts { get; set; }
    public DbSet<ShoppingCartItemModel> ShoppingCartItems { get; set; }
    public DbSet<ShoppingCartVasItemModel> ShoppingCartVasItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
 
}