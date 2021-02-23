using AutoMapper;
using CherryShop_API.Data;
using CherryShop_API.DTOs;

namespace CherryShop_API.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Brand, BrandDTO>().ReverseMap();
            CreateMap<Brand, BrandCreateDTO>().ReverseMap();
            CreateMap<Brand, BrandUpdateDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
            CreateMap<Category, CategoryUpdateDTO>().ReverseMap();

            CreateMap<Image, ImageDTO>().ReverseMap();
            CreateMap<Image, ImageCreateDTO>().ReverseMap();
            CreateMap<Image, ImageUpdateDTO>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();
        }
    }
}
