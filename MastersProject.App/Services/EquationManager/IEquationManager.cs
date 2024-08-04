using System;
using System.Collections.ObjectModel;
using MastersProject.App.Services.MathEngine;

namespace MastersProject.App.Services.EquationManager;

public interface IEquationManager
{
    event EventHandler? AttitudeEquationUpdated;
    event EventHandler? DisplayEquationUpdated;

    IEquation PitchAttitudeEquation { get; }
    IEquation RollAttitudeEquation { get; }
    ObservableCollection<IEquation> PitchAttitudeEquationsHistory { get; }
    ObservableCollection<IEquation> RollAttitudeEquationsHistory { get; }
    void SetNewPitchAttitudeEquation(IEquation equation);
    void SetOldPitchAttitudeEquation(int index);
    void SetNewRollAttitudeEquation(IEquation equation);
    void SetOldRollAttitudeEquation(int index);

    IEquation PitchDisplayEquation { get; }
    IEquation RollDisplayEquation { get; }
    void SetNewPitchDisplayEquation(IEquation equation);
    void SetNewRollDisplayEquation(IEquation equation);
}