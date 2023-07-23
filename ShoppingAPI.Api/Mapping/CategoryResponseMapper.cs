using AutoMapper;
using ShoppingAPI.Entity.DTO.Category;
using ShoppingAPI.Entity.Poco;

namespace ShoppingAPI.Api.Mapping
{
    public class CategoryResponseMapper:Profile
    {
        public CategoryResponseMapper()
        {
            CreateMap<Category, CategoryDTOResponse>()
               .ForMember(dest => dest.Name, opt =>
               {
                   opt.MapFrom(src => src.Name);
               })
               .ForMember(dest => dest.Guid, opt =>
               {
                   opt.MapFrom(src => src.GUID);
               }).ReverseMap();
              
        }
    }
}
