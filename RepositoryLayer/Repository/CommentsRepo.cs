using BlogBuilder.Models;
using BlogBuilder.RepositoryLayer.Interfaces;

namespace BlogBuilder.RepositoryLayer.Repository
{
    public class CommentsRepo : ICommentsRepo
    {
        private readonly BLOG_PROJECTContext _context;
        public CommentsRepo(BLOG_PROJECTContext context)
        {
            _context = context;
        }

        public void AddComment(BLOG_COMMENTS comment)
        {
            _context.BLOG_COMMENTS.Add(comment);
            _context.SaveChanges();
        }


        public bool UpdateComment(BLOG_COMMENTS updatedComment)
        {
            var existingComment = _context.BLOG_COMMENTS
                                        .FirstOrDefault(c => c.COMMENTID == updatedComment.COMMENTID);

            if (existingComment == null)
                return false;

            existingComment.COMMENT = updatedComment.COMMENT;
            existingComment.MODIFIED_DATE = updatedComment.MODIFIED_DATE;
            _context.SaveChanges();
            return true;

        }

        public bool DeleteComment(int commentId)
        {
            var entity = _context.BLOG_COMMENTS.Find(commentId);

            if (entity == null)
                return false;

            _context.BLOG_COMMENTS.Remove(entity);
            _context.SaveChanges();
            return true;
        }
    }
}
