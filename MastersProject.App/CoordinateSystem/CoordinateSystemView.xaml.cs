using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using MastersProject.App.CoordinateSystem;

namespace MastersProject.App.CoordinateSystem
{
    /// <summary>
    /// Interaction logic for CoordinateSystemView.xaml
    /// </summary>
    public partial class CoordinateSystemView: UserControl
    {
        public CoordinateSystemView()
        {
            InitializeComponent();
            this.SizeChanged += CoordinateSystemView_SizeChanged;
        }

        private void CoordinateSystemView_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            var context = (CoordinateSystemViewModel) DataContext;
            context.CanvasHeight = e.NewSize.Height;
            context.CanvasWidth = e.NewSize.Width;
        }
    }
}
