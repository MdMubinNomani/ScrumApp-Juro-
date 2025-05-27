using System.ComponentModel.DataAnnotations;

namespace ScrumApp__Juro_.Models.Entities
{
    public class Project
    {
        public int ProjectID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //Foreign Key
        [Required]
        public int ManagerID { get; set; }

        public virtual ICollection<Module> Modules { get; set; }
    }

}
