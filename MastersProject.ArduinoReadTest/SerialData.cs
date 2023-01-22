using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastersProject.ArduinoReadTest
{
    internal sealed class SerialData
    {
        public int Pitch { get; set; }
        public int Roll { get; set; }
        public long Timestamp { get; set; }
        public int Index { get; set; }

        public SerialData()
        {

        }

        public SerialData(string line)
        {
            string[] parts = line.Trim().Split(';');
            Pitch = int.Parse(parts[1]);
            Roll = int.Parse(parts[2]);
            Timestamp = int.Parse(parts[3]);
            Index = int.Parse(parts[4]);
        }

        public override string ToString()
        {
            return $"Index: {Index:00000}; Pitch: {Pitch:0000}; Roll: {Roll:0000}; Timestamp: {Timestamp}";
        }
    }
}
