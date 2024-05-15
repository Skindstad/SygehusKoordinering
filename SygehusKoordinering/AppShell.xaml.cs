using SygehusKoordinering.View;
using SygehusKoordinering.ViewModel;

namespace SygehusKoordinering
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(OprettelseBookingView), typeof(OprettelseBookingView));
            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
            Routing.RegisterRoute(nameof(OplysningView), typeof(OplysningView));
            Routing.RegisterRoute(nameof(ItemView), typeof(ItemView));
            Routing.RegisterRoute(nameof(PersonaleListeView), typeof(PersonaleListeView));
            Routing.RegisterRoute(nameof(ConformationView), typeof(ConformationView));
        }
    }
}
