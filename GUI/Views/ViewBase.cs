using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Input;
using Avalonia.ReactiveUI;
using Avalonia.VisualTree;
using ReactiveUI;

namespace GUI.Views
{
    public class ViewBase<TViewModel> : ReactiveUserControl<TViewModel>
        where TViewModel : class
    {
        // public ViewConstructorBoilerplate()
        // {
        //     AvaloniaXamlLoader.Load(this);
        //     base.InitializeComponent();
        // }

        protected void InitializeComponent()
        {
            // calling "AvaloniaXamlLoader.Load" from base class does not work...

            // setting name here does NOT add control to scope
            this.Name = this.GetType().Name;
            this.WhenActivated(RegisterEvents);
        }

        protected virtual void RegisterEvents(CompositeDisposable disposables)
        {
            var elementEvents = this.Events();

            // focus main window
            elementEvents.KeyDown.Where(args => args.Key == Key.Escape)
                .Subscribe(_ => this.FindAncestorOfType<Window>().Focus())
                .DisposeWith(disposables);
        }
    }
}
