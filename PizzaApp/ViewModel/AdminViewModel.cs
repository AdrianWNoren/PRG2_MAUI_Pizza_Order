using System.Collections.ObjectModel;
using System.Windows.Input;
using PizzaApp.Model;
using PizzaApp.Services;

namespace PizzaApp.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        private string _selectedType;
        private string _productName;
        private string _searchText;
        private double _price;
        private ObservableCollection<Ingredients> _filteredIngredients;
        private ObservableCollection<Ingredients> _selectedIngredients;

        public ObservableCollection<Ingredients> AllIngredients => AppData.AvailableIngredients;
        public List<string> ProductTypes { get; } = new() { "Ingredient", "Condiment", "Drink", "Pizza" };

        public string SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsPizzaSelected)); 
            }
        }
        public bool IsPizzaSelected => SelectedType == "Pizza";

        public string ProductName
        {
            get => _productName;
            set { _productName = value; OnPropertyChanged(); }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterIngredients();
            }
        }

        public double Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Ingredients> FilteredIngredients
        {
            get => _filteredIngredients;
            set { _filteredIngredients = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Ingredients> SelectedIngredients
        {
            get => _selectedIngredients;
            set { _selectedIngredients = value; OnPropertyChanged(); }
        }

        public ICommand AddIngredientCommand { get; }
        public ICommand RemoveIngredientCommand { get; }
        public ICommand SaveProductCommand { get; }

        public AdminViewModel()
        {
            FilteredIngredients = new ObservableCollection<Ingredients>(AllIngredients);
            SelectedIngredients = new ObservableCollection<Ingredients>();
            SelectedType = ProductTypes[0];

            AddIngredientCommand = new Command<Ingredients>(AddIngredient);
            RemoveIngredientCommand = new Command<Ingredients>(RemoveIngredient);
            SaveProductCommand = new Command(async () => await SaveProduct());
        }

        private void FilterIngredients()
        {
            FilteredIngredients.Clear();
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? AllIngredients
                : AllIngredients.Where(i => i.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            foreach (var item in filtered)
                FilteredIngredients.Add(item);
        }

        private void AddIngredient(Ingredients ingredient)
        {
            if (!SelectedIngredients.Any(i => i.Id == ingredient.Id))
                SelectedIngredients.Add(ingredient);
        }

        private void RemoveIngredient(Ingredients ingredient)
        {
            SelectedIngredients.Remove(ingredient);
        }

        private async Task SaveProduct()
        {
            if (string.IsNullOrWhiteSpace(ProductName))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter a name", "OK");
                return;
            }

            if (Price <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter a valid price", "OK");
                return;
            }

            switch (SelectedType)
            {
                case "Ingredient":
                    var newIngredient = new Ingredients(ProductName, Price, true);
                    AppData.AvailableIngredients.Add(newIngredient);
                    await AppData.SaveToJsonAsync();
                    FilterIngredients();
                    await Application.Current.MainPage.DisplayAlert("Success", $"Ingredient '{ProductName}' added", "OK");
                    ClearForm();
                    break;

                case "Condiment":
                    var newCondiment = new Condiment(ProductName, Price, true);
                    AppData.Condiments.Add(newCondiment);
                    await AppData.SaveToJsonAsync();
                    await Application.Current.MainPage.DisplayAlert("Success", $"Condiment '{ProductName}' added", "OK");
                    ClearForm();
                    break;

                case "Drink":
                    var newDrink = new Drink(ProductName, Price, true);
                    AppData.Drinks.Add(newDrink);
                    await AppData.SaveToJsonAsync();
                    await Application.Current.MainPage.DisplayAlert("Success", $"Drink '{ProductName}' added", "OK");
                    ClearForm();
                    break;

                case "Pizza":
                    if (SelectedIngredients.Count == 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Please add at least one ingredient to the pizza", "OK");
                        return;
                    }
                    var newPizza = new Pizza(ProductName, Price, SelectedIngredients.ToList());
                    AppData.Pizzas.Add(newPizza);
                    await AppData.SaveToJsonAsync();
                    await Application.Current.MainPage.DisplayAlert("Success", $"Pizza '{ProductName}' added with {SelectedIngredients.Count} ingredients", "OK");
                    ClearForm();
                    SelectedIngredients.Clear();
                    break;
            }
        }

        private void ClearForm()
        {
            ProductName = string.Empty;
            Price = 0;
            SearchText = string.Empty;
        }
    }
}