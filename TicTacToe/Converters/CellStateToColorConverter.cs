using System.Globalization;
using TicTacToe.Models;

namespace TicTacToe.Converters;

public class CellStateToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is CellState s ? s switch
        {
            CellState.X => Color.FromArgb("#6C63FF"),
            CellState.O => Color.FromArgb("#F687B3"),
            _ => Color.FromArgb("#E0E5EC")
        } : Color.FromArgb("#E0E5EC");

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
