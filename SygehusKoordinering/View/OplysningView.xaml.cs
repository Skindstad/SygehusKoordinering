using SygehusKoordinering.ViewModel;


namespace SygehusKoordinering.View;

public partial class OplysningView : ContentPage
{
	public OplysningView(OplysningViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
    }



}