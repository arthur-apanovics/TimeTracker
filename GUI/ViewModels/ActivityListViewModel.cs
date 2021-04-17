using System;
using System.Collections.ObjectModel;
using GUI.Models;
using GUI.Models.Interfaces;
using ReactiveUI.Fody.Helpers;

namespace GUI.ViewModels
{
    public class ActivityListViewModel : ViewModelBase
    {
        private readonly ITrackerTask? _task;
        private bool _isTaskSelected;

        public ObservableCollection<ActivityViewModel> Activities { get; } =
            new();

        [Reactive]
        public bool IsTaskSelected { get; set; }

        public ActivityListViewModel(ITrackerTask? task)
        {
            _task = task;

            foreach (var activity in _task?.Activities)
            {
                Activities.Add(new ActivityViewModel(activity));
            }
        }

        public ActivityListViewModel()
        {
            var descriptions = new[]
                {"Activity One", "Activity Two", "Activity Three"};

            foreach (var description in descriptions)
            {
                Activities.Add(
                    new ActivityViewModel(
                        new TrackerActivity(null)
                        {
                            Description = description,
                            DateStart = DateTime.Now - TimeSpan.FromMinutes(84),
                            DateEnd = DateTime.Now
                        }
                    )
                );
            }
        }
    }
}
