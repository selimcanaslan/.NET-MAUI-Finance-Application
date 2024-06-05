using MyaFinance.Repositories;
using System.Globalization;

namespace MyaFinance
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("tr-TR");

            MainPage = new Login();
        }
    }
}
