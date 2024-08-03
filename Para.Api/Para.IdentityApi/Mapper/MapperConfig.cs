using AutoMapper;
using Para.IdentityApi.Domain;
using Para.IdentityApi.Schema;

namespace Para.IdentityApi;

public class MapperConfig : Profile
{

    public MapperConfig()
    {
        CreateMap<Post, PostResponse>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.CategoryUrlName, opt => opt.MapFrom(src => src.Category.UrlName));
            
        CreateMap<PostRequest, Post>();

        CreateMap<Category, CategoryResponse>();
        CreateMap<CategoryRequest, Category>();
    }
}