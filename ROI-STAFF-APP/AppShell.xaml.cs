namespace ROI_STAFF_APP;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));

		Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));

        Routing.RegisterRoute(nameof(ContactsPage), typeof(ContactsPage));

        Routing.RegisterRoute(nameof(HRPage), typeof(HRPage));

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));

        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }
}

