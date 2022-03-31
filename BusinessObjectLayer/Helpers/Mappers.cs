using AutoMapper;
using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;

namespace BusinessObjectLayer.Helpers
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<UserDto, UserEntity>();

            CreateMap<ProductEntity, ProductDto>();
            CreateMap<ProductDto, ProductEntity>();

            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<CategoryDto, CategoryEntity>();

            CreateMap<SubcategoryEntity, SubcategoryDto>();
            CreateMap<SubcategoryDto, SubcategoryEntity>();
        }
    }
}
