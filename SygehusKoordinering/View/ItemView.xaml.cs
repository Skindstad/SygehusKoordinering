using SygehusKoordinering.ViewModel;

namespace SygehusKoordinering.View;

public partial class ItemView : ContentPage
{
	public ItemView(ItemViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}