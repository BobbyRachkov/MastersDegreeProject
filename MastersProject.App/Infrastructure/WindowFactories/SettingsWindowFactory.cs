using MastersProject.App.WindowBases;
using System.Windows;

namespace MastersProject.App.Infrastructure.WindowFactories;

internal class SettingsWindowFactory : DefaultWindowFactory
{
    public override Window Create()
    {
        return new DefaultWindow()
        {
            Title = "Settings",
            Width = 1200,
            Height = 700,
            ResizeMode = ResizeMode.NoResize,
            WindowStyle = WindowStyle.ToolWindow,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };
    }

}