using System.Globalization;
using TicTacToe.Models;

namespace TicTacToe.Converters;

public class CellStateToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is CellState s ? s switch
        {
            CellState.X => Color.FromArgb("#5563B3F9"),
            CellState.O => Color.FromArgb("#55F472B6"),
            _ => Color.FromArgb("#20FFFFFF")
        } : Color.FromArgb("#20FFFFFF");

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
