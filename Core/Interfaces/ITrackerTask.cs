using System;
using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
    public interface ITrackerTaskEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<TrackerActivity> Activities { get; }
    }
    
    public interface ITrackerTask : ITrackerTaskEntity
    {
        public TimeSpan TotalTime { get; }
        public TrackerTask Rename(string newName);
        public TrackerActivity GetActivity(string desc);
        public TrackerActivity GetActivity(int id);
        public TrackerTask AddActivity(ITrackerActivity activity);
        public TrackerTask RemoveActivity(int id);
    }
}