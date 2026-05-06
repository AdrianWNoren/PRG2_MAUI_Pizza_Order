namespace PizzaApp.Model
{
    /// <summary>
    /// Tillbehör som kan beställas (sallader, såser mm)
    /// Ärver från Ingredients och implementerar IIngredient.
    /// </summary>
    public class Condiment : Ingredients
    {
        public Condiment(string name, double price, bool isVisible)
            : base(name, price, isVisible) { }
    }
}
