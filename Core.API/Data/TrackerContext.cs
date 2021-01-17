using Core.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.API.Data
{
    public class TrackerContext : DbContext
    {
        public DbSet<TrackerTask> Tasks { get; set; }
        public DbSet<TrackerActivity> Activities { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder
        )
        {
#if DEBUG
            optionsBuilder.UseSqlite("Data Source=time-tracker-seeded.db");
            optionsBuilder.EnableSensitiveDataLogging();
#else
            optionsBuilder.UseSqlite("Data Source=time-tracker.db");
#endif
        }
    }
}
