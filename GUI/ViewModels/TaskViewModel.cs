using System;
using GUI.Models;
using GUI.Models.Interfaces;

namespace GUI.ViewModels
{
    public class TaskViewModel : ViewModelBase
    {
        public ITrackerTask Task { get; }

        public string Title => Task.Title;
        public TimeSpan Duration => Task.TotalTime;

        public int ActivitiesCount => Task.Activities.Count;

        public TaskViewModel(ITrackerTask task)
        {
            Task = task;
        }

        public TaskViewModel()
        {
            Task = new TrackerTask(null)
            {
                Title = "Design time title",
                Activities =
                {
                    new TrackerActivity(null)
                    {
                        Description = "Activity 1",
                        DateStart = new DateTime(2021, 01, 01, 1, 0, 0),
                        DateEnd = new DateTime(2021, 01, 01, 4, 32, 12),
                    }
                }
            };
        }
    }
}
