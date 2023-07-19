using ROI_STAFF_APP.Data;
using System.Collections.ObjectModel;

namespace ROI_STAFF_APP;

public partial class HRPage : ContentPage
{
	public HRPage()
	{
		InitializeComponent();

		

        var repo = new Repository();
        var productList = repo.GetAccount(isVerified: false);
        _repo = repo;

        AccountView.ItemsSource = accountL;

        foreach (var product in productList)
        {
            AccountL.Add(product);
        }

    }

    //private ContactsViewModel iewModel;
    private Repository _repo;

    ObservableCollection<Account> accountL = new ObservableCollection<Account>();
    public ObservableCollection<Account> AccountL { get { return accountL; } }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        //viewModel.LoadContacts();
    }

    async void AccountView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var account = e.SelectedItem as Account;

        // Retrieve the values from the selected account
        string firstName = account.FirstName;
        string lastName = account.LastName;
        string phoneNumber = account.PhoneNumber;
        string role = account.Role;
        string email = account.userEmail;
        bool answer = await DisplayAlert("Verify Staff Member", $"Would you like to verify this person as a staff member of ROI\nFull Name: {firstName} {lastName}\nPhone Number: {phoneNumber}\nPosition: {role}\nEmail: {email}", "Yes", "No");

        if (answer)
        {
            // Update the isVerified field of the selected account
            account.isVerified = true;

            AccountView.ItemsSource = accountL;

            accountL.Remove(account);
            

            // Call the repository method to update the database record
            _repo.UpdateAccountVerification(account.staffID);
            LoadAccounts();
            return;


        }
    }

    private void LoadAccounts()
    {
        var repo = new Repository();
        var productList = repo.GetAccount(isVerified: false);
        _repo = repo;

        // Clear the existing account list
        accountL.Clear();

        // Add the updated accounts to the ObservableCollection
        foreach (var product in productList)
        {
            AccountL.Add(product);
        }
    }
}
