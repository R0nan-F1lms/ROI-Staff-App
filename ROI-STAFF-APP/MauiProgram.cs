using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace ROI_STAFF_APP;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()

			.UseMauiCommunityToolkit()

			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("Trebuchet-MS.ttf", "Trebuchet");
				fonts.AddFont("Trebuchet-MS-Bold.ttf", "TrebuchetBold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		// Singleton Pages
		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<ContactsPage>();
		builder.Services.AddSingleton<HRPage>();
        builder.Services.AddSingleton<SettingsPage>();
		// Transient Pages
        builder.Services.AddTransient<SignInPage>();
        builder.Services.AddTransient<SignUpPage>();

		return builder.Build();
	}
}

