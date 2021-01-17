using System;
using System.Collections.Generic;

namespace API.Models.Interfaces
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
        public TrackerTask DeleteActivity(int id);
    }
}
