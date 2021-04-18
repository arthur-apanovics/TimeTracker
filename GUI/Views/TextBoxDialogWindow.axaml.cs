using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.VisualTree;
using GUI.ViewModels;
using ReactiveUI;

namespace GUI.Views
{
    public class TextBoxDialogWindow : ReactiveWindow<TextBoxDialogViewModel>
    {
        public TextBoxDialogWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            // this.WhenActivated(
            //     disposables => ViewModel.ReturnTextValue
            //         .Subscribe(s => Close(s))
            //         .DisposeWith(disposables)
            // );

            this.WhenActivated(RegisterEvents);
        }

        /// <summary>
        /// Registers event sequences for this view
        /// </summary>
        /// <param name="disposables"></param>
        /// <see href="https://www.reactiveui.net/docs/handbook/events"/>
        protected virtual void RegisterEvents(CompositeDisposable disposables)
        {
            var elementEvents = this.Events();

            // esc
            elementEvents.KeyDown.Where(k => k.Key == Key.Escape)
                .Subscribe(_ => Close(null))
                .DisposeWith(disposables);

            // enter
            elementEvents.KeyDown.Where(k => k.Key == Key.Enter)
                .Select(args => Unit.Default)
                .InvokeCommand(ViewModel, x => x.ReturnTextValue)
                .DisposeWith(disposables);

            // call 'close' and pass value when dialog closes
            ViewModel.ReturnTextValue.Subscribe(Close).DisposeWith(disposables);
        }
    }
}
