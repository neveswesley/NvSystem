using AutoMapper;
using NvSystem.Application.UseCases.Category.Query;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Entities;

namespace NvSystem.Application.Mapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<CreateUserRequest, User>().ReverseMap();
        CreateMap<UserResponse, User>().ReverseMap();
        CreateMap<GetCategoryResponse, Category>().ReverseMap();
        CreateMap<Product, ProductResponse>().ReverseMap();
        CreateMap<Category, CategorySimpleDto>().ReverseMap();
    }
}