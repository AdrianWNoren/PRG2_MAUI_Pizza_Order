using PizzaApp.ViewModels;

namespace PizzaApp;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _vm;

    public LoginPage()
    {
        InitializeComponent();
        _vm = new LoginViewModel();
        BindingContext = _vm;

        _vm.LoginSucceeded += async () =>
            await Shell.Current.GoToAsync(nameof(AdminPage));

        _vm.LoginCancelled += async () =>
            await Shell.Current.GoToAsync("..");
    }
}
