using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MastersProject.Serial;

namespace MastersProject.ArduinoReadTest
{
    internal class SerialTranslator : IObjectTranslator<SerialData>
    {
        public SerialData Translate(string data)
        {
            return new SerialData(data);
        }
    }
}
