using System;
using System.ComponentModel;
using MastersProject.App.Services.MathEngine;

namespace MastersProject.App.Services.AttitudeProvider;

internal interface IAttitudeProvider
{
    double Pitch { get; }
    double Roll { get; }
    int RawPitch { get; }
    int RawRoll { get; }
    long Timestamp { get; }
    int Index { get; }
    IEquation PitchEquation { get; set; }
    IEquation RollEquation { get; set; }
    void RestartConnection();
    string ToString();
    event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<Exception>? ErrorOccurred;
}