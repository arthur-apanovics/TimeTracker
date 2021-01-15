using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.API.Data
{
    public class TrackerContext : DbContext
    {
        private static TrackerContext _instance;

        public static TrackerContext Instance
        {
            get => _instance ??= (Instance = new TrackerContext());
            private set => _instance = value;
        }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<TrackerTask>()
            //     .HasMany<TrackerActivity>()
            //     .WithOne(a => a.Task)
            //     .OnDelete(DeleteBehavior.Cascade);
        }
    }
}