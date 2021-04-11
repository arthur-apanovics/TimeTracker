namespace GUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public TaskListViewModel TaskListViewModel { get; }

        public MainWindowViewModel(TaskListViewModel taskListViewModel)
        {
            TaskListViewModel = taskListViewModel;
        }

         public MainWindowViewModel()
         {

         }
    }
}
