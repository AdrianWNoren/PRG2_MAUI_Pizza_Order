namespace PizzaApp.Model
{
    /// <summary>
    /// Drickor som kan beställas (läsk, öl mm)
    /// Ärver från Ingredients och implementerar IIngredient.
    /// </summary>
    public class Drink : Ingredients
    {
        public Drink(string name, double price, bool isVisible)
            : base(name, price, isVisible) { }
    }
}
