using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GUI.Views.Controls
{
    public class ActivityView : UserControl
    {
        public ActivityView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}