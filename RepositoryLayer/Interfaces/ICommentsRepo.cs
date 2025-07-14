using BlogBuilder.Models;

namespace BlogBuilder.RepositoryLayer.Interfaces
{
    public interface ICommentsRepo
    {
        void AddComment(BLOG_COMMENTS comment);
        bool UpdateComment(BLOG_COMMENTS updatedComment);

        bool DeleteComment(int commentId);
    }
}
