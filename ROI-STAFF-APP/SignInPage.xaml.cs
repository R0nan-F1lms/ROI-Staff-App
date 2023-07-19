namespace ROI_STAFF_APP;

using ROI_STAFF_APP.Data;
public partial class SignInPage : ContentPage
{
	public SignInPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);


		if (email.Text == null || password.Text == null)
		{
			SignIn.IsEnabled = true;
		}
	}

    async void OnSubmitButtonClicked(object sender, EventArgs args)
    {
        string userEmail = email.Text.ToString().ToLower();
        string userPsw = password.Text.ToString();

        try
        {
            var repository = new Repository();
            var accounts = repository.GetAccount(isVerified: true);

            foreach (var account in accounts)
            {
                if (account.userEmail.ToLower() == userEmail && account.password == userPsw)
                {
                    Console.WriteLine($"Match found for user {account.FirstName} {account.LastName}!");
                    await Shell.Current.GoToAsync("//MainPage");
                    return;
                }
            }

            await DisplayAlert("Alert", "incorrect account information, you can either create an account or try using another account to sign in", "OK");
        }
        catch (Exception ex)
        {

            await DisplayAlert("Error:", $"{ex.Message}", "OK");
      
        }
    }


    async void OnSignUpButtonClick(object sender, EventArgs args)
	{
        //Redirects to the Sign Up Page.
        await Shell.Current.GoToAsync("//SignUpPage");
    }

    private void entry_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (email.Text == null || password.Text == null)
		{
            SignIn.IsEnabled = false;
        } else { SignIn.IsEnabled = true; }

	}
}
