using SygehusKoordinering.View;

namespace SygehusKoordinering
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
        }
    }
}
