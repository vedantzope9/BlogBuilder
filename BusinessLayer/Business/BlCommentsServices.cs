using BlogBuilder.BusinessLayer.Interfaces;
using BlogBuilder.DTOs;
using BlogBuilder.Models;
using BlogBuilder.RepositoryLayer.Interfaces;

namespace BlogBuilder.BusinessLayer.Business
{
    public class BlCommentsServices:ICommentsServices
    {
        private readonly ICommentsRepo _commentsRepo;

        public BlCommentsServices(ICommentsRepo commentsRepo)
        {
            _commentsRepo=commentsRepo;
        }

        public CommentsDTO AddComment(CommentsDTO commentsDTO)
        {
            try
            {
                BLOG_COMMENTS comment = new BLOG_COMMENTS
                {
                    BLOGID = commentsDTO.BLOGID,
                    USERID = commentsDTO.USERID,
                    COMMENT = commentsDTO.COMMENT,
                    MODIFIED_DATE = DateOnly.FromDateTime(DateTime.Now)
                };

                _commentsRepo.AddComment(comment);
                return commentsDTO;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public CommentsDTO UpdateComment(CommentsDTO commentsDTO)
        {
            try
            {
                BLOG_COMMENTS comment = new BLOG_COMMENTS
                {
                    COMMENTID = commentsDTO.COMMENTID,
                    BLOGID = commentsDTO.BLOGID,
                    USERID = commentsDTO.USERID,
                    COMMENT = commentsDTO.COMMENT,
                    MODIFIED_DATE = DateOnly.FromDateTime(DateTime.Now)
                };

                bool res = _commentsRepo.UpdateComment(comment);

                if (!res)
                    throw new Exception("Unable to Update Comment!");

                return commentsDTO;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteComment(int commentId)
        {
            try
            {
                bool res = _commentsRepo.DeleteComment(commentId);

                if (!res)
                    throw new Exception("Comment not found");

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
