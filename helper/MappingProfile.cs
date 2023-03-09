using AutoMapper;
using DummyProject.Dto;
using DummyProject.Models;

namespace DummyProject.helper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
