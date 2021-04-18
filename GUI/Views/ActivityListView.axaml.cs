using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using GUI.ViewModels;

namespace GUI.Views
{
    public class ActivityListView : ViewBase<ActivityListViewModel>
    {
        public ActivityListView()
        {
            AvaloniaXamlLoader.Load(this);
            base.InitializeComponent();
        }

        protected override void RegisterEvents(CompositeDisposable disposables)
        {
            this.Events()
                .KeyDown.Where(args => args.Key == Key.Escape)
                .Subscribe(
                    _ =>
                    {
                        ViewModel.SelectedActivity = null;

                        // TODO: shift focus to selected task
                        // var focusTarget = this.FindAncestorOfType<MainWindow>()
                        //     .FindDescendantOfType<TaskListView>()
                        //     .FindDescendantOfType<ListBox>()
                        //     .SelectedItem;
                        // FocusManager.Instance.Focus(focusTarget);
                    }
                )
                .DisposeWith(disposables);
        }
    }
}
