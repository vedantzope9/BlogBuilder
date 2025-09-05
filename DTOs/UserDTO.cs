using BlogBuilder.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BlogBuilder.DTOs
{
    public class UserDTO
    {
        public required string USERNAME { get; set; }

        public required string PASSWORD { get; set; }

        public required string EMAIL { get; set; }
        public virtual List<BlogDTO> BLOG { get; set; } = new List<BlogDTO>();
    }
}
