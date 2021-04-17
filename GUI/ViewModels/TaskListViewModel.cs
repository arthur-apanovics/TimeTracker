using System.Collections.ObjectModel;
using GUI.Data;
using GUI.Models;
using ReactiveUI.Fody.Helpers;

namespace GUI.ViewModels
{
    public class TaskListViewModel : ViewModelBase
    {
        private readonly TrackerRepository _repository;
        public ObservableCollection<TaskViewModel> Tasks { get; } = new();

        [Reactive]
        public TaskViewModel? SelectedTask { get; set; }

        public TaskListViewModel(TrackerRepository repository)
        {
            _repository = repository;

            foreach (var task in _repository.GetAllTasks())
            {
                Tasks.Add(new TaskViewModel(task));
            }
        }

        public TaskListViewModel()
        {
            var titles = new[]
            {
                "Some Task", "Another task", "Bla bla task",
                "A very long task name that will definitely go off screen"
            };
            foreach (var title in titles)
            {
                Tasks.Add(
                    new TaskViewModel(
                        new TrackerTask(null)
                        {
                            Title = title,
                            Activities =
                            {
                                new TrackerActivity(null)
                                {
                                    Description = $"{title} - activity",
                                }
                            }
                        }
                    )
                );
            }
        }
    }
}
