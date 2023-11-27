using System.Linq.Expressions;
using ShoppingCartService.Domain.Base;
using ShoppingCartService.Domain.Interfaces;

namespace ShoppingCartService.Application.Interfaces.Repositories;

public interface IGenericRepository < T > : IRepository<T> where T: BaseEntity 
{
    //Task<T> GetByIdAsync(int id);

}