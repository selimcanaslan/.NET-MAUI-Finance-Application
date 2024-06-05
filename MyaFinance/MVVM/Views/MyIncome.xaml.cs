using MyaFinance.MVVM.Models;
using MyaFinance.MVVM.Views;
using MyaFinance.Repositories;
using System;
using System.Collections.Generic;

namespace MyaFinance;

public partial class MyIncome : ContentPage
{
    private int _userId;
    private IncomeRepository _incomeRepository;

    public MyIncome(int id)
    {
        _userId = id;
        _incomeRepository = new IncomeRepository();
        InitializeComponent();
        LoadIncomes();
    }

    private void LoadIncomes()
    {
        List<Income> incomes = _incomeRepository.GetAll(_userId);
        incomeListView.ItemsSource = incomes;
        if (incomes == null || incomes.Count < 1)
        {
            noIncomeLabel.IsVisible = true;
        }
        else
        {
            noIncomeLabel.IsVisible = false;
        }
    }

    private void GoBack(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void AddNewIncomeButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new AddNewIncome(_userId, RefreshIncomeList));
    }

    private void EditButton_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var incomeId = (int)button.CommandParameter;

        var income = _incomeRepository.Get(incomeId);

        if (income != null)
        {
            Navigation.PushModalAsync(new EditIncome(income, RefreshIncomeList));
        }
        else
        {
            DisplayAlert("Error", "Income not found", "OK");
        }
    }

    private void DeleteButton_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var incomeId = (int)button.CommandParameter;
        _incomeRepository.Delete(incomeId);
        DisplayAlert("Success", "Record Deleted Succesfully", "OK");
        LoadIncomes();
    }

    private void RefreshIncomeList()
    {
        var incomes = _incomeRepository.GetAll(_userId);
        incomeListView.ItemsSource = incomes;
        if (incomes == null || incomes.Count < 1)
        {
            noIncomeLabel.IsVisible = true;
        }
        else
        {
            noIncomeLabel.IsVisible = false;
        }
    }

    private void incomeListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }
}
