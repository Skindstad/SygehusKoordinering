using SygehusKoordinering.ViewModel;
namespace SygehusKoordinering.View;

public partial class PersonaleListeView : ContentPage
{
	public PersonaleListeView(PersonaleListeViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
    }
}