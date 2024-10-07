using DEMO_ASP_.NET_CORE_Web_API.Data;
using DEMO_ASP_.NET_CORE_Web_API.Dtos;
using DEMO_ASP_.NET_CORE_Web_API.Helper;
using DEMO_ASP_.NET_CORE_Web_API.Interface;
using DEMO_ASP_.NET_CORE_Web_API.Model;
using Microsoft.EntityFrameworkCore;

namespace DEMO_ASP_.NET_CORE_Web_API.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context) 
        {
            _context = context;
        }

        public async Task<Comment> CreateComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteCommentAsync(long commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
            {
                return null;
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment ?? null;
        }

        public async Task<List<Comment>> GetAllAsync(CommentQueryObject query)
        {
            var result = _context.Comments.AsQueryable();
            result = ApplyFilter(result, query);
            return await result.ToListAsync();
        }
        private IQueryable<Comment> ApplyFilter(IQueryable<Comment> comment, CommentQueryObject query)
        {
            return comment = query.isDescending ? comment.OrderByDescending(x => x.CreatedOn) : 
                comment.OrderBy(x => x.CreatedOn);
        }

        public async Task<Comment?> GetByIdAsync(long id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            return comment;
        }

        public async Task<List<Comment>?> GetByStockId(long stockId, CommentQueryObject query)
        {
            var comments = _context.Comments
                      .Where(c => c.StockId == stockId)
                      .AsQueryable();
            comments = ApplyFilter(comments, query);
            return await comments.ToListAsync();
        }

        public async Task<Comment?> UpdateCommentAsync(long commentId, UpdateCommentDto commentDto)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if (comment == null)
            {
                return null;
            }
            comment.Title = commentDto.Title;
            comment.Description = commentDto.Description;
            _context.SaveChanges();
            return comment;
        }
    }
}
