using ScrumApp__Juro_.Models.Entities;

namespace ScrumApp__Juro_.ViewModels
{
    public class ProjectsViewModel
    {
        public Project Project { get; set; }
        public List<Module> Modules { get; set; } = new List<Module>();
        public List<SubModule> SubModules { get; set; } = new List<SubModule>();    
        public List<Models.Entities.Task> Tasks { get; set; } = new List<Models.Entities.Task>();
        public List<Developer> Developers { get; set; } = new List<Developer>();

    }
}
