using SygehusKoordinering.Models;
using SygehusKoordinering.ViewModel;


namespace SygehusKoordinering.View;

public partial class OplysningView : ContentPage
{
	public OplysningView(OplysningViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
    }

    private void ItemList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (BindingContext is OplysningViewModel viewModel)
        {
            if (e.Item is Booking tappedBooking)
            {
                viewModel.ItemTapped(tappedBooking);
            }
        }
    }
}