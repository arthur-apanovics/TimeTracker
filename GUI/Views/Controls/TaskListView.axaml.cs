using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using GUI.ViewModels;
using GUI.Views.Windows;
using ReactiveUI;

namespace GUI.Views.Controls
{
    public class TaskListView : ViewBase<TaskListViewModel>
    {
        public TaskListView()
        {
            AvaloniaXamlLoader.Load(this);
            base.InitializeComponent();

            this.WhenActivated(
                disposable => disposable(
                    ViewModel.TextBoxModalInteraction.RegisterHandler(
                        ShowDialogAsync
                    )
                )
            );
        }

        private async Task ShowDialogAsync(
            InteractionContext<TextBoxDialogViewModel, string?> interaction
        )
        {
            var dialog = new TextBoxDialogWindow
                {DataContext = interaction.Input};

            var owner = this.FindAncestorOfType<Window>();
            var result = await dialog.ShowDialog<string?>(owner);

            interaction.SetOutput(result);
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
