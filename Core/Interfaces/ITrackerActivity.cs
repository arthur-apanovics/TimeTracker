using System;

namespace Core.Interfaces
{
    public interface ITrackerActivityEntity
    {
        public int Id { get; }
        public string Description { get; }
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