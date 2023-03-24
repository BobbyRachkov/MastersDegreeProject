namespace MastersProject.SerialCommunicator.SerialWrapper;

public interface ISerialWrapper
{
    bool IsOpen { get; }
    int BaudRate { get; set; }
    string PortName { get; set; }

    void Open();

    void Close();

    string ReadLine();

    void WriteLine(string text);
    
    event EventHandler<DataReceivedEventArgs> DataReceived;
}