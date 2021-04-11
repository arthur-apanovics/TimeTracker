using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GUI.Views
{
    public class TaskListView : UserControl
    {
        public TaskListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
