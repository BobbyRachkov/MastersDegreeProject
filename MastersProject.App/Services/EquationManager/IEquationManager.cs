using System;
using System.Collections.ObjectModel;
using MastersProject.App.Services.MathEngine;

namespace MastersProject.App.Services.EquationManager;

public interface IEquationManager
{
    event EventHandler? EquationUpdated;
    IEquation PitchEquation { get; }
    IEquation RollEquation { get; }
    ObservableCollection<IEquation> PitchEquationsHistory { get; }
    ObservableCollection<IEquation> RollEquationsHistory { get; }
    void SetNewPitchEquation(IEquation equation);
    void SetOldPitchEquation(int index);
    void SetNewRollEquation(IEquation equation);
    void SetOldRollEquation(int index);
}