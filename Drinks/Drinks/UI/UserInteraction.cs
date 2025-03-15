using Spectre.Console;
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
            
            var drink = GetDrinkFromCategoryFromUser(drinksInCategory);
        }

        public List<Category> GetCategories()
        {
           return DrinkService.GetCategories();

        }
        public List<Drink> GetDrinksInCategory(string category)
        {
            return DrinkService.GetDrinksInCategory(category);
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
        public string GetDrinkFromCategoryFromUser(List<Drink> drinksInCategory)
        {

            var drinkId = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Please select a drink from list")
                .AddChoices(drinksInCategory.Select(x => x.StrDrink))
                .PageSize(drinksInCategory.Count)
                );

            Console.WriteLine(drinkId);

            return drinkId;
        }

        
        
    }
}
