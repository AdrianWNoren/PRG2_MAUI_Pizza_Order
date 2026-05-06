using PizzaApp.ViewModels;

namespace PizzaApp;

public partial class CartPage : ContentPage
{
    public CartPage()
    {
        InitializeComponent();
        BindingContext = new CartViewModel();
    }
}