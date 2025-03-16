namespace Drinks
{
    /// <summary>
    /// Represents the response object containing a list of drinks with details.
    /// This is reduced version of <see cref="DrinkDetail"/>
    /// </summary>
    internal class DrinkDetailDTO
    {
        public string IdDrink { get; set; }
        public string DrinkName { get; set; }
        public string? DrinkAlternate { get; set; }
        public string? Category { get; set; }
        public string? Alcoholic { get; set; }
        public string? Glass { get; set; }
        public string? Instructions { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>();
        public List<string> Measures { get; set; } = new List<string>();

        /// <summary>
        /// Initializes new object of <see cref="DrinkDetailDTO"/>
        /// Maps relevant properties of <see cref="DrinkDetail"/>
        /// </summary>
        /// <param name="drinkDetail"><see cref="DrinkDetail"/> to be mapped to <see cref="DrinkDetailDTO"/></param>
        public DrinkDetailDTO(DrinkDetail drinkDetail)
        {
            IdDrink = drinkDetail.IdDrink;
            DrinkName = drinkDetail.StrDrink;
            DrinkAlternate = drinkDetail.StrDrinkAlternate;
            Category = drinkDetail.StrCategory;
            Alcoholic = drinkDetail.StrAlcoholic;
            Glass = drinkDetail.StrGlass;
            Instructions = drinkDetail.StrInstructions;

            for (int i = 1; i <= 15; i++)
            {
                string? ingredient = (string?)typeof(DrinkDetail).GetProperty($"StrIngredient{i}")?.GetValue(drinkDetail);
                string? measure = (string?)typeof(DrinkDetail).GetProperty($"StrMeasure{i}")?.GetValue(drinkDetail);

                if (!string.IsNullOrEmpty(ingredient))
                    Ingredients.Add(ingredient);

                if (!string.IsNullOrEmpty(measure))
                    Measures.Add(measure);
            }
        }
    }
}