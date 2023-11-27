using AutoMapper;
using MediatR;
using ShoppingCartService.Application.Interfaces.Repositories;
using ShoppingCartService.Domain.AggregateModels.ShoppingModels;

namespace ShoppingCartService.Application.Features.Commands.ResetShoppingCart;

public class ResetShoppingCartCommandHandler : IRequestHandler<ResetShoppingCartCommand,Dictionary<bool, string>>
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IMapper _mapper;
    
    public ResetShoppingCartCommandHandler(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _mapper = mapper;
    }
    
    public Task<Dictionary<bool, string>> Handle(ResetShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var result = _shoppingCartRepository.ResetCartAsync();
        return result;
    }
}