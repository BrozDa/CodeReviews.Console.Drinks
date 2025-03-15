using System.Reflection;

namespace Drinks
{
    internal class DrinkDetailDTO
    {
        public string IdDrink { get; set; }
        public string DrinkName { get; set; }
        public string? DrinkAlternate { get; set; }
        public string? Category { get; set; }
        public string? Alcoholic { get; set; }
        public string? Glass { get; set; }
        public string Instructions { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>();
        public List<string> Measures { get; set; } = new List<string>();


        public void MapDetailToDTO(DrinkDetail drinkDetail)
        {

            IdDrink = drinkDetail.IdDrink;
            DrinkName = drinkDetail.StrDrink;
            DrinkAlternate = drinkDetail.StrDrinkAlternate;
            Glass = drinkDetail.StrGlass;
            Instructions = drinkDetail.StrInstructions;
            Category = drinkDetail.StrCategory;
            Alcoholic = drinkDetail.StrAlcoholic;

            for (int i = 1; i <= 15; i++) 
            {
                var ingredientProp = typeof(DrinkDetail).GetProperty($"StrIngredient{i}");
                var measureProp = typeof(DrinkDetail).GetProperty($"StrMeasure{i}");

                if (ingredientProp == null)
                {
                    throw new Exception($"Property StrIngredient{i} not found on DrinkDetail.");
                }
                if (measureProp == null)
                {
                    throw new Exception($"Property StrMeasure{i} not found on DrinkDetail.");
                }


                var ingredient = ingredientProp.GetValue(drinkDetail) as string; 

                if(!string.IsNullOrEmpty(ingredient))
                {
                    Ingredients.Add(ingredient);
                }

                var measure = measureProp.GetValue(drinkDetail) as string;

                if (measure is not null)
                {
                    Measures.Add(measure);
                }
            }
        }
    }

    
}
