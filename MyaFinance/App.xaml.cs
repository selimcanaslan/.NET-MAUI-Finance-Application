using MyaFinance.Repositories;
using System.Globalization;

namespace MyaFinance
{
    public partial class App : Application
    {
        public static UserRepository userRepo {  get; private set; }
        public App(UserRepository repo)
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("tr-TR");
            userRepo = repo;

            MainPage = new Login();
        }
    }
}
