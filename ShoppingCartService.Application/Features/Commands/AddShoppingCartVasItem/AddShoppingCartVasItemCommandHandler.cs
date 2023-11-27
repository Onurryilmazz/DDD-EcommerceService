using AutoMapper;
using MediatR;
using ShoppingCartService.Application.Interfaces.Repositories;
using ShoppingCartService.Domain.Models;

namespace ShoppingCartService.Application.Features.Commands.AddShoppingCartVasItem;

public class AddShoppingCartVasItemCommandHandler : IRequestHandler<AddShoppingCartVasItemCommand,Dictionary<bool,string>>
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IMapper _mapper;
    
    public AddShoppingCartVasItemCommandHandler(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _mapper = mapper;
    }
    public Task<Dictionary<bool,string>> Handle(AddShoppingCartVasItemCommand request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<VasItem>(request.VasItem);
        var response = _shoppingCartRepository.AddVasItemAsync(model);
        return response;
    }
}