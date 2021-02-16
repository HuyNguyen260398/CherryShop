using AutoMapper;
using CherryShop_API.Data;
using CherryShop_API.DTOs;

namespace CherryShop_API.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Image, ImageDTO>().ReverseMap();
            CreateMap<Brand, BrandDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
