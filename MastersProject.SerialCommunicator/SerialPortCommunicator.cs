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
        private Action<TData>? _synchronousCallback;
        private const int DefaultBaudRate = 9600;
        public SerialPortCommunicator(IObjectTranslator<TData> translator)
        {
            _serialPort = new SerialPort();
            _translator = translator;
            _errors = new();
        }
        public IReadOnlyCollection<Exception> Errors => _errors;
        public event EventHandler<Exception>? ErrorOccured;
        public event EventHandler<TData>? SynchronousReceived;
        public string[] GetPortNames() => SerialPort.GetPortNames();

        public void Setup(string portName, int baudRate)
        {
            SetPortName(portName);
            SetBaudRate(baudRate);
        }

        public void StartAsync(Action<TData> callback)
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
                    Read(callback);
                }
            });

            _serialPort.Open();
            _otherThread.Start();
        }

        public void StopAsync()
        {
            if (!_isAsyncRunning)
            {
                return;
            }

            _isAsyncRunning = false;
            _otherThread?.Join();
            _serialPort.Close();
        }

        private TData? Read(Action<TData>? callback)
        {
            try
            {
                var line = _serialPort.ReadLine();
                var data = _translator.Translate(line);
                if (callback is not null)
                {
                    callback(data);
                }

                return data;
            }
            catch (Exception ex)
            {
                _errors.Add(ex);
                ErrorOccured?.Invoke(this, ex);
                return default;
            }
        }

        public void Start()
        {
            Start(null);
        }

        public void Start(Action<TData>? callback)
        {
            if (_isAsyncRunning)
            {
                throw new InvalidOperationException("Synchronous read cannot be started while asynchronous read running!");
            }

            if (_isSyncRunning)
            {
                return;
            }

            _synchronousCallback = callback;
            _serialPort.DataReceived += SerialPort_DataReceived;
            _serialPort.Open();
        }
        public void Stop()
        {
            if (!_isSyncRunning)
            {
                return;
            }

            _isSyncRunning = false;
            _serialPort.DataReceived -= SerialPort_DataReceived;
            _serialPort.Close();
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var data = Read(_synchronousCallback);
            if (data is not null)
            {
                SynchronousReceived?.Invoke(this, data);
            }
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

        public void SetPortName(string newPortName)
        {
            _serialPort.PortName = newPortName;
        }

        public void Restart()
        {
            var oldBaudRate = _serialPort.BaudRate;
            _serialPort.BaudRate = DefaultBaudRate;
            SetBaudRate(oldBaudRate);
        }
    }
}
