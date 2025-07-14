using BlogBuilder.DTOs;
using BlogBuilder.Models;

namespace BlogBuilder.BusinessLayer.Interfaces
{
    public interface ICommentsServices
    {
        CommentsDTO AddComment(CommentsDTO commentsDTO);
        CommentsDTO UpdateComment(CommentsDTO commentsDTO);

        bool DeleteComment(int commentId);
    }
}
