using MyaFinance.Repositories;

namespace MyaFinance
{
    public partial class App : Application
    {
        public static UserRepository userRepo {  get; private set; }
        public App(UserRepository repo)
        {
            InitializeComponent();

            userRepo = repo;

            MainPage = new Login();
        }
    }
}
