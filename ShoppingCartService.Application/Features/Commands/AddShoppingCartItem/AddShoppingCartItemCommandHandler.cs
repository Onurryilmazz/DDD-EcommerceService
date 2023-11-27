using AutoMapper;
using MediatR;
using ShoppingCartService.Application.Interfaces.Repositories;
using ShoppingCartService.Domain.AggregateModels.ShoppingModels;

namespace ShoppingCartService.Application.Features.Commands.AddShoppingCartItem;

public class AddShoppingCartItemCommandHandler : IRequestHandler<AddShoppingCartItemCommand,Dictionary<bool,string>>
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IMapper _mapper;
    
    public AddShoppingCartItemCommandHandler(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _mapper = mapper;
    }
    public Task<Dictionary<bool,string>> Handle(AddShoppingCartItemCommand request, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<ShoppingCartItem>(request.Item);
        var result = _shoppingCartRepository.AddItemAsync(item);
        return result;
    }
}