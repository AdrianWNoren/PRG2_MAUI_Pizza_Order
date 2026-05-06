namespace PizzaApp.Model
{
    /// <summary>
    /// Interface för ett orderbart objekt.
    /// Implementeras av Ingredients, Pizza, Condiment och Drink.
    /// </summary>
    public interface IIngredient
    {
        /// <summary>Ger varje objekt ett unikt id</summary>
        Guid Id { get; set; }

        /// <summary>Objektets namn</summary>
        string Name { get; set; }

        /// <summary>Ett baspris för ett objekt</summary>
        double Price { get; set; }

        /// <summary>Om objektet är synligt eller ej(tillagt eller ej)</summary>
        bool IsVisible { get; set; }

        /// <summary>hur många av objektet som finns i användarens order</summary>
        int Quantity { get; set; }

        /// <summary>Totalt pris (pris * antal).</summary>
        double TotalPrice { get; }
    }
}
