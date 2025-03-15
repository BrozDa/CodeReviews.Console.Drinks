using System.Reflection;

namespace Drinks
{
    internal class DrinkDetailDTO
    {
        public string IdDrink { get; set; }
        public string Drink { get; set; }
        public string? DrinkAlternate { get; set; }
        public string? Category { get; set; }
        public string? Alcoholic { get; set; }
        public string? Glass { get; set; }
        public string Instructions { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Measures { get; set; }


        public void MapDetailToDTO(DrinkDetail drinkDetail)
        {
            DrinkDetailDTO drinkDetailDTO = new DrinkDetailDTO();

            IdDrink = drinkDetail.IdDrink;
            Drink = drinkDetail.StrDrink;
            DrinkAlternate = drinkDetail.StrDrinkAlternate;
            Category = drinkDetail.StrCategory;
            Alcoholic = drinkDetail.StrAlcoholic;

            for (int i = 1; i <= 15; i++) 
            {
                var ingredientProp = typeof(DrinkDetail).GetProperty($"StrIngredient{i}");
                var measureProp = typeof(DrinkDetail).GetProperty($"StrMeasure{i}");

                string ingredient = ingredientProp.GetValue(this) as string;

                if(ingredient is not null)
                {
                    Ingredients.Add( ingredient );
                }

                string measure = measureProp.GetValue(this) as string;

                if (measure is not null)
                {
                    Measures.Add(measure);
                }
            }
        }
    }

    
}
