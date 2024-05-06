using SygehusKoordinering.ViewModel;

namespace SygehusKoordinering.View;

public partial class OprettelseBookingView : ContentPage
{
	public OprettelseBookingView(OprettelseBookingViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}