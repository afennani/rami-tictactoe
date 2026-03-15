using System.Globalization;
using TicTacToe.Models;

namespace TicTacToe.Converters;

public class CellStateToTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is CellState s ? s switch
        {
            CellState.X => "X",
            CellState.O => "O",
            _ => ""
        } : "";

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
