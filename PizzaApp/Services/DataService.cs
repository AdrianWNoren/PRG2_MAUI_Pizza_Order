using PizzaApp.Model;
using System.Text.Json;

namespace PizzaApp.Services
{
    public static class DataService
    {
        private static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "pizzaapp_data.json");

        public static async Task SaveDataAsync(AppDataModel data)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(data, options);
            await File.WriteAllTextAsync(FilePath, json);
        }

        public static async Task<AppDataModel> LoadDataAsync()
        {
            if (!File.Exists(FilePath))
                return new AppDataModel();

            string json = await File.ReadAllTextAsync(FilePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<AppDataModel>(json, options) ?? new AppDataModel();
        }
    }

    public class AppDataModel
    {
        public List<Pizza> Pizzas { get; set; } = new();
        public List<Ingredients> AvailableIngredients { get; set; } = new();
        public List<Ingredients> Condiments { get; set; } = new(); 
        public List<Ingredients> Drinks { get; set; } = new();       
    }
}