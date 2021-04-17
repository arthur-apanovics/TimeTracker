using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GUI.Views
{
    public class ActivityListView : UserControl
    {
        public ActivityListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}