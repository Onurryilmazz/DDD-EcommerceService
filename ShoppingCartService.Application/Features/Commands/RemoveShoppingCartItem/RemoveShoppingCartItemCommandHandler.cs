using AutoMapper;
using MediatR;
using ShoppingCartService.Application.Interfaces.Repositories;
using ShoppingCartService.Domain.AggregateModels.ShoppingModels;

namespace ShoppingCartService.Application.Features.Commands.RemoveShoppingCartItem;

public class RemoveShoppingCartItemCommandHandler : IRequestHandler<RemoveShoppingCartItemCommand,Dictionary<bool, string>>
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IMapper _mapper;
    
    public RemoveShoppingCartItemCommandHandler(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _mapper = mapper;
    }
    
    public Task<Dictionary<bool, string>> Handle(RemoveShoppingCartItemCommand request, CancellationToken cancellationToken)
    {
        var removeItem =  _shoppingCartRepository.RemoveItemAsync(request.ShoppingCartItemId);
        return removeItem;
    }
}