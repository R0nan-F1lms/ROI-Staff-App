using System.Collections.ObjectModel;
using ROI_STAFF_APP.Data;
//using Xamarin.KotlinX.Coroutines.Channels;

namespace ROI_STAFF_APP;

public partial class ContactsPage : ContentPage
{
    //private ContactsViewModel iewModel;
    private Repository _repo;

    ObservableCollection<Account> accountL = new ObservableCollection<Account>();
    public ObservableCollection<Account> AccountL { get { return accountL; } }


    public ContactsPage()
    {
        InitializeComponent();
        //viewModel = new ContactsViewModel();
        //BindingContext = viewModel;

        //Load DB
        var repo = new Repository();
        var accountList = repo.GetAccount(isVerified: true);
        _repo = repo;

        AccountView.ItemsSource = accountL;
        

        foreach (var account in accountList)
        {
            AccountL.Add(account);
        }

        LoadAccounts();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        //viewModel.LoadContacts();
        LoadAccounts();
    }

    async void AccountView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var account = e.SelectedItem as Account;

        // Retrieve the values from the selected account
        int staffID = account.staffID;
        string firstName = account.FirstName;
        string lastName = account.LastName;
        string phoneNumber = account.PhoneNumber;
        string role = account.Role;
        string email = account.userEmail;
        bool answer = await DisplayAlert($"{firstName}'s Contact Card", $"Full Name: {firstName} {lastName}\nPhone Number: {phoneNumber}\nPosition: {role}\nEmail: {email}", "Delete", "Close");

        if (answer)
        {
            string userName = await DisplayPromptAsync("Sign In required", "Please enter your username", "OK", "Cancel");
            string userPsw = await DisplayPromptAsync("Sign In required", $"Please enter your password for the account {userName}", "OK", "Cancel");
            string adminRole = "Human Resource";

            var repository = new Repository();
            var accounts = repository.GetAccount(isVerified:true);

            foreach (var account_admin in accounts)
            {
                if (account_admin.Role == adminRole && userName.ToLower() == account_admin.userEmail && userPsw == account_admin.password)
                {
                    _repo.DeleteAccount(account.staffID);


                    AccountView.ItemsSource = accountL;

                    accountL.Remove(account);
                    LoadAccounts();
                    return;
                }
            }

            LoadAccounts();
            await DisplayAlert("Error", "incorrect account information or you do not have the correct permissions to access this part of the application", "OK");
        }
    }

    private void LoadAccounts()
    {
        var repo = new Repository();
        var accountList = repo.GetAccount(isVerified: true);
        _repo = repo;

        // Clear the existing account list
        accountL.Clear();
        
        // Add the updated accounts to the ObservableCollection
        foreach (var account in accountList)
        {
            AccountL.Add(account);
        }
    }

}


