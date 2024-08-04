using MastersProject.App.Infrastructure.DataStreaming;
using MastersProject.App.Models.DataStreaming;
using MastersProject.App.Models.SerialCommunication;
using MastersProject.App.Services.EquationManager;
using MastersProject.App.Services.InputStream.Models;
using MastersProject.Serial;

namespace MastersProject.App.Services.InputStream;

public class SerialManager : IInputStreamManager
{
    private readonly ISerialCommunicator<SerialData> _serialCommunicator;
    private readonly IDataStreamWriter<AttitudeDataFrame> _attitudeWriter;
    private readonly IEquationManager _equationManager;

    private bool _isPaused = false;
    private bool _isStarted = false;
    private bool _isConfigured = false;

    public SerialManager(
        ISerialCommunicator<SerialData> serialCommunicator,
        IDataStreamWriter<AttitudeDataFrame> attitudeWriter,
        IEquationManager equationManager)
    {
        _serialCommunicator = serialCommunicator;
        _attitudeWriter = attitudeWriter;
        _equationManager = equationManager;

        _serialCommunicator.DataReceived += DataReceived;
    }

    public void Start()
    {
        if (_isPaused)
        {
            _isPaused = false;

            if (_isStarted)
            {
                return;
            }
        }

        if (!_isConfigured)
        {
            return;
        }

        _serialCommunicator.StartAsync();
        _isStarted = true;
    }

    public void Stop()
    {
        _serialCommunicator.Stop();
        _isStarted = false;
        _isPaused = false;
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public bool TryConfigure(InputStreamType type, InputStreamConfig config)
    {
        if (type is not InputStreamType.Serial
            || config.Serial is null)
        {
            return false;
        }

        if (_isStarted)
        {
            Stop();
        }

        if (_serialCommunicator.TrySetup(config.Serial.PortName, config.Serial.BaudRate))
        {
            _isConfigured = true;
            return true;
        }

        return false;
    }

    private void DataReceived(object? sender, SerialData e)
    {
        if (_isPaused)
        {
            return;
        }

        (double pitch, double roll) parsedData = MapToDegrees(e.Pitch, e.Roll);

        _attitudeWriter.WriteEntry(new AttitudeDataFrame
        {
            Pitch = parsedData.pitch,
            Roll = parsedData.roll,
            RawPitch = e.Pitch,
            RawRoll = e.Roll,
            Index = e.Index,
        });
    }

    private (double pitch, double roll) MapToDegrees(int rawPitch, int rawRoll)
    {
        return (
            _equationManager.PitchAttitudeEquation.CalculateYValue(rawPitch),
            _equationManager.RollAttitudeEquation.CalculateYValue(rawRoll)
            );
    }
}