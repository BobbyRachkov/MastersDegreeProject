using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MastersProject.App.Infrastructure.Interfaces;
using MastersProject.App.WindowBases;

namespace MastersProject.App.Infrastructure.WindowFactories
{
    internal class DotSelectorFactory:DefaultWindowFactory
    {
        public override Window Create()
        {
            return new DefaultWindow()
            {
                Title = "Enter point coordinate",
                Width = 200,
                Height = 100,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.ToolWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
        }
    }
}
