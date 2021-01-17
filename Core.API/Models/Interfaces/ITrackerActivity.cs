using System;

namespace Core.API.Models.Interfaces
{
    public interface ITrackerActivityEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
    
    public interface ITrackerActivity : ITrackerActivityEntity
    {
        public TimeSpan TimeSpent { get; }
        public DateTime StartTracking();
        public DateTime StopTracking();
    }
}
