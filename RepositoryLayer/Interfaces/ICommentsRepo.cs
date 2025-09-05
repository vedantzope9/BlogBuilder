using BlogBuilder.Models;

namespace BlogBuilder.RepositoryLayer.Interfaces
{
    public interface ICommentsRepo
    {
        Task AddComment(BLOG_COMMENTS comment);
        Task<bool> UpdateComment(BLOG_COMMENTS updatedComment);

        Task<bool> DeleteComment(int commentId);

        Task<List<BLOG_COMMENTS>> GetCommentsByBlogId(int blogId, int pageNumber);
    }
}
