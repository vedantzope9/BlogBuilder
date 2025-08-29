using BlogBuilder.DTOs;
using BlogBuilder.Models;

namespace BlogBuilder.BusinessLayer.Interfaces
{
    public interface ICommentsServices
    {
        Task<CommentsDTO> AddComment(CommentsDTO commentsDTO);
        Task<CommentsDTO> UpdateComment(CommentsDTO commentsDTO);

        Task<bool> DeleteComment(int commentId);
    }
}
