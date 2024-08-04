using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MastersProject.App.Extensions;
using MastersProject.App.Infrastructure.Interfaces;
using MastersProject.App.Infrastructure.Mvvm;
using MastersProject.App.Infrastructure.WindowFactories;
using MastersProject.App.Models;
using MastersProject.App.Models.SerialCommunication;
using MastersProject.App.Services.InputStream;
using MastersProject.App.Services.InputStream.Models;
using MastersProject.Serial;

namespace MastersProject.App.ViewModels.PortSelector;

internal class PortSelectorViewModel : ViewModelBase, IDisposable
{
    private readonly ISerialCommunicator<SerialData> _serial;
    private readonly IWindowManager _windowManager;
    private readonly IInputStreamManager _inputStreamManager;
    private Task[] _checks = Array.Empty<Task>();

    public PortSelectorViewModel(
        ISerialCommunicator<SerialData> serial,
        IWindowManager windowManager,
        IInputStreamManager inputStreamManager)
    {
        _serial = serial;
        _windowManager = windowManager;
        _inputStreamManager = inputStreamManager;
        Ports = new();
        RefreshPortsCommand = new(RefreshPorts);
        RerunChecksCommand = new(RunChecks);
        RefreshPorts();
    }

    public RelayCommand RefreshPortsCommand { get; }
    public RelayCommand RerunChecksCommand { get; }

    public ObservableCollection<PortViewModel> Ports { get; }

    private void RefreshPorts()
    {
        Ports.Clear();
        foreach (var portName in _serial.GetPortNames())
        {
            Ports.Add(new(portName, LaunchOnPort));
        }
        RunChecks();
    }

    private void RunChecks()
    {
        _checks = new Task[Ports.Count];
        for (int i = 0; i < Ports.Count; i++)
        {
            _checks[i] = Ports[i].RunConnectivityCheck();
        }
    }

    private void LaunchOnPort(string portName)
    {
        //_serial.TrySetup(portName, 9600);
        //_serial.StartAsync();
        _windowManager.SetActiveFactory<PfdWindowFactory>();
        _windowManager.ShowWindow<PfdViewModel>();
        _windowManager.CloseWindow(this);

        _inputStreamManager.TryConfigure(InputStreamType.Serial,
            new InputStreamConfig
            {
                Serial = new SerialConfig
                {
                    BaudRate = 9600,
                    PortName = portName
                }
            });
        _inputStreamManager.Start();

    }

    public void Dispose()
    {
        _checks.ForEach(x =>
        {
            if (x.Status is TaskStatus.Running)
            {
                x.Dispose();
            }
        });
    }
}