using TicTacToe.Models;

namespace TicTacToe.Services;

public class ComputerPlayerService : IComputerPlayer
{
    private static readonly Random _rng = new();

    public int GetMove(GameBoard board, CellState computerMark, Difficulty difficulty) =>
        difficulty == Difficulty.Easy ? RandomMove(board) : MinimaxMove(board, computerMark);

    private static int RandomMove(GameBoard board)
    {
        var empty = board.GetEmptyCells();
        return empty[_rng.Next(empty.Count)];
    }

    private static int MinimaxMove(GameBoard board, CellState computerMark)
    {
        var opponentMark = computerMark == CellState.X ? CellState.O : CellState.X;
        int bestScore = int.MinValue;
        int bestIndex = -1;

        foreach (var index in board.GetEmptyCells())
        {
            var clone = board.Clone();
            clone.TryMakeMove(index);
            int score = Minimax(clone, computerMark, opponentMark, isMaximizing: false, depth: 1);
            if (score > bestScore)
            {
                bestScore = score;
                bestIndex = index;
            }
        }

        return bestIndex;
    }

    private static int Minimax(GameBoard board, CellState computerMark, CellState opponentMark,
                                bool isMaximizing, int depth)
    {
        var result = board.Result;
        if (result == (computerMark == CellState.X ? GameResult.XWins : GameResult.OWins))
            return 10 - depth;
        if (result == (opponentMark == CellState.X ? GameResult.XWins : GameResult.OWins))
            return -10 + depth;
        if (result == GameResult.Draw)
            return 0;

        var emptyCells = board.GetEmptyCells();
        if (emptyCells.Count == 0) return 0;

        if (isMaximizing)
        {
            int best = int.MinValue;
            foreach (var index in emptyCells)
            {
                var clone = board.Clone();
                clone.TryMakeMove(index);
                best = Math.Max(best, Minimax(clone, computerMark, opponentMark, false, depth + 1));
            }
            return best;
        }
        else
        {
            int best = int.MaxValue;
            foreach (var index in emptyCells)
            {
                var clone = board.Clone();
                clone.TryMakeMove(index);
                best = Math.Min(best, Minimax(clone, computerMark, opponentMark, true, depth + 1));
            }
            return best;
        }
    }
}
