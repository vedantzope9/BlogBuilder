using BlogBuilder.BusinessLayer.Interfaces;
using BlogBuilder.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlogBuilder.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsServices _commentsServices;

        public CommentsController(ICommentsServices commentsServices)
        {
            _commentsServices = commentsServices;
        }

        [HttpPost]
        public IActionResult AddComment(CommentsDTO commentsDTO)
        {
            _commentsServices.AddComment(commentsDTO);
            return View();
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
