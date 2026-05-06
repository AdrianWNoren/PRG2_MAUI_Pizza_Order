using PizzaApp.Model;
using PizzaApp.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;

namespace PizzaApp.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        private double _totalPrice;

        public ObservableCollection<object> CartItems => AppData.CartItems;

        public double TotalPrice
        {
            get => _totalPrice;
            set { _totalPrice = value; OnPropertyChanged(); }
        }

        public ICommand PlaceOrderCommand { get; }

        public CartViewModel()
        {
            PlaceOrderCommand = new Command(async () => await PlaceOrder());
            CartItems.CollectionChanged += OnCartChanged;

            foreach (var item in CartItems)
                SubscribeToItem(item);

            UpdateTotal();
        }

        private void OnCartChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (var item in e.NewItems)
                    SubscribeToItem(item);

            if (e.OldItems != null)
                foreach (var item in e.OldItems)
                    UnsubscribeFromItem(item);

            UpdateTotal();
        }

        private void SubscribeToItem(object item)
        {
            if (item is INotifyPropertyChanged notifier)
                notifier.PropertyChanged += OnItemPropertyChanged;
        }

        private void UnsubscribeFromItem(object item)
        {
            if (item is INotifyPropertyChanged notifier)
                notifier.PropertyChanged -= OnItemPropertyChanged;
        }

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Pizza.TotalPrice) ||
                e.PropertyName == nameof(Pizza.Quantity) ||
                e.PropertyName == nameof(Ingredients.TotalPrice) ||
                e.PropertyName == nameof(Ingredients.Quantity))
            {
                UpdateTotal();
            }
        }

        private void UpdateTotal()
        {
            double total = 0;
            foreach (var item in CartItems)
            {
                if (item is Pizza p) total += p.TotalPrice;
                else if (item is Ingredients i) total += i.TotalPrice;
            }
            TotalPrice = total;
        }

        private async Task PlaceOrder()
        {
            if (CartItems.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Cart empty", "Add items before placing order.", "OK");
                return;
            }
            await Application.Current.MainPage.DisplayAlert("Order Placed", $"Your order has been placed! Total: {TotalPrice:F2} kr", "OK");
            CartItems.Clear();
            UpdateTotal();
        }
    }
}