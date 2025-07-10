using BlogBuilder.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BlogBuilder.DTOs
{
    public class UserDTO
    {
        public string USERNAME { get; set; }

        public string PASSWORD { get; set; }

        public string EMAIL { get; set; }
        public virtual List<BlogDTO> BLOG { get; set; } = new List<BlogDTO>();
    }
}
