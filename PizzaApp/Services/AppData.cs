using System.Collections.ObjectModel;
using PizzaApp.Model;

namespace PizzaApp.Services
{
    public static class AppData
    {
        public static ObservableCollection<Pizza> Pizzas { get; set; } = new();
        public static ObservableCollection<Ingredients> AvailableIngredients { get; set; } = new();
        public static ObservableCollection<Ingredients> Condiments { get; set; } = new();
        public static ObservableCollection<Ingredients> Drinks { get; set; } = new();
        public static ObservableCollection<object> CartItems { get; set; } = new();

        public static async Task LoadFromJsonAsync()
        {
            var data = await DataService.LoadDataAsync();
            Pizzas.Clear();
            foreach (var p in data.Pizzas)
            {
                p.Quantity = 0;
                Pizzas.Add(p);
            }
            AvailableIngredients.Clear();
            foreach (var i in data.AvailableIngredients)
            {
                i.Quantity = 0;
                AvailableIngredients.Add(i);
            }
            Condiments.Clear();
            foreach (var c in data.Condiments)
            {
                c.Quantity = 0;
                Condiments.Add(c);
            }
            Drinks.Clear();
            foreach (var d in data.Drinks)
            {
                d.Quantity = 0;
                Drinks.Add(d);
            }
        }

        public static async Task SaveToJsonAsync()
        {
            foreach (var p in Pizzas) p.Quantity = 0;
            foreach (var i in AvailableIngredients) i.Quantity = 0;
            foreach (var c in Condiments) c.Quantity = 0;
            foreach (var d in Drinks) d.Quantity = 0;

            var data = new AppDataModel
            {
                Pizzas = Pizzas.ToList(),
                AvailableIngredients = AvailableIngredients.ToList(),
                Condiments = Condiments.ToList(),
                Drinks = Drinks.ToList()
            };
            await DataService.SaveDataAsync(data);
        }

        public static async Task InitAsync()
        {
            if (!File.Exists(Path.Combine(FileSystem.AppDataDirectory, "pizzaapp_data.json")))
            {
                Pizzas.Add(new Pizza("Margherita", 89.0, new List<Ingredients>
                {
                    new Ingredients("Tomatsås", 0, true),
                    new Ingredients("Mozarella", 0, true)
                }));
                Pizzas.Add(new Pizza("Capricciosa", 99.0, new List<Ingredients>
                {
                    new Ingredients("Tomatsås", 0, true),
                    new Ingredients("Mozarella", 0, true),
                    new Ingredients("Skinka", 0, true),
                    new Ingredients("Champinjoner", 0, true)
                }));

                foreach (var ing in Ingredients.GetSampleIngredients())
                    AvailableIngredients.Add(ing);

                Condiments.Add(new Ingredients("Kebabsås", 15.0, true));
                Condiments.Add(new Ingredients("Vitlökssås", 15.0, true));
                Condiments.Add(new Ingredients("Stark sås", 15.0, true));

                Drinks.Add(new Ingredients("Coca Cola 33cl", 25.0, true));
                Drinks.Add(new Ingredients("Fanta 33cl", 25.0, true));
                Drinks.Add(new Ingredients("Sprite 33cl", 25.0, true));

                await SaveToJsonAsync();
            }
            else
            {
                await LoadFromJsonAsync();
            }
        }
    }
}