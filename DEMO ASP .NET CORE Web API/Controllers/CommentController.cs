using DEMO_ASP_.NET_CORE_Web_API.Dtos;
using DEMO_ASP_.NET_CORE_Web_API.Helper;
using DEMO_ASP_.NET_CORE_Web_API.Interface;
using DEMO_ASP_.NET_CORE_Web_API.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEMO_ASP_.NET_CORE_Web_API.Controllers
{
    [Route("/api/comment")]
    [ApiController]
    public class CommentController: ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] CommentQueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comments = await _commentRepository.GetAllAsync(query);
            var commentDots = comments.Select(s => s.ToDto());
            return Ok(commentDots);
        }
        [HttpGet]
        [Route("{id:long}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commentRepository.GetByIdAsync(id);
            if(comment == null)
            {
                return NotFound("For inputted id: " + id);
            }
            return Ok(comment.ToDto());
        }
        [HttpPost("{stockId:long}")]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromRoute] long stockId, [FromBody] CreateCommentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!await _stockRepository.StockExist(stockId))
            {
                return BadRequest("Stock not found!");
            }
            var comment = dto.ToEntity(stockId);
            await _commentRepository.CreateComment(comment);
            return CreatedAtAction("GetById", new { id = comment.Id }, comment.ToDto());
        }
        [HttpPut]
        [Route("{commentId:long}")]
        [Authorize]
        public async Task<IActionResult> UpdateComment([FromRoute] long commentId, [FromBody] UpdateCommentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commentRepository.UpdateCommentAsync(commentId, dto);
            if(comment == null)
            {
                return NotFound("Comment not found!");
            }
            return Ok(comment.ToDto());
        }
        [HttpDelete]
        [Route("{commentId:long}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteComment([FromRoute] long commentId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commentRepository.DeleteCommentAsync(commentId);
            if(comment == null)
            {
                return NotFound("For input Id: " + commentId);
            }
            return Ok(comment);
        }
        [HttpGet]
        [Route("/api/comment/StockId/{stockId}")]
        [Authorize]
        public async Task<IActionResult> GetByStockId([FromRoute] long stockId, [FromQuery] CommentQueryObject query)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commentRepository.GetByStockId(stockId, query);
            if (comment == null)
            {
                return NotFound("For input Id: " + stockId);
            }
            return Ok(comment);
        }
    }
}
