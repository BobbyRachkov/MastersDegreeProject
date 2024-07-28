using System;
using System.Collections.ObjectModel;
using MastersProject.App.Infrastructure.Mvvm;
using MastersProject.App.Services.MathEngine;

namespace MastersProject.App.Services.EquationManager;

internal class EquationManager : IEquationManager
{
    private IEquation _rollEquation = null!;
    private IEquation _pitchEquation = null!;

    public event EventHandler? EquationUpdated;

    public EquationManager()
    {
        PitchEquationsHistory = new();
        RollEquationsHistory = new();
    }


    public IEquation PitchEquation
    {
        get => _pitchEquation;
        private set
        {
            _pitchEquation = value;
            EquationUpdated?.Invoke(this, EventArgs.Empty);
        }
    }

    public IEquation RollEquation
    {
        get => _rollEquation;
        private set
        {
            _rollEquation = value;
            EquationUpdated?.Invoke(this, EventArgs.Empty);
        }
    }

    public ObservableCollection<IEquation> PitchEquationsHistory { get; }
    public ObservableCollection<IEquation> RollEquationsHistory { get; }

    public void SetNewPitchEquation(IEquation equation)
    {
        PitchEquationsHistory.Insert(0, equation);
        PitchEquation = equation;
    }

    public void SetOldPitchEquation(int index)
    {
        PitchEquation = PitchEquationsHistory[index];
    }

    public void SetNewRollEquation(IEquation equation)
    {
        RollEquationsHistory.Insert(0, equation);
        RollEquation = equation;
    }

    public void SetOldRollEquation(int index)
    {
        RollEquation = RollEquationsHistory[index];
    }
}