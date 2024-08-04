using System;
using System.Collections.ObjectModel;
using MastersProject.App.Services.MathEngine;

namespace MastersProject.App.Services.EquationManager;

internal class EquationManager : IEquationManager
{
    private IEquation _pitchAttitudeEquation = null!;
    private IEquation _rollAttitudeEquation = null!;

    private IEquation _pitchDisplayEquation = null!;
    private IEquation _rollDisplayEquation = null!;

    public event EventHandler? AttitudeEquationUpdated;
    public event EventHandler? DisplayEquationUpdated;

    public EquationManager()
    {
        _pitchAttitudeEquation = new LinearEquation(1, 0);

        PitchAttitudeEquationsHistory = new();
        RollAttitudeEquationsHistory = new();
    }

    #region Attitude Equations

    public IEquation PitchAttitudeEquation
    {
        get => _pitchAttitudeEquation;
        private set
        {
            _pitchAttitudeEquation = value;
            AttitudeEquationUpdated?.Invoke(this, EventArgs.Empty);
        }
    }

    public IEquation RollAttitudeEquation
    {
        get => _rollAttitudeEquation;
        private set
        {
            _rollAttitudeEquation = value;
            AttitudeEquationUpdated?.Invoke(this, EventArgs.Empty);
        }
    }

    public ObservableCollection<IEquation> PitchAttitudeEquationsHistory { get; }
    public ObservableCollection<IEquation> RollAttitudeEquationsHistory { get; }

    public void SetNewPitchAttitudeEquation(IEquation equation)
    {
        PitchAttitudeEquationsHistory.Insert(0, equation);
        PitchAttitudeEquation = equation;
    }

    public void SetOldPitchAttitudeEquation(int index)
    {
        PitchAttitudeEquation = PitchAttitudeEquationsHistory[index];
    }

    public void SetNewRollAttitudeEquation(IEquation equation)
    {
        RollAttitudeEquationsHistory.Insert(0, equation);
        RollAttitudeEquation = equation;
    }

    public void SetOldRollAttitudeEquation(int index)
    {
        RollAttitudeEquation = RollAttitudeEquationsHistory[index];
    }

    #endregion

    #region Display Equations

    public IEquation PitchDisplayEquation
    {
        get => _pitchDisplayEquation;
        private set
        {
            _pitchDisplayEquation = value;
            DisplayEquationUpdated?.Invoke(this, EventArgs.Empty);
        }
    }

    public IEquation RollDisplayEquation
    {
        get => _rollDisplayEquation;
        private set
        {
            _rollDisplayEquation = value;
            DisplayEquationUpdated?.Invoke(this, EventArgs.Empty);
        }
    }

    public void SetNewPitchDisplayEquation(IEquation equation)
    {
        PitchAttitudeEquationsHistory.Insert(0, equation);
        PitchAttitudeEquation = equation;
    }

    public void SetNewRollDisplayEquation(IEquation equation)
    {
        RollAttitudeEquationsHistory.Insert(0, equation);
        RollAttitudeEquation = equation;
    }

    #endregion
}