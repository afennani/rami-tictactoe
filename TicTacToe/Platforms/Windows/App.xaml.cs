using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace TicTacToe.WinUI;

public partial class App : MauiWinUIApplication
{
	public App()
	{
		this.InitializeComponent();
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

	protected override void OnLaunched(LaunchActivatedEventArgs args)
	{
		base.OnLaunched(args);

		if (MauiWinUIApplication.Current.Application.Windows.Count > 0 &&
		    MauiWinUIApplication.Current.Application.Windows[0].Handler?.PlatformView is MauiWinUIWindow winUIWindow)
		{
			try
			{
				winUIWindow.SystemBackdrop = new MicaBackdrop { Kind = MicaKind.BaseAlt };
			}
			catch
			{
				try { winUIWindow.SystemBackdrop = new DesktopAcrylicBackdrop(); }
				catch { /* backdrop not supported on this system */ }
			}
		}
	}
}

