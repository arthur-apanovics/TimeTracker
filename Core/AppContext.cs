using Core.Entities;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core
{
    public class AppContext : DbContext
    {
        private static AppContext _instance;

        public static AppContext Instance
        {
            get => _instance ??= (Instance = new AppContext());
            private set => _instance = value;
        }

        public DbSet<TrackerTaskEntity> Tasks { get; set; }
        public DbSet<TrackerActivityEntity> Activities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=time-tracker.db");
    }
}