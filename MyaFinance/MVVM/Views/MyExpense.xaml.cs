using MyaFinance.MVVM.Models;
using MyaFinance.Repositories;
using MyaFinance.MVVM.Views;
using System;

namespace MyaFinance.MVVM.Views;

public partial class MyExpense : ContentPage
{
    private int _userId;
    private ExpenseRepository _expenseRepository;

    public MyExpense(int id)
    {
        _userId = id;
        _expenseRepository = new ExpenseRepository();
        InitializeComponent();
        LoadExpenses();
    }

    private void LoadExpenses()
    {
        List<Expense> expenses = _expenseRepository.GetAll(_userId);
        expenseListView.ItemsSource = expenses;
        if (expenses == null || expenses.Count < 1)
        {
            noExpenseLabel.IsVisible = true;
        }
        else
        {
            noExpenseLabel.IsVisible = false;
        }
    }

    private void GoBack(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void AddNewExpenseButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new AddNewExpense(_userId, RefreshIncomeList));
    }

    private void EditButton_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var expenseId = (int)button.CommandParameter;

        // Fetch the income details using the incomeId
        var expense = _expenseRepository.Get(expenseId);

        if (expense != null)
        {
            // Navigate to the EditIncome page with the income details
            Navigation.PushModalAsync(new EditExpense(expense, RefreshIncomeList));
        }
        else
        {
            DisplayAlert("Error", "Income not found", "OK");
        }
    }

    private void DeleteButton_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var expenseId = (int)button.CommandParameter;
        _expenseRepository.Delete(expenseId);
        DisplayAlert("Success", "Record Deleted Succesfully", "OK");
        LoadExpenses();
    }

    private void RefreshIncomeList()
    {
        var expenses = _expenseRepository.GetAll(_userId);
        expenseListView.ItemsSource = expenses;
        if (expenses == null || expenses.Count < 1)
        {
            noExpenseLabel.IsVisible = true;
        }
        else
        {
            noExpenseLabel.IsVisible = false;
        }
    }
}