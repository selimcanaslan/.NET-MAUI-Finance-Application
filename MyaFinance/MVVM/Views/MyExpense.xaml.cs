namespace MyaFinance.MVVM.Views;

public partial class MyExpense : ContentPage
{
    private int _userId;
    public MyExpense(int id)
	{
        _userId = id;
        InitializeComponent();
	}
    private void GoBack(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}