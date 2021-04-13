using System.Collections.Generic;
using System.Collections.ObjectModel;
using GUI.Models;
using GUI.Models.Interfaces;

namespace GUI.ViewModels
{
    public class ActivityListViewModel
    {
        private readonly ITrackerTask? _task;

        public ObservableCollection<ActivityViewModel> Activities { get; } =
            new();

        public bool IsTaskSelected => _task is not null;

        public ActivityListViewModel(ITrackerTask? task)
        {
            _task = task;

            foreach (var activity in _task.Activities)
            {
                Activities.Add(new ActivityViewModel(activity));
            }
        }
    }

    public class ActivityViewModel
    {
        public ActivityViewModel(ITrackerActivity activity)
        {

        }
    }
}
