using SygehusKoordinering.ViewModel;


namespace SygehusKoordinering.View;

public partial class OplysningView : ContentPage
{
    private OplysningViewModel viewModel = new OplysningViewModel();
	public OplysningView(OplysningViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
    }

    private void ItemList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        viewModel.ObjectSelected = ItemList.SelectedItem;
    }
}