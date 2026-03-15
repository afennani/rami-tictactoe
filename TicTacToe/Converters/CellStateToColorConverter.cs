using System.Globalization;
using TicTacToe.Models;

namespace TicTacToe.Converters;

public class CellStateToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is CellState s ? s switch
        {
            CellState.X => Color.FromArgb("#6C63FF"),
            CellState.O => Color.FromArgb("#C084FC"),
            _ => Color.FromArgb("#D8DCE6")   // slightly darker than bg = inset/recessed look
        } : Color.FromArgb("#D8DCE6");

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
