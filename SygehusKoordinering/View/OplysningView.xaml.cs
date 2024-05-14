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

    private void Oprettelse_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is OplysningViewModel viewModel)
        {
          viewModel.Oprettelse();
        }
    }

    private void PersonaleListe_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is OplysningViewModel viewModel)
        {
            viewModel.PersonaleListe();
        }
    }
}