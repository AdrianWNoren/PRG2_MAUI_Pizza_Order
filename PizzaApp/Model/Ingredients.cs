using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PizzaApp.Model
{
    /// <summary>
    /// Representerar en pizzatopping eller orderbar vara
    /// </summary>
    public class Ingredients : IIngredient, INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsVisible { get; set; }

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

        public double TotalPrice => Price * Quantity;

        public Ingredients(string name, double price, bool isVisible)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            IsVisible = isVisible;
            Quantity = 0;
        }

        public static List<Ingredients> GetSampleIngredients()
        {
            return new List<Ingredients>
            {
                new Ingredients("Mozarella", 10.0, true),
                new Ingredients("Tomatsås", 5.0, true),
                new Ingredients("Oregano", 1.0, true),
                new Ingredients("Ananas", 7.0, true),
                new Ingredients("Skinka", 8.0, true),
                new Ingredients("Basilika", 1.0, true),
                new Ingredients("Salami", 9.0, true),
                new Ingredients("prosciutto", 15.0, true),
                new Ingredients("pommes", 20.0, true),
                new Ingredients("Gold flakes", 150.0, true),
                new Ingredients("kebab", 30.0, true),
                new Ingredients("Paprika", 6.0, true),
                new Ingredients("Champinjoner", 6.0, true),
                new Ingredients("Oliver", 7.0, true),
                new Ingredients("Köttfärs", 20.0, true),
                new Ingredients("Oxfilé", 30.0, true),
                new Ingredients("kebabsås", 15.0, true),
                new Ingredients("mild sås", 15.0, true),
                new Ingredients("vitlökssås", 15.0, true),
                new Ingredients("stark sås", 15.0, true),
                new Ingredients("blandad sås", 15.0, true),
                new Ingredients("bearneisesås", 15.0, true),
                new Ingredients("Glutenfri deg", 12.0, true)
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
