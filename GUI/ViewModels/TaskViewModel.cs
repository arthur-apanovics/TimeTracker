using System;
using GUI.Models;
using GUI.Models.Interfaces;

namespace GUI.ViewModels
{
    public class TaskViewModel : ViewModelBase
    {
        private readonly ITrackerTask _task;

        public string Title => _task.Title;
        public TimeSpan Duration => _task.TotalTime;

        public int ActivitiesCount => _task.Activities.Count;

        public TaskViewModel(ITrackerTask task)
        {
            _task = task;
        }

        public TaskViewModel()
        {
            _task = new TrackerTask(null)
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
