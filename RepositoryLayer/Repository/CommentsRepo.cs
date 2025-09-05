using BlogBuilder.Models;
using BlogBuilder.RepositoryLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogBuilder.RepositoryLayer.Repository
{
    public class CommentsRepo : ICommentsRepo
    {
        private readonly BLOG_PROJECTContext _context;
        public CommentsRepo(BLOG_PROJECTContext context)
        {
            _context = context;
        }

        public async Task AddComment(BLOG_COMMENTS comment)
        {
            await _context.BLOG_COMMENTS.AddAsync(comment);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> UpdateComment(BLOG_COMMENTS updatedComment)
        {
            var existingComment = await _context.BLOG_COMMENTS
                                        .FirstOrDefaultAsync(c => c.COMMENTID == updatedComment.COMMENTID);

            if (existingComment == null)
                return false;

            existingComment.COMMENT = updatedComment.COMMENT;
            existingComment.MODIFIED_DATE = updatedComment.MODIFIED_DATE;
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DeleteComment(int commentId)
        {
            var entity =await _context.BLOG_COMMENTS.FindAsync(commentId);

            if (entity == null)
                return false;

            _context.BLOG_COMMENTS.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BLOG_COMMENTS>> GetCommentsByBlogId(int blogId, int pageNumber)
        {
            return await _context.BLOG_COMMENTS
                    .Where(c => c.BLOGID == blogId)
                    .OrderByDescending(c => c.MODIFIED_DATE)
                    .Skip((pageNumber-1)*10)
                    .Take(10)
                    .ToListAsync();
        }
    }
}
