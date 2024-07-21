using System.Diagnostics;
using System.IO.Ports;

namespace MastersProject.Serial;

public class SerialPortConnectivityTester : ISerialPortConnectivityTester
{
    private readonly ManualResetEvent _event = new(false);
    private SerialPort? _serialPort;

    public bool TryConnect(string portName, CancellationToken cancellationToken)
    {
        _serialPort = new SerialPort(portName, 9600);
        _serialPort.DataReceived += SerialPortOnDataReceived;

        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            _serialPort.Open();
            Debug.WriteLine($"Opened {portName}");
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"Caught {portName} - {ex.Message}");
            _serialPort.Close();
            _serialPort.Dispose();
            Debug.WriteLine($"returning {portName}");
            return false;
        }

        cancellationToken.ThrowIfCancellationRequested();
        var isEventSuccessful = _event.WaitOne(TimeSpan.FromSeconds(4));
        Debug.WriteLine($"{portName} -> {isEventSuccessful}");

        _serialPort.Close();
        _serialPort.Dispose();
        return true;
    }

    private void SerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        //try
        //{
        //    Debug.WriteLine(_serialPort?.ReadLine());
        //}
        //catch
        //{
        //    return;
        //}

        _event.Set();
    }
}