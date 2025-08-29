using BlogBuilder.BusinessLayer.Interfaces;
using BlogBuilder.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogBuilder.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ICommentsServices _commentsServices;

        public CommentsController(ICommentsServices commentsServices)
        {
            _commentsServices = commentsServices;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CommentsDTO commentsDTO)
        {
            if (commentsDTO == null || string.IsNullOrWhiteSpace(commentsDTO.COMMENT))
                return BadRequest("Invalid comment data.");

            await _commentsServices.AddComment(commentsDTO);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateComment(CommentsDTO commentsDTO)
        {
            await _commentsServices.UpdateComment(commentsDTO);
            return View();
        }

        public async Task<IActionResult> DeleteComment(int commentId)
        {
            await _commentsServices.DeleteComment(commentId);
            return View();
        }
    }
}
