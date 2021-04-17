using ReactiveUI;
using Splat;

namespace GUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ActivityListViewModel? _activityListViewModel;
        public TaskListViewModel TaskListViewModel { get; }

        public ActivityListViewModel? ActivityListViewModel
        {
            get => _activityListViewModel;
            private set =>
                this.RaiseAndSetIfChanged(ref _activityListViewModel, value);
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
