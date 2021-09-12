using Microsoft.EntityFrameworkCore;

namespace WhoWantsToBeAMillionaire.Models
{
    public class MillionaireDbContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public MillionaireDbContext(DbContextOptions<MillionaireDbContext> options)
            : base(options) { }
    }
}
