using AutoMapper;
using ShoppingAPI.Entity.DTO.Category;
using ShoppingAPI.Entity.Poco;

namespace ShoppingAPI.Api.Mapping
{
    public class CategoryRequestMapper:Profile
    {
        public CategoryRequestMapper()
        {
            CreateMap<Category, CategoryDTORequest>()
                .ForMember(dest => dest.Name, opt =>
                {
                    opt.MapFrom(src => src.Name);
                }).ReverseMap();

          
        }
    }
}
