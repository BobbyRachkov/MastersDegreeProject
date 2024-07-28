using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MastersProject.App.Infrastructure.Mvvm;
using MastersProject.Serial;

namespace MastersProject.App.ViewModels.PortSelector;

internal class PortViewModel : ViewModelBase
{
    private string _name;
    private bool? _isCheckSuccessful;
    private bool _isCheckInProgress;

    public PortViewModel(string name, Action<string> portSelectedCallback)
    {
        _name = name;
        _isCheckSuccessful = null;
        _isCheckInProgress = false;
        UsePortCommand = new(
            () => portSelectedCallback(name),
            () => !IsCheckInProgress && IsCheckSuccessful == true);
    }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            NotifyPropertyChanged();
        }
    }
    public bool? IsCheckSuccessful
    {
        get => _isCheckSuccessful;
        set
        {
            _isCheckSuccessful = value;
            NotifyPropertyChanged();
        }
    }
    public bool IsCheckInProgress
    {
        get => _isCheckInProgress;
        set
        {
            _isCheckInProgress = value;
            NotifyPropertyChanged();
        }
    }

    public RelayCommand UsePortCommand { get; }

    public async Task RunConnectivityCheck()
    {
        OnUiThread(() =>
        {
            IsCheckSuccessful = null;
            IsCheckInProgress = true;
        });
        Debug.WriteLine($"Prepared {Name}");
        ISerialPortConnectivityTester checker = new SerialPortConnectivityTester();

        await Task.Delay(TimeSpan.FromMilliseconds(500));

        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));


        var checkTask = Task.Run(
            () => checker.TryConnect(Name, cts.Token),
            cts.Token);

        await checkTask;
        OnUiThread(() => IsCheckInProgress = false);
        
        if (checkTask is { IsCompletedSuccessfully: true, Result: true })
        {
            OnUiThread(() => IsCheckSuccessful = true);
            return;
        }

        OnUiThread(() => IsCheckSuccessful = false);
    }
}