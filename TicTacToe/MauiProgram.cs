using Microsoft.Extensions.Logging;
using TicTacToe.Services;
using TicTacToe.ViewModels;
using TicTacToe.Views;

namespace TicTacToe;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<IComputerPlayer, ComputerPlayerService>();
		builder.Services.AddTransient<GameViewModel>();
		builder.Services.AddTransient<GamePage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
