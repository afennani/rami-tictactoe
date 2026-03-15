using System.Globalization;
using TicTacToe.Models;

namespace TicTacToe.Converters;

public class CellStateToTextColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is CellState s ? s switch
        {
            CellState.X => Colors.White,
            CellState.O => Colors.White,
            _ => Color.FromArgb("#C8CDD8")  // invisible on empty
        } : Colors.White;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
