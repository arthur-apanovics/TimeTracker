using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GUI.Views.Controls
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