namespace MyaFinance.MVVM.Views;
using MyaFinance.MVVM.Models;
using MyaFinance.Repositories;

public partial class EditIncome : ContentPage
{
    private Income _income;
    private IncomeRepository _incomeRepository;
    private Action _refreshCallback;

    public EditIncome(Income income, Action refreshCallback)
    {
        InitializeComponent();
        _income = income;
        _incomeRepository = new IncomeRepository();
        _refreshCallback = refreshCallback;

        // Initialize the fields with the current income data
        titleEntry.Text = _income.Title;
        descriptionEntry.Text = _income.Description;
        amountEntry.Text = _income.Amount.ToString();
        datePicker.Date = _income.Date;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        _income.Title = titleEntry.Text;
        _income.Description = descriptionEntry.Text;
        _income.Amount = double.Parse(amountEntry.Text);
        _income.Date = datePicker.Date;

        _incomeRepository.Update(_income);
        _refreshCallback?.Invoke();
        Navigation.PopModalAsync();
    }

    private void CancelButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}