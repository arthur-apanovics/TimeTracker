using System;
using GUI.Models;
using GUI.Models.Interfaces;

namespace GUI.ViewModels.Controls
{
    public class ActivityViewModel : ViewModelBase
    {
        private ITrackerActivity _activity;

        public string Description => _activity.Description;

        public string TimeSpent =>
            $"{_activity.TimeSpent.ToString(@"hh\:mm\:ss")}";

        public ActivityViewModel(ITrackerActivity activity)
        {
            _activity = activity;
        }

        public ActivityViewModel()
        {
            _activity = new TrackerActivity(null)
            {
                Description = "Design time activity",
                DateStart = DateTime.Now - TimeSpan.FromMinutes(105),
                DateEnd = DateTime.Now
            };
        }
    }
}
