using Microsoft.EntityFrameworkCore;
using EnglishLearning.Api.Entities;

namespace EnglishLearning.Api.Data
{
    public class EnglishLearningDbContext : DbContext
	{
        public EnglishLearningDbContext(DbContextOptions<TopicDbContext> options) : base(options)
        {

        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}