using SygehusKoordinering.ViewModel;

namespace SygehusKoordinering.View;

public partial class ConformationView : ContentPage
{
	public ConformationView(ConformationViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}