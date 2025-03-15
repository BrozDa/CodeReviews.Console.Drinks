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
            GetCategories();
            Console.WriteLine(GetCategoryFromUser(Categories));
        }

        public void GetCategories()
        {
           Categories = DrinkService.GetCategories();

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
        
    }
}
