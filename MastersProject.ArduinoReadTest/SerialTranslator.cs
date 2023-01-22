using MastersProject.SerialCommunicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
