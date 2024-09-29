using ECommerceAPI.Application.Abstractions.Comment;
using ECommerceAPI.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Member")] // Kullanıcının giriş yapmış olması gerekli
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("add-comment")]
        public async Task<IActionResult> AddComment([FromBody] UserCommentDto userCommentDto)
        {
            bool result = await _commentService.AddCommentAsync(userCommentDto);

            return Ok(result);

        }

        [HttpDelete("delete-comment/{id}")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            await _commentService.DeleteCommentAsync(id);
            
            return Ok();
        }

        [HttpGet("user-comments")]
        public async Task<IActionResult> UserComments()
        {
            var userCommentDtos = await _commentService.GetAllUserCommentsAsync();

            return Ok(userCommentDtos);
        }

        [HttpGet("member-comments/{id}")]
        public async Task<IActionResult> MemberComments(string id)
        {
            var userCommentDtos = await _commentService.GetCommentsByMemberIdAsync(id);

            return Ok(userCommentDtos);
        }

        [HttpGet("seller-comments/{id}")]
        public async Task<IActionResult> SellerComments(string id)
        {
            var userCommentDtos = await _commentService.GetCommentsBySellerIdAsync(id);

            return Ok(userCommentDtos);
        }

        [HttpGet("product-comments/{id}")]
        public async Task<IActionResult> ProductComments(string id)
        {
            var userCommentDtos = await _commentService.GetCommentsByProductIdAsync(id);

            return Ok(userCommentDtos);
        }
    }
}
