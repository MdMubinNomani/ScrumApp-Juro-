using System.ComponentModel.DataAnnotations;

namespace ScrumApp__Juro_.Models.Entities
{
    public class Task
    {
        public int TaskID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int ProjectID { get; set; }
        public int SubModuleID { get; set; }
        public virtual SubModule SubModule { get; set; }
    }

}
