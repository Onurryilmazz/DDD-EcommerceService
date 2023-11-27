using AutoMapper;
using MediatR;
using ShoppingCartService.Application.Interfaces.Repositories;
using ShoppingCartService.Domain.AggregateModels.ShoppingModels;

namespace ShoppingCartService.Application.Features.Queries.GetShoppingCart;

public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery,ShoppingCartViewModel>
{
    IShoppingCartRepository _shoppingCartRepository;
    IMapper _mapper;
    
    public GetShoppingCartQueryHandler(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _mapper = mapper;
    }
    
    public Task<ShoppingCartViewModel> Handle(GetShoppingCartQuery request, CancellationToken cancellationToken)
    {
        var shoppingCart = _shoppingCartRepository.GetCart().Result;
        var result = _mapper.Map<ShoppingCartViewModel>(shoppingCart);
        return Task.FromResult(result);
    }
}