using System;
using System.Windows.Threading;

namespace MastersProject.App.Infrastructure.Mvvm
{
    internal class ViewModelBase : PropertyChangedBase
    {
        protected void OnUiThread(Action action)
        {
            Dispatcher.CurrentDispatcher.Invoke(action);
        }
    }
}
