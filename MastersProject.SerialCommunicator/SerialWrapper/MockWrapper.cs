using System.IO.Ports;
using System.Runtime.InteropServices;

namespace MastersProject.SerialCommunicator.SerialWrapper;

public class MockWrapper : ISerialWrapper
{
    private Task? _asyncProducerTask;
    private CancellationTokenSource? _cancellationTokenSource;
    private string _currentData;

    private const int BoundaryX = 1023;
    private const int BoundaryY = 1023;

    public MockWrapper()
    {
        PortName = "Mock COM1";
        _currentData = "0;0";
    }

    public bool IsOpen { get; private set; }
    public int BaudRate { get; set; }
    public string PortName { get; set; }
    public void Open()
    {
        IsOpen = true;
        _cancellationTokenSource = new CancellationTokenSource();
        _asyncProducerTask = Task.Run(ProduceAsync, _cancellationTokenSource.Token);
    }

    public void Close()
    {
        IsOpen = false;
        _cancellationTokenSource?.Cancel();
    }

    public string ReadLine() => _currentData;

    public void WriteLine(string text)
    {

    }

    public event EventHandler<DataReceivedEventArgs>? DataReceived;


    private void ProduceAsync()
    {
        int delay = 2;
        while (true)
        {
            for (int i = BoundaryY / 2; i <= BoundaryY; i++)
            {
                _currentData = $";{i - (BoundaryX / 2)};{i};{DateTime.Now.Millisecond};{i}";
                DataReceived?.Invoke(this, new(_currentData));
                Task.Delay(delay).Wait();
            }
            for (int i = BoundaryY; i >= BoundaryY / 2; i--)
            {
                _currentData = $";{i};{i - (BoundaryX / 2)};{DateTime.Now.Millisecond};{i}";
                DataReceived?.Invoke(this, new(_currentData));
                Task.Delay(delay).Wait();
            }
        }
    }
}