using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastersProject.App.Infrastructure.Interfaces
{
    internal interface ICanHideShow
    {
        public event Action Hide;

        public event Action Show;
    }
}
