using System.ComponentModel.DataAnnotations;

namespace ScrumApp__Juro_.Models.Entities
{
    public class Developer
    {
        public int DeveloperID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string GoogleID { get; set; }
        // Foreign Key

    }
}
