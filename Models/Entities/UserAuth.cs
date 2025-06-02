using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

namespace ScrumApp__Juro_.Models.Entities
{
    public class UserAuth
    {
        public int UserAuthID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
