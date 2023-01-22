using System;

namespace MastersProject.App.Infrastructure.Interfaces
{
    internal interface ICanClose
    {
        void OnClose(bool? dialogResult);

        event Action Close;
    }
}
