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

            CreateMap<CartItemEntity, CartItemDto>();
            CreateMap<CartItemDto, CartItemEntity>();

            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<CategoryDto, CategoryEntity>();

            CreateMap<OrderEntity, OrderDto>();
            CreateMap<OrderDto, OrderEntity>();

            CreateMap<ProductWarehouseDto, ProductWarehouseMapping>();
            CreateMap<ProductWarehouseMapping, ProductWarehouseDto>();

            CreateMap<WarehouseDto, WarehouseEntity>();
            CreateMap<WarehouseEntity, WarehouseDto>();

            CreateMap<PickupPointDto, PickupPointEntity>();
            CreateMap<PickupPointEntity, PickupPointDto>();

            CreateMap<SubcategoryEntity, SubcategoryDto>();
            CreateMap<SubcategoryDto, SubcategoryEntity>();
        }
    }
}
