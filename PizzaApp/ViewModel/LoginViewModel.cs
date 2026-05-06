using System.Windows.Input;
using PizzaApp.Services;

namespace PizzaApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        private string _password;
        private string _errorMessage;
        private bool _isLoading;

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand CancelCommand { get; }

        public event Action LoginSucceeded;
        public event Action LoginCancelled;

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await OnLoginAsync());
            CancelCommand = new Command(() => LoginCancelled?.Invoke());
        }

        private async Task OnLoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter email and password.";
                return;
            }

            IsLoading = true;
            ErrorMessage = string.Empty;

            bool success = await AuthService.LoginAsync(Email, Password);

            IsLoading = false;

            if (success)
                LoginSucceeded?.Invoke();
            else
                ErrorMessage = "Invalid email or password.";
        }
    }
}
