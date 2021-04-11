using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GUI.Views
{
    public class TaskView : UserControl
    {
        public TaskView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}