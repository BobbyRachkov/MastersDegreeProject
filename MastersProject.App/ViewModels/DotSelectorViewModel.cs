using System;
using MastersProject.App.Infrastructure;
using MastersProject.App.Infrastructure.Interfaces;

namespace MastersProject.App.ViewModels
{
    internal class DotSelectorViewModel : PropertyChangedBase, ICanClose
    {
        private readonly Action<double, double> _onSave;
        private double _valueY;

        public DotSelectorViewModel(double valueX, Action<double, double> onSave)
        {
            CancelCommand = new(() => Close?.Invoke());
            SaveCommand = new(SaveClick);
            ValueX = valueX;
            _onSave = onSave;
        }

        public double ValueX { get; }

        public double ValueY
        {
            get => _valueY;
            set
            {
                _valueY = value;
                NotifyPropertyChanged();
            }
        }

        public RelayCommand CancelCommand { get; }
        public RelayCommand SaveCommand { get; }

        public void OnClose(bool? dialogResult)
        {

        }

        private void SaveClick()
        {
            _onSave.Invoke(ValueX, ValueY);
            Close?.Invoke();
        }

        public event Action? Close;
    }
}
