using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using GUI.Data;
using GUI.Models;
using GUI.ViewModels.Windows;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;

namespace GUI.ViewModels.Controls
{
    public class TaskListViewModel : ViewModelBase
    {
        private readonly TrackerRepository _repository;

        public ObservableCollection<TaskViewModel> Tasks { get; } = new();

        [Reactive]
        public TaskViewModel? SelectedTask { get; set; }

        public Interaction<TextBoxDialogViewModel, string?>
            TextBoxModalInteraction { get; }

        public ICommand CreateTask { get; }

        public TaskListViewModel(TrackerRepository repository)
        {
            _repository = repository;

            foreach (var task in _repository.GetAllTasks())
            {
                Tasks.Add(new TaskViewModel(task));
            }

            TextBoxModalInteraction =
                new Interaction<TextBoxDialogViewModel, string?>();

            CreateTask = ReactiveCommand.CreateFromTask(
                async () =>
                {
                    var viewModel = new TextBoxDialogViewModel();

                    var uniqueTaskNameObservable = viewModel
                        .WhenAnyValue(x => x.TextValue)
                        .Throttle(TimeSpan.FromMilliseconds(400))
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Select(x => _repository.GetTaskOrNull(x) is null);

                    viewModel.ValidationRule(
                        x => x.TextValue,
                        uniqueTaskNameObservable,
                        "Task with than name already exists"
                    );

                    var result =
                        await TextBoxModalInteraction.Handle(viewModel);

                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        _repository.CreateTask(result);
                    }
                }
            );
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
