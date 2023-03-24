using System;
using System.ComponentModel;
using MastersProject.App.MathEngine;
using MastersProject.App.Models;

namespace MastersProject.App.Infrastructure.Interfaces;

internal interface IAttitudeProvider
{
    double Pitch { get; }
    double Roll { get; }
    int RawPitch { get; }
    int RawRoll { get; }
    long Timestamp { get; }
    int Index { get; }
    Equation PitchEquation { get; set; }
    Equation RollEquation { get; set; }
    void RestartConnection();
    string ToString();
    event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<Exception>? ErrorOccurred;
}