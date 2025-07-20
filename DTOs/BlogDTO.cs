using BlogBuilder.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogBuilder.DTOs
{
    public class BlogDTO
    {
        public int BLOGID { get; set; }

        public int? USERID { get; set; }

        public string BLOG_NAME { get; set; }
        public string TOPIC_NAME { get; set; }

        public string BLOG_CONTENT { get; set; }

        public byte[] IMAGE_DATA { get; set; }

        public DateOnly? MODIFIED_DATE { get; set; }

        public bool isUpdated { get; set; }

        public  List<CommentsDTO> BLOG_COMMENTS { get; set; } = new List<CommentsDTO>();
    }
}
