using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MastersProject.App.Infrastructure
{
    internal class ViewModelBase:PropertyChangedBase
    {
        protected void OnUiThread(Action action)
        {
            Dispatcher.CurrentDispatcher.Invoke(action);
        }
    }
}
