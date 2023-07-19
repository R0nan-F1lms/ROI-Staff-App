using ROI_STAFF_APP.Data;


namespace ROI_STAFF_APP;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        
        
    }
    
	async void OnSignInButtonClicked(object sender, EventArgs args)
    {
        //Redirects to the Sign Up Page.
        await Shell.Current.GoToAsync("//SignInPage");
    }

    async void OnRequestButtonClicked(object sender, EventArgs args)
    {
        string firstName = name.Text.ToString();
        string lastName = lastname.Text.ToString();
        string phoneNum = phone.Text.ToString();
        string role = staffrole.SelectedItem as string;
        string userEmail = email.Text.ToString().ToLower();
        string userPsw = password.Text.ToString();
        string confirmUserPsw = password.Text.ToString();

        if (firstName == null || lastName == null || phoneNum == null || role == null || userEmail == null || userPsw == null || confirmUserPsw == null)
        {
            await DisplayAlert("Alert", "Please fill out all of the entries.", "OK");
        }
        else if (!userEmail.Contains("@roi.com.au"))
        {
            await DisplayAlert("Alert", "please enter your company email.", "OK");
        }
        else if (userPsw != confirmUserPsw)
        {
            await DisplayAlert("Alert", "Your passwords do not match.t", "OK");
        }
        else
        {
            try
            {
                var repository = new Repository();

                var newAccount = new Account
                {
                    staffID = 0, // Set the correct staffID value here
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNum,
                    Role = role,
                    userEmail = userEmail,
                    password = userPsw,
                    isVerified = false
                };

                int result = repository.InsertAccount(newAccount);

                if (result > 0)
                {
                    await DisplayAlert("Alert", "Request has been sent off! Your account will be verified within 24 hours", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "An error occoured while trying too request your account, if this error persists please contact the developer", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occoured while trying too request your account, if this error persists please contact the developer\n\n{ex.Message}", "OK");
            }
        }
    }



}