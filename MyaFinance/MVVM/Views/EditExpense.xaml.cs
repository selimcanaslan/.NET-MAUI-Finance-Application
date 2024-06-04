using MyaFinance.Repositories;
using MyaFinance.MVVM.Models;
using MyaFinance.MVVM.Views;

namespace MyaFinance.MVVM.Views;


public partial class EditExpense : ContentPage
{
    private Expense _expense;
    private ExpenseRepository _expenseRepository;
    private Action _refreshCallback;

    public EditExpense(Expense expense, Action refreshCallback)
    {
        InitializeComponent();
        _expense = expense;
        _expenseRepository = new ExpenseRepository();
        _refreshCallback = refreshCallback;

        // Initialize the fields with the current income data
        titleEntry.Text = _expense.Title;
        descriptionEntry.Text = _expense.Description;
        amountEntry.Text = _expense.Amount.ToString();
        datePicker.Date = _expense.Date;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        _expense.Title = titleEntry.Text;
        _expense.Description = descriptionEntry.Text;
        _expense.Amount = double.Parse(amountEntry.Text);
        _expense.Date = datePicker.Date;

        _expenseRepository.Update(_expense);
        _refreshCallback?.Invoke();
        Navigation.PopModalAsync();
    }

    private void CancelButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}