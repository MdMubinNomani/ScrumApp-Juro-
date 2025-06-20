using System.ComponentModel.DataAnnotations;

namespace ScrumApp__Juro_.Models.Entities
{
    public class Module
    {
        public int ModuleID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int ProjectID { get; set; }
        public int DeveloperID { get; set; }
        public virtual Project Project { get; set; }

        public virtual ICollection<SubModule> SubModules { get; set; }
    }

}
