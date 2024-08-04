using System.Diagnostics;
using System.IO.Ports;

namespace MastersProject.Serial.SerialWrapper
{
    public class SerialWrapper : ISerialWrapper
    {
        private readonly SerialPort _serialPort;

        public SerialWrapper()
        {
            _serialPort = new SerialPort();
            _serialPort.DataReceived += SerialPortOnDataReceived;
        }

        private void SerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var data = _serialPort.ReadLine();
            Debug.WriteLine(data);
            DataReceived?.Invoke(this, new DataReceivedEventArgs(data));
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
