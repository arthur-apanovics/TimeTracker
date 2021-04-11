using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;

namespace GUI.Views
{
    public class AcrylicMaterial : UserControl
    {
        public AcrylicMaterial()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            var win = GetParentWindow();
            if (win.ExtendClientAreaChromeHints ==
                ExtendClientAreaChromeHints.Default)
            {
                win.ExtendClientAreaChromeHints =
                    ExtendClientAreaChromeHints.PreferSystemChrome |
                    ExtendClientAreaChromeHints.OSXThickTitleBar;
            }

            win.ExtendClientAreaToDecorationsHint = true;
            win.ExtendClientAreaTitleBarHeightHint = -1;
            win.TransparencyLevelHint = WindowTransparencyLevel.AcrylicBlur;
            win.Background = null;
        }

        private Window GetParentWindow()
        {
            IControl parent = Parent;
            while (parent is not Window)
            {
                parent = parent.Parent;
            }

            return (Window) parent;
        }
    }
}
