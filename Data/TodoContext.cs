using Microsoft.EntityFrameworkCore;
using Model;

namespace Data
{
    public class TodoContext : DbContext
    {
        public DbSet<Answer> Answers => Set<Answer>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Subject> Subjects => Set<Subject>();

        public TodoContext (DbContextOptions<TodoContext> options)
            : base(options)
        {
            // Den her er tom. Men ": base(options)" sikre at constructor
            // på DbContext super-klassen bliver kaldt.
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Et eksempel på at man selv kan styre hvad en tabel skal hedde.
            modelBuilder.Entity<Subject>().ToTable("Subjects");
              modelBuilder.Entity<Answer>().ToTable("Answers");
                modelBuilder.Entity<Question>().ToTable("Questions");
        }
    }
}