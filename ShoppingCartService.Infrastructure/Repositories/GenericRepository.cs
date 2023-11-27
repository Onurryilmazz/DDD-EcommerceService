using ShoppingCartService.Application.Interfaces.Repositories;
using ShoppingCartService.Domain.Base;

namespace ShoppingCartService.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    
}