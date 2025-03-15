using Spectre.Console;
using System.Reflection;
using System.Threading.Channels;

namespace Drinks
{
    internal class UserInteraction
    {
        public DrinkService DrinkService { get;}
        private List<Category> Categories { get; set; }

        public UserInteraction(DrinkService DrinkService)
        {
            this.DrinkService = DrinkService;
        }

        public void RunService()
        {
            Categories = GetCategories();

            string category = GetCategoryFromUser(Categories);

            var drinksInCategory = GetDrinksInCategory(category);
            
            var drinkID = GetDrinkIdFromCategoryFromUser(drinksInCategory);

            var drinkDetail = GetDrinkDetails(drinkID);

            DrinkDetailDTO drinkDetailDTO = new DrinkDetailDTO();
            drinkDetailDTO.MapDetailToDTO(drinkDetail[0]);

            PrintDrinkDetailDTO(drinkDetailDTO);

        }

        public List<Category> GetCategories()
        {
           return DrinkService.GetCategories();

        }
        public List<Drink> GetDrinksInCategory(string category)
        {
            return DrinkService.GetDrinksInCategory(category);
        }
        public List<DrinkDetail> GetDrinkDetails(string drinkId)
        {
            return DrinkService.GetDrinkDetailsbyID(drinkId);
        }
        public string GetCategoryFromUser(List<Category> categories)
        {
            var category = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Please select category \n")
                .AddChoices(categories.Select(x => x.StrCategory))
                .PageSize(categories.Count)
                );

            return category.Replace(' ', '_');
        }
        public string GetDrinkIdFromCategoryFromUser(List<Drink> drinksInCategory)
        {

            var drink = AnsiConsole.Prompt(
                new SelectionPrompt<Drink>()
                .Title("Please select a drink from list")
                .AddChoices<Drink>(drinksInCategory)
                .UseConverter(x => x.StrDrink)
                .PageSize(drinksInCategory.Count)
                );

            return drink.IdDrink;
        }
        public void PrintDrinkDetailDTO(DrinkDetailDTO drinkDetailDTO)
        {
            var table = new Table();
            table.AddColumns("Drink Property", "Value");
            var properties = typeof(DrinkDetailDTO).GetProperties();

            foreach (var property in properties) 
            {
                var prop = property.GetValue(drinkDetailDTO) as string;

                if (prop is not null)
                {
                    table.AddRow(property.Name, prop);
                }
            }

            int index = 0;

            for(;index < drinkDetailDTO.Measures.Count; index++)
            {
                table.AddRow($"Ingredient {index + 1}", drinkDetailDTO.Measures[index] +" "+ drinkDetailDTO.Ingredients[index]);
            }

            for (; index < drinkDetailDTO.Ingredients.Count; index++)
            {
                table.AddRow($"Ingredient {index + 1}", drinkDetailDTO.Ingredients[index]);
            }

            AnsiConsole.Write(table);
        }
       
        

        
        
    }
}
