using AutoMapper;
using EvaFloraStore.Models.ViewModels;
using EvaFloraStore.Models;

namespace EvaFloraStore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<ProductAdding, Product>()
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Product.CategoryId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
            .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.Product.ShortDescription))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl))
            .ForMember(dest => dest.IsVisible, opt => opt.MapFrom(src => src.Product.IsVisible));

            CreateMap<Product, ProductAdding>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Categories, opt => opt.Ignore());


        }
    }
}
