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
        public IActionResult AddComment([FromBody] CommentsDTO commentsDTO)
        {
            if (commentsDTO == null || string.IsNullOrWhiteSpace(commentsDTO.COMMENT))
                return BadRequest("Invalid comment data.");

            _commentsServices.AddComment(commentsDTO);
            return Ok();
        }

        [HttpPost]
        public IActionResult UpdateComment(CommentsDTO commentsDTO)
        {
            _commentsServices.UpdateComment(commentsDTO);
            return View();
        }

        public IActionResult DeleteComment(int commentId)
        {
            _commentsServices.DeleteComment(commentId);
            return View();
        }
    }
}
