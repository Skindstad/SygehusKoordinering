using SygehusKoordinering.View;

namespace SygehusKoordinering
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage()
            MainPage = new AppShell();
        }
    }
}
