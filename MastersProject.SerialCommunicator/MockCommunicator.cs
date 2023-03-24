using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MastersProject.SerialCommunicator
{
    public class MockCommunicator<TData> : ISerialCommunicator<TData>
    {
        private readonly IObjectTranslator<TData> _translator;
        private readonly SerialPort _serialPort;
        private Task? _asyncTask;
        private CancellationTokenSource? _asyncTaskCancellationTokenSource;
        private bool _isAsyncRunning = false;
        private bool _isSyncRunning = false;
        private readonly List<Exception> _errors;
        private const int DefaultBaudRate = 9600;
        private Random _random;

        public MockCommunicator(IObjectTranslator<TData> translator)
        {
            _serialPort = new SerialPort();
            _translator = translator;
            _errors = new();
            _random = new();
        }

        public IReadOnlyCollection<Exception> Errors => _errors;
        public event EventHandler<Exception>? ErrorOccurred;
        public event EventHandler<TData>? DataReceived;

        public bool TrySetup(string portName, int baudRate)
        {
            return true;
        }

        public string[] GetPortNames()
        {
            return Enumerable
                .Range(1, 8)
                .Select(c => $"Dummy COM{c}")
                .ToArray();
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
            _asyncTaskCancellationTokenSource = new CancellationTokenSource();
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

            _serialPort.Open();
        }

        public void Stop()
        {
            if (_isAsyncRunning)
            {
                _isAsyncRunning = false;
                _asyncTaskCancellationTokenSource?.Cancel();
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
                var line = $"12;1020;";
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

            _serialPort.Open();
        }

        public void SetBaudRate(int newBaudRate)
        {
            
        }

        public bool TrySetPortName(string newPortName)
        {
            return true;
        }

        public void Restart()
        {
            
        }
    }
}
