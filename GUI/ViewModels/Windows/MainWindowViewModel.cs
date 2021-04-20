using System;
using GUI.ViewModels.Controls;
using ReactiveUI;
using Splat;

namespace GUI.ViewModels.Windows
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
