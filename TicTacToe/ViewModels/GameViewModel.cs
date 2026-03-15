using System.Windows.Input;
using TicTacToe.Models;
using TicTacToe.Services;

namespace TicTacToe.ViewModels;

public class GameViewModel : BaseViewModel
{
    private readonly IComputerPlayer _computerPlayer;
    private readonly GameBoard _board = new();

    private bool _isGameActive;
    private bool _isShowingResult;
    private bool _isComputerThinking;
    private bool _isEasySelected;
    private bool _isHardSelected = true;
    private bool _isHumanFirst = true;
    private bool _isComputerFirst;
    private string _statusMessage = "Configure your game and press Start!";

    private CellState _humanMark;
    private CellState _computerMark;

    public GameViewModel(IComputerPlayer computerPlayer)
    {
        _computerPlayer = computerPlayer;
        StartGameCommand = new RelayCommand(StartGame);
        MakeMoveCommand = new AsyncRelayCommand<int>(OnCellTapped);
        RestartCommand = new RelayCommand(Restart);
        SelectEasyCommand = new RelayCommand(() => IsEasySelected = true);
        SelectHardCommand = new RelayCommand(() => IsHardSelected = true);
        SelectHumanFirstCommand = new RelayCommand(() => IsHumanFirst = true);
        SelectComputerFirstCommand = new RelayCommand(() => IsComputerFirst = true);
    }

    public ICommand StartGameCommand { get; }
    public ICommand MakeMoveCommand { get; }
    public ICommand RestartCommand { get; }
    public ICommand SelectEasyCommand { get; }
    public ICommand SelectHardCommand { get; }
    public ICommand SelectHumanFirstCommand { get; }
    public ICommand SelectComputerFirstCommand { get; }

    public CellState Cell0 => _board.Cells[0];
    public CellState Cell1 => _board.Cells[1];
    public CellState Cell2 => _board.Cells[2];
    public CellState Cell3 => _board.Cells[3];
    public CellState Cell4 => _board.Cells[4];
    public CellState Cell5 => _board.Cells[5];
    public CellState Cell6 => _board.Cells[6];
    public CellState Cell7 => _board.Cells[7];
    public CellState Cell8 => _board.Cells[8];

    public bool IsGameActive
    {
        get => _isGameActive;
        private set => SetProperty(ref _isGameActive, value);
    }

    public bool IsShowingResult
    {
        get => _isShowingResult;
        private set => SetProperty(ref _isShowingResult, value);
    }

    public bool IsSetupVisible => !_isGameActive && !_isShowingResult;

    public bool IsComputerThinking
    {
        get => _isComputerThinking;
        private set => SetProperty(ref _isComputerThinking, value);
    }

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    public bool IsEasySelected
    {
        get => _isEasySelected;
        set
        {
            SetProperty(ref _isEasySelected, value);
            if (value) IsHardSelected = false;
        }
    }

    public bool IsHardSelected
    {
        get => _isHardSelected;
        set
        {
            SetProperty(ref _isHardSelected, value);
            if (value) IsEasySelected = false;
        }
    }

    public bool IsHumanFirst
    {
        get => _isHumanFirst;
        set
        {
            SetProperty(ref _isHumanFirst, value);
            if (value) IsComputerFirst = false;
        }
    }

    public bool IsComputerFirst
    {
        get => _isComputerFirst;
        set
        {
            SetProperty(ref _isComputerFirst, value);
            if (value) IsHumanFirst = false;
        }
    }

    private Difficulty SelectedDifficulty => _isEasySelected ? Difficulty.Easy : Difficulty.Hard;

    private void StartGame()
    {
        _board.Reset();
        NotifyCellsChanged();

        if (_isHumanFirst)
        {
            _humanMark = CellState.X;
            _computerMark = CellState.O;
        }
        else
        {
            _humanMark = CellState.O;
            _computerMark = CellState.X;
        }

        IsShowingResult = false;
        IsGameActive = true;
        OnPropertyChanged(nameof(IsSetupVisible));
        StatusMessage = _isHumanFirst ? "Your turn!" : "Computer is thinking...";

        if (!_isHumanFirst)
            _ = TriggerComputerMove();
    }

    private async Task OnCellTapped(int cellIndex)
    {
        if (_isComputerThinking || !_isGameActive) return;
        if (!_board.TryMakeMove(cellIndex)) return;

        NotifyCellsChanged();

        if (CheckAndHandleResult()) return;

        await TriggerComputerMove();
    }

    private async Task TriggerComputerMove()
    {
        IsComputerThinking = true;
        StatusMessage = "Computer is thinking...";

        int move = await Task.Run(async () =>
        {
            await Task.Delay(400);
            return _computerPlayer.GetMove(_board, _computerMark, SelectedDifficulty);
        });

        _board.TryMakeMove(move);
        IsComputerThinking = false;
        NotifyCellsChanged();
        CheckAndHandleResult();
    }

    private bool CheckAndHandleResult()
    {
        switch (_board.Result)
        {
            case GameResult.XWins:
                IsGameActive = false;
                IsShowingResult = true;
                OnPropertyChanged(nameof(IsSetupVisible));
                StatusMessage = _humanMark == CellState.X ? "You win! 🎉" : "Computer wins! 🤖";
                return true;
            case GameResult.OWins:
                IsGameActive = false;
                IsShowingResult = true;
                OnPropertyChanged(nameof(IsSetupVisible));
                StatusMessage = _humanMark == CellState.O ? "You win! 🎉" : "Computer wins! 🤖";
                return true;
            case GameResult.Draw:
                IsGameActive = false;
                IsShowingResult = true;
                OnPropertyChanged(nameof(IsSetupVisible));
                StatusMessage = "It's a draw! 🤝";
                return true;
            default:
                StatusMessage = "Your turn!";
                return false;
        }
    }

    private void Restart()
    {
        _board.Reset();
        NotifyCellsChanged();
        IsGameActive = false;
        IsShowingResult = false;
        OnPropertyChanged(nameof(IsSetupVisible));
        StatusMessage = "Configure your game and press Start!";
    }

    private void NotifyCellsChanged()
    {
        for (int i = 0; i < 9; i++)
            OnPropertyChanged($"Cell{i}");
    }
}
