using System.Security.Cryptography.X509Certificates;

namespace MastersProject.SerialCommunicator
{
    public interface ISerialCommunicator<TData>
    {
        IReadOnlyCollection<Exception> Errors { get; }

        event EventHandler<Exception>? ErrorOccured;

        event EventHandler<TData>? SynchronousReceived;

        void Setup(string portName, int baudRate);

        string[] GetPortNames(); 

        void StartAsync(Action<TData> callback);

        void StopAsync();

        void Start();
        void Start(Action<TData>? callback);

        void Stop();

        void SetBaudRate(int newBaudRate);

        void SetPortName(string newPortName);

        void Restart();
    }
}
