using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ITrackerTaskEntity
    {
        public int Id { get; }
        public string Title { get; }
        public List<TrackerActivityEntity> Activities { get; }
    }
    
    public interface ITrackerTask : ITrackerTaskEntity
    {
        public IList<ITrackerActivity> Activities { get; }
        public TimeSpan TotalTime { get; }
        public ITrackerTask Rename(string newName);
        public ITrackerActivity GetActivity(string desc);
        public ITrackerTask AddActivity(ITrackerActivity activityEntity);
        public ITrackerTask DeleteActivity(ITrackerActivity activityEntity);
        public ITrackerTask TransferActivity(ITrackerActivity activityEntity, ITrackerTask targetTaskEntity);
    }
}