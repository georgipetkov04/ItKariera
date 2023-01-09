using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_7.Data;

namespace Project_7.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ToDoList>()
            .HasMany(tdl => tdl.Items)
            .WithOne(tdi => tdi.ToDoList)
            .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }
        public DbSet<Project_7.Data.ToDoList> ToDoList { get; set; }
        public DbSet<Project_7.Data.ToDoItem> ToDoItem { get; set; }
    }
}