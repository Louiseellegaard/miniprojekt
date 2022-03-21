using Microsoft.EntityFrameworkCore;
using Model;

namespace Data
{
    public class QuestionContext : DbContext
    {
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Answer> Answers => Set<Answer>();
        public DbSet<Subject> Subjects => Set<Subject>();

        public QuestionContext (DbContextOptions<QuestionContext> options)
            : base(options)
        {
            // Den her er tom. Men ": base(options)" sikre at constructor
            // på DbContext super-klassen bliver kaldt.
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Et eksempel på at man selv kan styre hvad en tabel skal hedde.
            modelBuilder.Entity<Question>().ToTable("Questions");
        }
    }
}