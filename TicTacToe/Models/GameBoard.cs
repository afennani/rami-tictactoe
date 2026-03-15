namespace TicTacToe.Models;

public class GameBoard
{
    private static readonly int[][] WinLines =
    [
        [0, 1, 2], [3, 4, 5], [6, 7, 8],  // rows
        [0, 3, 6], [1, 4, 7], [2, 5, 8],  // cols
        [0, 4, 8], [2, 4, 6]               // diags
    ];

    public CellState[] Cells { get; } = new CellState[9];
    public CellState CurrentTurn { get; private set; } = CellState.X;
    public GameResult Result { get; private set; } = GameResult.InProgress;
    public bool IsGameOver => Result != GameResult.InProgress;

    public bool TryMakeMove(int index)
    {
        if (IsGameOver || index < 0 || index > 8 || Cells[index] != CellState.Empty)
            return false;

        Cells[index] = CurrentTurn;
        Result = ComputeResult();
        if (!IsGameOver)
            CurrentTurn = CurrentTurn == CellState.X ? CellState.O : CellState.X;

        return true;
    }

    public IReadOnlyList<int> GetEmptyCells()
    {
        var list = new List<int>();
        for (int i = 0; i < 9; i++)
            if (Cells[i] == CellState.Empty) list.Add(i);
        return list;
    }

    public GameBoard Clone()
    {
        var clone = new GameBoard { CurrentTurn = CurrentTurn, Result = Result };
        Array.Copy(Cells, clone.Cells, 9);
        return clone;
    }

    public void Reset()
    {
        Array.Clear(Cells);
        CurrentTurn = CellState.X;
        Result = GameResult.InProgress;
    }

    private GameResult ComputeResult()
    {
        foreach (var line in WinLines)
        {
            var a = Cells[line[0]];
            if (a != CellState.Empty && a == Cells[line[1]] && a == Cells[line[2]])
                return a == CellState.X ? GameResult.XWins : GameResult.OWins;
        }
        foreach (var cell in Cells)
            if (cell == CellState.Empty) return GameResult.InProgress;
        return GameResult.Draw;
    }
}
