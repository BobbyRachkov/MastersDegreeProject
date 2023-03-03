using MastersProject.App.Models;
using MastersProject.SerialCommunicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace MastersProject.App.Translators
{
    internal class DefaultTranslator : IObjectTranslator<SerialData>
    {
        public SerialData Translate(string data)
        {
            string[] parts = data.Trim().Split(';');
            return new()
            {
                Pitch = int.Parse(parts[1]),
                Roll = int.Parse(parts[2]),
                Timestamp = int.Parse(parts[3]),
                Index = int.Parse(parts[4])
            };
        }
    }
}
