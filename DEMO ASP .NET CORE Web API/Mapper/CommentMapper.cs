using DEMO_ASP_.NET_CORE_Web_API.Dtos;
using DEMO_ASP_.NET_CORE_Web_API.Model;
using System.Runtime.CompilerServices;

namespace DEMO_ASP_.NET_CORE_Web_API.Mapper
{
    public static class CommentMapper
    {
        public static CommentDto ToDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Description = commentModel.Description,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId,
            };
        }
        public static Comment ToEntity(this CreateCommentDto commentDto, long stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Description = commentDto.Description,
                StockId = stockId
            };
        }
    }
}
