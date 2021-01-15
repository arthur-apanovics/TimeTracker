using Core.Models;
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
            optionsBuilder.UseSqlite("Data Source=time-tracker.db");
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
#endif
        }
    }
}