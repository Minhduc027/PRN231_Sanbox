
using AutoMapper;
using DEMO_ASP_.NET_CORE_Web_API.Dtos;
using DEMO_ASP_.NET_CORE_Web_API.Model;

namespace DEMO_ASP_.NET_CORE_Web_API.Helper;

public class MapperConfig: Profile
{
    public MapperConfig()
    {
        CreateMap<CreateCommentDto, Comment>().ReverseMap();
        CreateMap<CreateStockDto, Stock>().ReverseMap();
        CreateMap<CommentDto, Comment>().ReverseMap();
        CreateMap<Stock, StockDto>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments)).ReverseMap();
    }
}
