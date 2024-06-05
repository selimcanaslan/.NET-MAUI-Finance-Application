using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MyaFinance.MVVM.Views;
using MyaFinance.MVVM.Models;
using Newtonsoft.Json.Linq;
using MyaFinance.Repositories;

namespace MyaFinance
{
    public partial class MainPage : ContentPage
    {
        private IncomeRepository _incomeRepository;
        private ExpenseRepository _expenseRepository;
        private HttpClient _client = new HttpClient();
        private const string BaseUrl = "https://cdn.jsdelivr.net/npm/@fawazahmed0/currency-api@latest/v1/";

        public static User currentUser;

        public MainPage(User user)
        {
            InitializeComponent();
            _expenseRepository = new ExpenseRepository();
            _incomeRepository = new IncomeRepository();
            currentUser = user;
            helloLabel.Text = $"Merhaba, {user.Name}";
            LoadCurrencyData();
            LoadLastTransacitons();
        }
        private void LoadLastTransacitons()
        {
            LoadLatestExpenses();
            LoadLatestIncomes();
        }
        private void LoadLatestIncomes()
        {
            List<Income> incomes = _incomeRepository.GetAll(currentUser.Id);

            int incomesListItemCount = incomes.Count;
            int i = 0;
            if (incomesListItemCount < 1 || incomes == null)
            {
                sondanIkinciIslemLabel.Text = "Henüz Gelir Girmediniz";
                sondanIkinciIslemLabel.BackgroundColor = Colors.Gray;
                sondanIkinciIslemFrame.BackgroundColor = Colors.Gray;
                sondanUcuncuIslemFrame.IsVisible = false;
                sondanUcuncuIslemLabel.IsVisible = false;
            }
            else
            {
                foreach (Income income in incomes)
                {
                    if (incomesListItemCount == 1)
                    {
                        sondanIkinciIslemLabel.Text = $"{income.Title} - {income.Amount}TL - {income.Date.ToString("dd MMM HH:mm")}";
                        sondanIkinciIslemLabel.BackgroundColor = Colors.Green;
                        sondanIkinciIslemFrame.BackgroundColor = Colors.Green;

                        sondanUcuncuIslemFrame.IsVisible = false;
                        sondanUcuncuIslemLabel.IsVisible = false;
                    }
                    else if (incomesListItemCount > 1)
                    {
                        if (i == 0)
                        {
                            sondanIkinciIslemLabel.Text = $"{income.Title} - {income.Amount}TL - {income.Date.ToString("dd MMM HH:mm")}";
                        }
                        if (i == 1)
                        {
                            sondanUcuncuIslemLabel.Text = $"{income.Title} - {income.Amount}TL - {income.Date.ToString("dd MMM HH:mm")}";
                        }
                    }
                    i++;
                }
            }
        }
        private void LoadLatestExpenses()
        {
            List<Expense> expenses = _expenseRepository.GetAll(currentUser.Id);

            int expenseListItemCount = expenses.Count;
            int i = 0;
            if (expenseListItemCount < 1 || expenses == null)
            {
                sonIslemLabel.Text = "Henüz Gider Girmediniz";
                sonIslemFrame.BackgroundColor = Colors.Gray;
                sonIslemFrame.BackgroundColor = Colors.Gray;
                sondanBirinciIslemFrame.IsVisible = false;
                sondanBirinciIslemLabel.IsVisible = false;
            }
            else
            {
                foreach (Expense expense in expenses)
                {
                    if (expenseListItemCount == 1)
                    {
                        sonIslemLabel.Text = $"{expense.Title} - {expense.Amount}TL - {expense.Date.ToString("dd MMM HH:mm")}";
                        sonIslemLabel.BackgroundColor = Colors.Red;
                        sonIslemFrame.BackgroundColor = Colors.Red;

                        sondanBirinciIslemFrame.IsVisible = false;
                        sondanBirinciIslemLabel.IsVisible = false;
                    }
                    else if (expenseListItemCount > 1)
                    {
                        if (i == 0)
                        {
                            sonIslemLabel.Text = $"{expense.Title} - {expense.Amount}TL - {expense.Date.ToString("dd MMM HH:mm")}";
                            sonIslemLabel.BackgroundColor = Colors.Red;
                            sonIslemFrame.BackgroundColor = Colors.Red;
                        }
                        if (i == 1)
                        {
                            sondanBirinciIslemLabel.Text = $"{expense.Title} - {expense.Amount}TL - {expense.Date.ToString("dd MMM HH:mm")}";
                            sondanBirinciIslemLabel.BackgroundColor = Colors.Red;
                            sondanBirinciIslemFrame.BackgroundColor = Colors.Red;
                        }
                    }
                    i++;
                }
            }
        }
        private async void LoadCurrencyData()
        {
            try
            {
                var euroCurrencies = await GetEuroBasedCurrenciesFromFallback();
                if (!string.IsNullOrEmpty(euroCurrencies))
                {
                    var currencyRates = ParseCurrencyRates(euroCurrencies);
                    if (currencyRates != null)
                    {
                        var formattedRates = FormatCurrencyRates(currencyRates);
                        euroCurrencyLabel.Text = formattedRates;
                    }
                    else
                    {
                        euroCurrencyLabel.Text = "Error: Parsed currency rates are null.";
                    }
                }
                else
                {
                    euroCurrencyLabel.Text = "Error: Euro-based currencies response is null or empty.";
                }
            }
            catch (Exception ex)
            {
                euroCurrencyLabel.Text = $"Error: {ex.Message}";
            }
        }

