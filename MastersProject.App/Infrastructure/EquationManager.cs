using System.Collections.ObjectModel;
using MastersProject.App.Infrastructure.Mvvm;
using MastersProject.App.Services.MathEngine;

namespace MastersProject.App.Infrastructure;

internal class EquationManager : ViewModelBase
{
    private IEquation _rollEquation = null!;
    private IEquation _pitchEquation = null!;


    public EquationManager(IEquation rollEquation, IEquation pitchEquation)
    {
        SetNewPitchEquation(pitchEquation);
        SetNewRollEquation(rollEquation);
        PitchEquationsHistory = new();
        RollEquationsHistory = new();
    }


    public IEquation PitchEquation
    {
        get => _pitchEquation;
        private set
        {
            _pitchEquation = value;
            NotifyPropertyChanged();
        }
    }

    public IEquation RollEquation
    {
        get => _rollEquation;
        private set
        {
            _rollEquation = value;
            NotifyPropertyChanged();
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