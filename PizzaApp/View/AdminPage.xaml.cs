using PizzaApp.ViewModels;

namespace PizzaApp;

public partial class AdminPage : ContentPage
{
    public AdminPage()
    {
        InitializeComponent();
        BindingContext = new AdminViewModel();
    }
}