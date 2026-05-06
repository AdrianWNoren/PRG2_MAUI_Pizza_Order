using PizzaApp.ViewModels;

namespace PizzaApp.Model
{
    /// <summary>
    /// Pizza med ett baspris och oignredienser som kan läggas till eller tas bort.
    /// Ärver från BaseViewModel och implementerar IIngredient för att hantera property change notifications.
    /// </summary>
    public class Pizza : BaseViewModel, IIngredient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public double BasePrice { get; set; }
        public List<Ingredients> Ingredients { get; set; }

        /// <summary>Total price including all ingredients.</summary>
        public double Price => BasePrice + Ingredients.Sum(i => i.Price);

        /// <summary>Total price multiplied by quantity.</summary>
        public double TotalPrice => Price * Quantity;

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        // IIngredient requires Price with a setter, so we expose BasePrice as the setter
        double IIngredient.Price
        {
            get => Price;
            set => BasePrice = value;
        }

        public Pizza(string name, double basePrice, List<Ingredients> ingredients)
        {
            Id = Guid.NewGuid();
            Name = name;
            BasePrice = basePrice;
            Ingredients = ingredients ?? new List<Ingredients>();
            IsVisible = true;
            Quantity = 0;
        }

        public void AddIngredient(Ingredients ingredient)
        {
            if (!Ingredients.Contains(ingredient))
            {
                Ingredients.Add(ingredient);
                OnPropertyChanged(nameof(Ingredients));
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public void RemoveIngredient(Ingredients ingredient)
        {
            if (Ingredients.Contains(ingredient))
            {
                Ingredients.Remove(ingredient);
                OnPropertyChanged(nameof(Ingredients));
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public void calculatePrice()
        {
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(TotalPrice));
        }
    }
}
