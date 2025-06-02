using System.ComponentModel.DataAnnotations;

namespace ScrumApp__Juro_.Models.Entities
{
    public class Manager
    {
        public int ManagerID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Username { get; set; }

        // Foreign Key
        public virtual ICollection<Project> Projects { get; set; }
    }
}
