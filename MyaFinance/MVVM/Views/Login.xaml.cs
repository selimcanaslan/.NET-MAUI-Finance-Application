
using MyaFinance.MVVM.Views;
using MyaFinance.Repositories;
using MyaFinance.MVVM.Models;

namespace MyaFinance;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
        emailEntry.Text = "selimcanaslan33@gmail.com";
        passwordEntry.Text = "seloselo1";
    }
    private void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string userMail = emailEntry.Text;
        string password = passwordEntry.Text;

        UserRepository userRepository = new UserRepository();

        User user = userRepository.Get(userMail);
        
        if (user != null && user.Password == password)
        {
            messageLabel.IsVisible = false;


            MainPage mainPage = new MainPage(user);
            Application.Current.MainPage.Navigation.PushModalAsync(mainPage);
        }
        else
        {
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