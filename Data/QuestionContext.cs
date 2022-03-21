using Microsoft.EntityFrameworkCore;
using Model;

namespace Data
{
	public class QuestionContext : DbContext
	{
		public DbSet<Question> Questions => Set<Question>();
		public DbSet<Subject> Subjects => Set<Subject>();
		public DbSet<Answer> Answers => Set<Answer>();

		public QuestionContext (DbContextOptions<QuestionContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Question>().ToTable("Questions");
			modelBuilder.Entity<Subject>().ToTable("Subjects");
			modelBuilder.Entity<Answer>().ToTable("Answers");
		}

	}
}