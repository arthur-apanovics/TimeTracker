using System;
using ReactiveUI;
using Splat;

namespace GUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public TaskListViewModel TaskListViewModel { get; }

        public ActivityListViewModel? ActivityListViewModel { get; }

        public MainWindowViewModel(
            TaskListViewModel taskListViewModel,
            ActivityListViewModel activityListViewModel
        )
        {
            TaskListViewModel = taskListViewModel;
            ActivityListViewModel = activityListViewModel;

            this.WhenAnyValue(x => x.TaskListViewModel.SelectedTask)
                .Subscribe(tvm => ActivityListViewModel.Task = tvm?.Task);
        }

        public MainWindowViewModel()
        {
            TaskListViewModel =
                Locator.Current.GetRequiredService<TaskListViewModel>();
        }
    }
}
