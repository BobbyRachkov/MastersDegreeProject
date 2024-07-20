using System;

namespace MastersProject.App.Infrastructure.Interfaces
{
    internal interface ICanHideShow
    {
        public event Action Hide;

        public event Action Show;
    }
}
