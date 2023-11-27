using AutoMapper;
using ShoppingCartService.Application.Features.Commands.RequestModels;
using ShoppingCartService.Application.Features.Queries;
using ShoppingCartService.Application.ResponseModel;
using ShoppingCartService.Domain.AggregateModels.ShoppingModels;
using ShoppingCartItem = ShoppingCartService.Domain.AggregateModels.ShoppingModels.ShoppingCartItem;
using VasItem = ShoppingCartService.Domain.Models.VasItem;

namespace ShoppingCartService.Application.Mapping.ShoppingCartMapping;

public class ShoppingCartMappingProfile : Profile
{
    public ShoppingCartMappingProfile()
    {
        CreateMap<ShoppingCart, ShoppingCartViewModel>().ReverseMap();
        CreateMap<ShoppingCartItemResponseModel, ShoppingCartItemViewModel>().ReverseMap();
        CreateMap<VasItemResponseModel, VasItemViewModel>().ReverseMap();
        CreateMap<ShoppingCartResponseModel, ShoppingCartViewModel>()
            .ForMember(x => x.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(x => x.AppliedPromotionId, opt => opt.MapFrom(src => src.AppliedPromotionId))
            .ForMember(x => x.TotalDiscount, opt => opt.MapFrom(src => src.TotalDiscount))
            .ForMember(x => x.AppliedPromotionId, opt => opt.MapFrom(src => src.AppliedPromotionId))
            .ForMember(x => x.Items, opt => opt.MapFrom(src => src.Items))
            .ReverseMap();

            
        CreateMap<ShoppingCart,ShoppingCartRequestModel>().ReverseMap();
        CreateMap<ShoppingCartItem, ShoppingCartItemRequestModel>().ReverseMap();
        CreateMap<VasItem, VasItemRequestModel>().ReverseMap();
        
    }   
}