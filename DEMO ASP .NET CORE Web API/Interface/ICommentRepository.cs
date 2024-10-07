using DEMO_ASP_.NET_CORE_Web_API.Dtos;
using DEMO_ASP_.NET_CORE_Web_API.Helper;
using DEMO_ASP_.NET_CORE_Web_API.Model;

namespace DEMO_ASP_.NET_CORE_Web_API.Interface
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(CommentQueryObject query);
        Task<Comment?> GetByIdAsync(long id);
        Task<Comment> CreateComment(Comment comment);
        Task<List<Comment>?> GetByStockId(long stockId, CommentQueryObject query);
        Task<Comment?> UpdateCommentAsync(long commentId, UpdateCommentDto commentDto);
        Task<Comment?> DeleteCommentAsync(long commentId);
    }
}
