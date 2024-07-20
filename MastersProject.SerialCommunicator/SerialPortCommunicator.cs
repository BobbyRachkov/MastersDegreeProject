using System.IO.Ports;
using MastersProject.Serial.SerialWrapper;

namespace MastersProject.Serial
{
    public class SerialPortCommunicator<TData> : ISerialCommunicator<TData>
    {
        private readonly IObjectTranslator<TData> _translator;
        private readonly ISerialWrapper _serialPortProvider;
        private Task? _asyncTask;
        private CancellationTokenSource? _asyncTaskCancellationTokenSource;
        private bool _isAsyncRunning = false;
        private bool _isSyncRunning = false;
        private readonly List<Exception> _errors;
        private const int DefaultBaudRate = 9600;

        public SerialPortCommunicator(IObjectTranslator<TData> translator,ISerialWrapper wrapper)
        {
            _serialPortProvider = wrapper;
            _translator = translator;
            _errors = new();
        }

        public IReadOnlyCollection<Exception> Errors => _errors;
        public event EventHandler<Exception>? ErrorOccurred;
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
            _asyncTaskCancellationTokenSource=new CancellationTokenSource();
            _asyncTask = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Read();
                }
            },
                _asyncTaskCancellationTokenSource.Token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);

            _serialPortProvider.Open();
        }

        public void Stop()
        {
            if (_isAsyncRunning)
            {
                _isAsyncRunning = false;
                _asyncTaskCancellationTokenSource?.Cancel();
                _serialPortProvider.Close();
                return;
            }

            _isSyncRunning = false;
            _serialPortProvider.Close();
        }

        private TData? Read()
        {
            try
            {
                var line = _serialPortProvider.ReadLine();
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
            ErrorOccurred?.Invoke(this, ex);
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

            _serialPortProvider.Open();
        }

        public void SetBaudRate(int newBaudRate)
        {
            var wasClosed = !_serialPortProvider.IsOpen;
            if (wasClosed)
            {
                _serialPortProvider.Open();
            }
            _serialPortProvider.WriteLine(newBaudRate.ToString());
            if (wasClosed)
            {
                _serialPortProvider.Close();
            }
            _serialPortProvider.BaudRate = newBaudRate;
        }

        public bool TrySetPortName(string newPortName)
        {
            var wasOpen = _serialPortProvider.IsOpen;
            if (wasOpen)
            {
                _serialPortProvider.Close();
            }
            _serialPortProvider.PortName = newPortName;

            try
            {
                _serialPortProvider.Open();
            }
            catch (IOException)
            {
                Stop();
                return false;
            }
            catch (Exception ex)
            {
                Stop();
                RaiseError(ex);
                return false;
            }
            if (!wasOpen)
            {
                _serialPortProvider.Close();
            }
            int oldBaudRate = _serialPortProvider.BaudRate;
            _serialPortProvider.BaudRate = DefaultBaudRate;
            SetBaudRate(oldBaudRate);
            return true;
        }

        public void Restart()
        {
            var oldBaudRate = _serialPortProvider.BaudRate;
            _serialPortProvider.BaudRate = DefaultBaudRate;
            SetBaudRate(oldBaudRate);
        }
    }
}
