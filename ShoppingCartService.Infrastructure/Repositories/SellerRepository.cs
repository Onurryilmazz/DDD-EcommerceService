using ShoppingCartService.Application.Interfaces.Repositories;
using ShoppingCartService.Domain.AggregateModels.SellerModels;
using ShoppingCartService.Domain.Base;

namespace ShoppingCartService.Infrastructure.Repositories;

public class SellerRepository: IGenericRepository<Seller>,ISellerRepository
{
    
}