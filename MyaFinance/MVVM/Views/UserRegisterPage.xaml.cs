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
        // Get user input from the Entry fields
        string name = NameEntry.Text;
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        // Check if any of the fields are empty
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        // Create a new User object
        User newUser = new User
        {
            Name = name,
            Email = email,
            Password = password
        };

        // Create an instance of the UserRepository
        UserRepository userRepository = new UserRepository();

        // Add the new user
        int result = userRepository.Add(newUser);
        if (result == 1)
        {
            DisplayAlert("Register Success", "User registered successfully!", "OK");
        }
        else
        {
            DisplayAlert("Register Failed", "User couldn't be registered!", "OK");
        }
        // Display success message


        // Clear the entry fields
        NameEntry.Text = "";
        EmailEntry.Text = "";
        PasswordEntry.Text = "";
    }
    private void GoBackLogin(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}