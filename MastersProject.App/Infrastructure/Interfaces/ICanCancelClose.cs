using System.ComponentModel;

namespace MastersProject.App.Infrastructure.Interfaces
{
    internal interface ICanCancelClose
    {
        void OnClosing(CancelEventArgs e);
    }
}
