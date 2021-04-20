using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;

namespace GUI.ViewModels.Windows
{
    public class TextBoxDialogViewModel : ViewModelBase, IValidatableViewModel
    {
        [Reactive]
        public string? TextValue { get; set; }

        public ReactiveCommand<Unit, string?> ReturnTextValue { get; }

        public ValidationContext ValidationContext { get; } = new();

        public TextBoxDialogViewModel()
        {
            this.ValidationRule(
                x => x.TextValue,
                value => !string.IsNullOrWhiteSpace(value),
                "Please specify a value"
            );

            ReturnTextValue = ReactiveCommand.Create(
                execute: () => TextValue,
                canExecute: this.IsValid()
            );
        }
    }
}
