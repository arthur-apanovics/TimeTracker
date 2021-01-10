using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Data
{
    public class AppContext : DbContext
    {
        private static AppContext _instance;

        public static AppContext Instance
        {
            get => _instance ??= (Instance = new AppContext());
            private set => _instance = value;
        }

        public DbSet<TrackerTask> Tasks { get; set; }
        public DbSet<TrackerActivity> Activities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=time-tracker.db");
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
#endif
        }
    }
}