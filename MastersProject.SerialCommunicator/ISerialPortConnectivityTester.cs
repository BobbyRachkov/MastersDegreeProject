namespace MastersProject.Serial;

public interface ISerialPortConnectivityTester
{
    bool TryConnect(string portName, CancellationToken cancellationToken);
}