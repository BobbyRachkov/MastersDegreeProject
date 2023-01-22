using MastersProject.App.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastersProject.App.Models
{
    internal sealed class AttitudeInformation : PropertyChangedBase
    {
        private EquationDescriptor _pitchEquation;
        private EquationDescriptor _rollEquation;
        private int _rawPitch;
        private int rawRoll;

        public double Pitch => PitchEquation.CalculateValue(RawPitch);
        public double Roll => RollEquation.CalculateValue(RawRoll);

        public int RawPitch
        {
            get => _rawPitch;
            set
            {
                _rawPitch = value;
                NotifyPropertyChanged(nameof(RawPitch));
                NotifyPropertyChanged(nameof(Pitch));
            }
        }
        public int RawRoll
        {
            get => rawRoll;
            set
            {
                rawRoll = value;
                NotifyPropertyChanged(nameof(RawRoll));
                NotifyPropertyChanged(nameof(Roll));
            }
        }
        public long Timestamp { get; set; }
        public int Index { get; set; }

        public EquationDescriptor PitchEquation
        {
            get => _pitchEquation;
            set
            {
                _pitchEquation = value;
                NotifyPropertyChanged(nameof(PitchEquation));
                NotifyPropertyChanged(nameof(Pitch));
            }
        }
        public EquationDescriptor RollEquation
        {
            get => _rollEquation;
            set
            {
                _rollEquation = value;
                NotifyPropertyChanged(nameof(RollEquation));
                NotifyPropertyChanged(nameof(Roll));
            }
        }

        public AttitudeInformation()
        {
            _pitchEquation = new EquationDescriptor(0, 0);
            _rollEquation = new EquationDescriptor(0, 0);
        }



        public override string ToString()
        {
            return $"Index: {Index:00000}; Pitch: {RawPitch:0000}; Roll: {RawRoll:0000}; Timestamp: {Timestamp}";
        }
    }
}
