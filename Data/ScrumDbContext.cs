using Microsoft.EntityFrameworkCore;
using ScrumApp__Juro_.Models.Entities;
using Task = ScrumApp__Juro_.Models.Entities.Task;

namespace ScrumApp__Juro_.Data
{
    public class ScrumDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<SubModule> SubModules { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Developer> Developers { get; set; }

        public ScrumDbContext(DbContextOptions<ScrumDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // Enable Cascade Delete
        {
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Modules)
                .WithOne(m => m.Project)
                .HasForeignKey(m => m.ProjectID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Module>()
                .HasMany(m => m.SubModules)
                .WithOne(s => s.Module)
                .HasForeignKey(s => s.ModuleID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubModule>()
                .HasMany(s => s.Tasks)
                .WithOne(t => t.SubModule)
                .HasForeignKey(t => t.SubModuleID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Manager>()
                .HasMany(m => m.Projects)
                .WithOne()
                .HasForeignKey(p => p.ManagerID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
