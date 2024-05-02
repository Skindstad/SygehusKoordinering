using SygehusKoordinering.ViewModel;

namespace SygehusKoordinering.View;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        

    }
}