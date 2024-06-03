using MyaFinance.MVVM.ViewModels;
using MyaFinance.MVVM.Views;
using MyaFinance.Repositories;
using MyaFinance.MVVM.Models;

namespace MyaFinance;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }
    private void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string userMail = emailEntry.Text;
        string password = passwordEntry.Text;

        // Create an instance of the UserRepository
        UserRepository userRepository = new UserRepository();

        // Get the user from the database based on the entered username
        User user = userRepository.Get(userMail);
        
        // Check if the user exists and the password matches
        if (user != null && user.Password == password)
        {
            // Hide any error message
            messageLabel.IsVisible = false;

            // Display a success message

            // Navigate to the main page
            MainPage mainPage = new MainPage(user);
            Application.Current.MainPage.Navigation.PushModalAsync(mainPage);
        }
        else
        {
            // Display an error message
            messageLabel.Text = "Invalid username or password.";
            messageLabel.IsVisible = true;
        }
    }
    private void RegisterButtonClicked(object sender, EventArgs e)
    {
        UserRegisterPage registerPage = new UserRegisterPage();
        Application.Current.MainPage.Navigation.PushModalAsync(registerPage);

    }
}