using MyaFinance.MVVM.Models;
using MyaFinance.MVVM.Views;

namespace MyaFinance
{
    public partial class MainPage : ContentPage
    {
        public static User currentUser;
        public MainPage(User user)
        {
            InitializeComponent();
            currentUser = user;
            helloLabel.Text = $"Merhaba, {user.Name}";
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
    }

}
