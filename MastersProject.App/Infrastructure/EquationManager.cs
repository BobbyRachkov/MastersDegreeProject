using System.Collections.ObjectModel;
using MastersProject.App.MathEngine;

namespace MastersProject.App.Infrastructure;

internal class EquationManager : PropertyChangedBase
{
    private Equation _rollEquation = null!;
    private Equation _pitchEquation = null!;


    public EquationManager(Equation rollEquation, Equation pitchEquation)
    {
        SetNewPitchEquation(pitchEquation);
        SetNewRollEquation(rollEquation);
        PitchEquationsHistory = new();
        RollEquationsHistory = new();
    }


    public Equation PitchEquation
    {
        get => _pitchEquation;
        private set
        {
            _pitchEquation = value;
            NotifyPropertyChanged();
        }
    }

    public Equation RollEquation
    {
        get => _rollEquation;
        private set
        {
            _rollEquation = value;
            NotifyPropertyChanged();
        }
    }

    public ObservableCollection<Equation> PitchEquationsHistory { get; }
    public ObservableCollection<Equation> RollEquationsHistory { get; }

    public void SetNewPitchEquation(Equation equation)
    {
        PitchEquationsHistory.Insert(0, equation);
        PitchEquation = equation;
    }
    public void SetOldPitchEquation(int index)
    {
        PitchEquation = PitchEquationsHistory[index];
    }
    public void SetNewRollEquation(Equation equation)
    {
        RollEquationsHistory.Insert(0, equation);
        RollEquation = equation;
    }
    public void SetOldRollEquation(int index)
    {
        RollEquation = RollEquationsHistory[index];
    }
}