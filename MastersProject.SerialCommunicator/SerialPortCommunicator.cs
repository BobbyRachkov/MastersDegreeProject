using System.IO.Ports;

namespace MastersProject.SerialCommunicator
{
    public class SerialPortCommunicator<TData> : ISerialCommunicator<TData>
    {
        private readonly IObjectTranslator<TData> _translator;
        private readonly SerialPort _serialPort;
        private Thread? _otherThread;
        private bool _isAsyncRunning = false;
        private bool _isSyncRunning = false;
        private readonly List<Exception> _errors;
        private const int DefaultBaudRate = 9600;

        public SerialPortCommunicator(IObjectTranslator<TData> translator)
        {
            _serialPort = new SerialPort();
            _translator = translator;
            _errors = new();
        }

        public IReadOnlyCollection<Exception> Errors => _errors;
        public event EventHandler<Exception>? ErrorOccured;
        public event EventHandler<TData>? DataReceived;

        public string[] GetPortNames() => SerialPort.GetPortNames();

        public bool TrySetup(string portName, int baudRate)
        {
            var res = TrySetPortName(portName);
            SetBaudRate(baudRate);
            return res;
        }

        public void StartAsync()
        {
            if (_isSyncRunning)
            {
                throw new InvalidOperationException("Asynchronous read cannot be started while synchronous read running!");
            }

            if (_isAsyncRunning)
            {
                return;
            }

            _isAsyncRunning = true;
            _otherThread = new Thread(() =>
            {
                while (_isAsyncRunning)
                {
                    Read();
                }
            });

            _serialPort.Open();
            _otherThread.Start();
        }

        public void Stop()
        {
            if (_isAsyncRunning)
            {
                _isAsyncRunning = false;
                _otherThread?.Join();
                _serialPort.Close();
                return;
            }

            _isSyncRunning = false;
            _serialPort.Close();
        }

        private TData? Read()
        {
            try
            {
                var line = _serialPort.ReadLine();
                var data = _translator.Translate(line);
                DataReceived?.Invoke(this, data);

                return data;
            }
            catch (Exception ex)
            {
                RaiseError(ex);
                return default;
            }
        }

        private void RaiseError(Exception ex)
        {
            _errors.Add(ex);
            ErrorOccured?.Invoke(this, ex);
        }

        public void Start()
        {
            if (_isAsyncRunning)
            {
                throw new InvalidOperationException("Synchronous read cannot be started while asynchronous read running!");
            }

            if (_isSyncRunning)
            {
                return;
            }

            _serialPort.Open();
        }

        public void SetBaudRate(int newBaudRate)
        {
            var wasClosed = !_serialPort.IsOpen;
            if (wasClosed)
            {
                _serialPort.Open();
            }
            _serialPort.WriteLine(newBaudRate.ToString());
            if (wasClosed)
            {
                _serialPort.Close();
            }
            _serialPort.BaudRate = newBaudRate;
        }

        public bool TrySetPortName(string newPortName)
        {
            var wasOpen = _serialPort.IsOpen;
            if (wasOpen)
            {
                _serialPort.Close();
            }
            _serialPort.PortName = newPortName;

            try
            {
                _serialPort.Open();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(IOException))
                {
                    Stop();
                    return false;
                }

                Stop();
                RaiseError(ex);
                return false;
            }
            if (!wasOpen)
            {
                _serialPort.Close();
            }
            int oldBaudRate = _serialPort.BaudRate;
            _serialPort.BaudRate = DefaultBaudRate;
            SetBaudRate(oldBaudRate);
            return true;
        }

        public void Restart()
        {
            var oldBaudRate = _serialPort.BaudRate;
            _serialPort.BaudRate = DefaultBaudRate;
            SetBaudRate(oldBaudRate);
        }
    }
}
