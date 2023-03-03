using System.Security.Cryptography.X509Certificates;

namespace MastersProject.SerialCommunicator
{
    public interface ISerialCommunicator<TData>
    {
        IReadOnlyCollection<Exception> Errors { get; }


        event EventHandler<Exception>? ErrorOccured;


        event EventHandler<TData>? DataReceived;

        bool TrySetup(string portName, int baudRate);

        string[] GetPortNames(); 

        void StartAsync();

        void Start();

        void Stop();

        void SetBaudRate(int newBaudRate);

        bool TrySetPortName(string newPortName);

        void Restart();
    }
}
