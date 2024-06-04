using MyaFinance.Repositories;
using MyaFinance.MVVM.Models;

namespace MyaFinance.MVVM.Views;

public partial class AddNewExpense : ContentPage
{
    private int _userId;
    private ExpenseRepository _expenseRepository;
    private Action _refreshCallback;

    public AddNewExpense(int userId, Action refreshCallback)
    {
        InitializeComponent();
        _userId = userId;
        _expenseRepository = new ExpenseRepository();
        _refreshCallback = refreshCallback;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        var title = titleEntry.Text;
        var description = descriptionEntry.Text;
        var amount = double.Parse(amountEntry.Text);
        var date = datePicker.Date;
        var currentTime = DateTime.Now.TimeOfDay;
        date = new DateTime(date.Year, date.Month, date.Day, currentTime.Hours, currentTime.Minutes, currentTime.Seconds);

        var newExpense = new Expense
        {
            Title = title,
            Description = description,
            Amount = amount,
            Date = date,
            UserId = _userId
        };

        _expenseRepository.Add(newExpense);
        _refreshCallback?.Invoke();
        Navigation.PopModalAsync();
        UnfocusAll();
    }

    private void UnfocusAll()
    {
        titleEntry.Unfocus();
        descriptionEntry.Unfocus();
        amountEntry.Unfocus();
        datePicker.Unfocus();
    }

    private void CancelButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}
