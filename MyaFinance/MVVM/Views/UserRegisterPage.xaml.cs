using MyaFinance.MVVM.Models;
using MyaFinance.Repositories;
using System.Collections.ObjectModel;

namespace MyaFinance.MVVM.Views;

public partial class UserRegisterPage : ContentPage
{
    public UserRegisterPage()
    {
        InitializeComponent();
    }

    private void Register_Clicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text;
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        User newUser = new User
        {
            Name = name,
            Email = email,
            Password = password
        };

        UserRepository userRepository = new UserRepository();

        int result = userRepository.Add(newUser);
        if (result == 1)
        {
            DisplayAlert("Register Success", "User registered successfully!", "OK");
            Login login = new Login();
            Application.Current.MainPage.Navigation.PushModalAsync(login);
        }
        else
        {
            DisplayAlert("Register Failed", "User couldn't be registered!", "OK");
        }


        NameEntry.Text = "";
        EmailEntry.Text = "";
        PasswordEntry.Text = "";
    }
    private void GoBackLogin(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}