namespace ROI_STAFF_APP;
using ROI_STAFF_APP.Data;


public partial class MainPage : ContentPage
{


    public MainPage()
    {
        InitializeComponent();
        
        Shell.Current.GoToAsync("//SignInPage");
    }

    async void contactsOpen(object sender, EventArgs args)
    {
        await Navigation.PushAsync(new ContactsPage());
    }

    async void hrOpen(object sender, EventArgs args)
    {
        string userName = await DisplayPromptAsync("Sign In required", "Please enter your username", "OK", "Cancel");
        string userPsw = await DisplayPromptAsync("Sign In required", $"Please enter your password for the account {userName}", "OK", "Cancel");
        string AdminRole = "Human Resource";

        try
        {
            var repository = new Repository();
            var accounts = repository.GetAccount(isVerified:true);

            foreach (var account in accounts)
            {
                if (account.Role == AdminRole && userName.ToLower() == account.userEmail && userPsw == account.password)
                {
                    await Navigation.PushAsync(new HRPage());
                    return;
                }
            }

            await DisplayAlert("Error", "incorrect account information or you do not have the correct permissions to access this part of the application", "OK");
        }
        catch (Exception ex)
        {
            Console.Write($"{ex.Message}");
        }
        
    }

    async void SettingsOpen(object sender, EventArgs args)
    {
        try
        {
            await Navigation.PushAsync(new SettingsPage());
        } catch (Exception e)
        {
            DisplayAlert("Error:", $"{e.Message}", "Ok");
        }
    }
}
