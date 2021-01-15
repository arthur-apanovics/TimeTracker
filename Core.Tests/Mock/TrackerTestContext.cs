using Core.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Core.Tests.Mock
{
    public class TrackerTestContext : TrackerContext
    {
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder
        )
        {
            optionsBuilder.UseSqlite("Data Source=time-tracker-test.db");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}