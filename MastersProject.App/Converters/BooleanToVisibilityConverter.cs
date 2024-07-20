using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MastersProject.App.Converters;

public sealed class BooleanToVisibilityConverter : IValueConverter
{
    public Visibility TreatNullAs { get; set; } = Visibility.Hidden;
    public Visibility TreatWrongTypeAs { get; set; } = Visibility.Visible;
    public Visibility FalseValue { get; set; } = Visibility.Hidden;
    public bool Invert { get; set; } = false;

    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return TreatNullAs;
        }

        if (value is not bool val)
        {
            return TreatWrongTypeAs;
        }

        if (Invert)
        {
            val = !val;
        }

        return val ? Visibility.Visible : FalseValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}