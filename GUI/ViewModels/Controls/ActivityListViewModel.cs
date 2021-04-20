using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using GUI.Models;
using GUI.Models.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace GUI.ViewModels.Controls
{
    public class ActivityListViewModel : ViewModelBase
    {
        [Reactive]
        public ITrackerTask? Task { get; set; }

        public ObservableCollection<ActivityViewModel> Activities { get; } =
            new();

        [Reactive]
        public ActivityViewModel? SelectedActivity { get; set; }

        public ActivityListViewModel()
        {
            if (Design.IsDesignMode) PopulateDesignTimeValues();

            this.WhenAnyValue(x => x.Task).Subscribe(SetActivitiesFromTask);
        }

        private void SetActivitiesFromTask(ITrackerTask? task)
        {
            Activities.Clear();

            if (task?.Activities is null)
            {
                return;
            }

            foreach (var activity in task.Activities)
            {
                Activities.Add(new ActivityViewModel(activity));
            }
        }

        private void PopulateDesignTimeValues()
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
