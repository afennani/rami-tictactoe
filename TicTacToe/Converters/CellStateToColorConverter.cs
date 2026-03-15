using System.Globalization;
using TicTacToe.Models;

namespace TicTacToe.Converters;

public class CellStateToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is CellState s ? s switch
        {
            CellState.X => Color.FromArgb("#2196F3"),
            CellState.O => Color.FromArgb("#F44336"),
            _ => Color.FromArgb("#E8E8E8")
        } : Color.FromArgb("#E8E8E8");

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
