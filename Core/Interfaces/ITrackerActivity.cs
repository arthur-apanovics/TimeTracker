using System;

namespace Core.Interfaces
{
    public interface ITrackerActivityEntity
    {
        public int Id { get; }
        public string Description { get; }
        public DateTime DateStart { get; }
        public DateTime DateEnd { get; }
    }
    
    public interface ITrackerActivity : ITrackerActivityEntity
    {
        public TimeSpan TimeSpent { get; }
        public DateTime StartTracking();
        public DateTime StopTracking();
    }
}