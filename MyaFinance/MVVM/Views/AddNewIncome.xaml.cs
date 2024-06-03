using MyaFinance.Repositories;
using MyaFinance.MVVM.Models;
using System;

namespace MyaFinance
{
    public partial class AddNewIncome : ContentPage
    {
        private int _userId;
        private IncomeRepository _incomeRepository;
        private Action _refreshCallback;

        public AddNewIncome(int userId, Action refreshCallback)
        {
            InitializeComponent();
            _userId = userId;
            _incomeRepository = new IncomeRepository();
            _refreshCallback = refreshCallback;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            var title = titleEntry.Text;
            var description = descriptionEntry.Text;
            var amount = double.Parse(amountEntry.Text);
            var date = datePicker.Date;

            var newIncome = new Income
            {
                Title = title,
                Description = description,
                Amount = amount,
                Date = date,
                UserId = _userId
            };

            _incomeRepository.Add(newIncome);
            _refreshCallback?.Invoke();
            Navigation.PopModalAsync();
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
