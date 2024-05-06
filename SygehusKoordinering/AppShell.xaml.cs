using SygehusKoordinering.View;

namespace SygehusKoordinering
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
            Routing.RegisterRoute(nameof(OplysningView), typeof(OplysningView));
        }
    }
}
