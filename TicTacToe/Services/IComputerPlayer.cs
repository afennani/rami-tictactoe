using TicTacToe.Models;

namespace TicTacToe.Services;

public interface IComputerPlayer
{
    int GetMove(GameBoard board, CellState computerMark, Difficulty difficulty);
}
