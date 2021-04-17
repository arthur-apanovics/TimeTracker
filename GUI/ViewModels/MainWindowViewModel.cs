using ReactiveUI.Fody.Helpers;
using Splat;

namespace GUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ActivityListViewModel? _activityListViewModel;
        public TaskListViewModel TaskListViewModel { get; }

        [Reactive]
        public ActivityListViewModel? ActivityListViewModel
        {
            get;
            private set;
        }

        public MainWindowViewModel(TaskListViewModel taskListViewModel)
        {
            TaskListViewModel = taskListViewModel;
        }

        public MainWindowViewModel()
        {
            TaskListViewModel =
                Locator.Current.GetRequiredService<TaskListViewModel>();
        }
    }
}
