using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using SQLite;
using System;
//using System.IO;
//using System.Linq;
using static ROI_STAFF_APP.Data.UserSettingsPage;

namespace ROI_STAFF_APP;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
        InitializeComponent();

        // Initialize the SQLite database connection
        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "settings.db");
        _database = new SQLiteAsyncConnection(databasePath);
        _database.CreateTableAsync<UserSet>().Wait();

        // Load the user settings
        LoadUserSettings();
    }
    private SQLiteAsyncConnection _database;

   

    private async void LoadUserSettings()
    {
        // Check if the user settings already exist in the database
        var existingSettings = await _database.Table<UserSet>().FirstOrDefaultAsync();
        if (existingSettings != null)
        {
            
            newFontSize.Value = existingSettings.FontSize;

            if (existingSettings.lightOrDark)
            {
                togTheme.IsToggled = true;
            }
            else
            {
                togTheme.IsToggled = false;
            }

            var currentTheme = existingSettings.lightOrDark;
            if (currentTheme)
                Application.Current.UserAppTheme = AppTheme.Dark;
            else
                Application.Current.UserAppTheme = AppTheme.Light;
        }
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {

       
        bool theme = togTheme.IsToggled;

        var userSettings = new UserSet
        {

            FontSize = (int)Math.Round(newFontSize.Value, 0),
            lightOrDark = theme
        };

        await _database.InsertOrReplaceAsync(userSettings);

        // Show a confirmation message
        await DisplayAlert("Success", "User settings saved", "OK");
    }

    void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
    {
        int value = (int)Math.Round(args.NewValue, 0);
        fontSizeLable.Text = string.Format("Font Size: {0}", value);

        
        Application.Current.Resources["DynamicFontSize"] = value;
    }


    //PREFERENCES
    //SWITCH: https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/switch
    private void OnThemeSwitchToggled(object sender, ToggledEventArgs e)
    {
        bool isDarkTheme = e.Value;
        Preferences.Set("DarkThemeOn", isDarkTheme ? "Dark" : "Light");

        // Apply the theme
        if (isDarkTheme)
            Application.Current.UserAppTheme = AppTheme.Dark;
        else
            Application.Current.UserAppTheme = AppTheme.Light;
    }
}
