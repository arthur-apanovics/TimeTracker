using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using GUI.ViewModels;

namespace GUI.Views
{
    public class TaskListView : ViewBase<TaskListViewModel>
    {
        public TaskListView()
        {
            AvaloniaXamlLoader.Load(this);
            base.InitializeComponent();
        }

        protected override void RegisterEvents(CompositeDisposable disposables)
        {
            this.Events()
                .KeyDown.Where(args => args.Key == Key.Escape)
                .Subscribe(_ => ViewModel.SelectedTask = null)
                .DisposeWith(disposables);
        }
    }
}
