using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("ManagerID")]
        public virtual Manager Manager { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
    }

}
