using PizzaApp.Model;
using PizzaApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PizzaApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Pizza> Pizzas => AppData.Pizzas;
        public ObservableCollection<Ingredients> Drinks => AppData.Drinks;
        public ObservableCollection<Ingredients> Condiments => AppData.Condiments;

        public ICommand AddToCartCommand { get; }
        public ICommand GoToCartCommand { get; }

        public MainViewModel()
        {
            AddToCartCommand = new Command<object>(AddToCart);
            GoToCartCommand = new Command(async () => await GoToCart());
        }

        private void AddToCart(object item)
        {
            if (item == null) return;

            if (item is Pizza pizza)
            {
                int qty = pizza.Quantity > 0 ? pizza.Quantity : 1;
                var cartPizza = new Pizza(pizza.Name, pizza.BasePrice, pizza.Ingredients)
                {
                    Quantity = qty
                };
                AppData.CartItems.Add(cartPizza);
                pizza.Quantity = 0;
            }
            else if (item is Ingredients ingredient)
            {
                int qty = ingredient.Quantity > 0 ? ingredient.Quantity : 1;
                var cartItem = new Ingredients(ingredient.Name, ingredient.Price, ingredient.IsVisible)
                {
                    Quantity = qty
                };
                AppData.CartItems.Add(cartItem);
                ingredient.Quantity = 0; 
            }

            Application.Current.MainPage.DisplayAlert("Added", $"Item added to cart", "OK");
        }

        private async Task GoToCart()
        {
            await Shell.Current.GoToAsync(nameof(CartPage));
        }
    }
}