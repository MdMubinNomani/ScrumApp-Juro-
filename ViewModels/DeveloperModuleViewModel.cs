using ScrumApp__Juro_.Models.Entities;

namespace ScrumApp__Juro_.ViewModels
{
    public class DeveloperModuleViewModel
    {
        public Developer developer { get; set; }
        public List<Module> modules { get; set; }
        public List<SubModule> submodules { get; set; }
        public List<ScrumApp__Juro_.Models.Entities.Task> tasks { get; set; }
    }
}