        private Dictionary<string, double> ParseCurrencyRates(string json)
        {
            var currencyRates = new Dictionary<string, double>();
            try
            {
                JObject jsonObject = JObject.Parse(json);
                var rates = jsonObject["eur"];
                foreach (JProperty rate in rates.Children())
                {
                    var currencyCode = rate.Name;
                    var exchangeRate = (double)rate.Value;
                    currencyRates.Add(currencyCode, exchangeRate);
                }
                return currencyRates;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON parsing error: {ex.Message}");
                return null;
            }
        }

        private string FormatCurrencyRates(Dictionary<string, double> currencyRates)
        {
            if (currencyRates == null || currencyRates.Count == 0)
            {
                return "No currency rates available.";
            }

            double euroRate = currencyRates["try"];
            double usdRate = currencyRates["usd"];
            double usdToTry = euroRate - ((euroRate * usdRate) - euroRate);
            dollarCurrencyLabel.Text = " " + usdToTry.ToString("F6") + " TL";
            return $" {euroRate:F6} TL";
        }

        private async Task<string> GetEuroBasedCurrencies()
        {
            try
            {
                var response = await _client.GetAsync($"{BaseUrl}currencies/eur.json");
                if (response != null && response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception("Error fetching Euro-based currencies");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching Euro-based currencies: {ex.Message}");
            }
        }

        private async Task<string> GetEuroBasedCurrenciesFromFallback()
        {
            try
            {
                var fallbackUrl = "https://latest.currency-api.pages.dev/v1/currencies/eur.json";
                var response = await _client.GetAsync(fallbackUrl);
                if (response != null && response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception("Fallback Error fetching Euro-based currencies");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Fallback Error fetching Euro-based currencies: {ex.Message}");
            }
        }
        private void RefreshTransactions()
        {
            LoadLastTransacitons();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshTransactions();
        }
        private void IncomeClicked(object sender, EventArgs e)
        {
            MyIncome myIncome = new MyIncome(currentUser.Id);
            Application.Current.MainPage.Navigation.PushModalAsync(myIncome);
        }

        private void ExpenseClicked(object sender, EventArgs e)
        {
            MyExpense myExpense = new MyExpense(currentUser.Id);
            Application.Current.MainPage.Navigation.PushModalAsync(myExpense);
        }

        private void Logout(object sender, EventArgs e)
        {
            Login login = new Login();
            Application.Current.MainPage.Navigation.PushModalAsync(login);
        }
    }
}
