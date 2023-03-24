using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastersProject.SerialCommunicator.SerialWrapper
{
    internal class SerialWrapper : ISerialWrapper
    {
        private readonly SerialPort _serialPort;

        public SerialWrapper()
        {
            _serialPort = new SerialPort();
            _serialPort.DataReceived += SerialPortOnDataReceived;
        }

        private void SerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            DataReceived?.Invoke(this, new DataReceivedEventArgs(_serialPort.ReadLine()));
        }

        public bool IsOpen => _serialPort.IsOpen;

        public int BaudRate
        {
            get => _serialPort.BaudRate;
            set => _serialPort.BaudRate = value;
        }

        public string PortName
        {
            get => _serialPort.PortName;
            set => _serialPort.PortName = value;
        }

        public void Open() => _serialPort.Open();

        public void Close() => _serialPort.Close();

        public string ReadLine() => _serialPort.ReadLine();

        public void WriteLine(string text) => _serialPort.WriteLine(text);

        public event EventHandler<DataReceivedEventArgs>? DataReceived;
    }
}
