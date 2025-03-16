using Spectre.Console;

namespace Drinks
{
    internal class UserInteraction
    {
        public DrinkService DrinkService { get;}
        private List<Category> Categories { get; set; }

        public UserInteraction(DrinkService DrinkService)
        {
            this.DrinkService = DrinkService;
            Categories = GetCategories();
        }

        public void RunService()
        {
            bool isAppRunning = true;

            while (isAppRunning)
            {
                try
                {
                    string category = GetCategoryFromUser(Categories);
                    List<Drink> drinksInCategory = GetDrinksInCategory(category);
                    string drinkID = GetDrinkIdFromCategoryFromUser(drinksInCategory);

                    DrinkDetail drinkDetail = GetDrinkDetails(drinkID)[0];

                    DrinkDetailDTO drinkDetailDTO = new DrinkDetailDTO(drinkDetail);

                    PrintDrinkDetailDTO(drinkDetailDTO);

                    isAppRunning = ShouldContinue();
                    Console.Clear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Somethin went wrong");
                    Console.WriteLine($"Details: {ex.Message}");
                }

            }

        }
        public List<Category> GetCategories()
        {
           return DrinkService.GetCategories().ToList();

        }
        public List<Drink> GetDrinksInCategory(string category)
        {
            return DrinkService.GetDrinksInCategory(category).ToList();
        }
        public List<DrinkDetail> GetDrinkDetails(string drinkId)
        {
            return DrinkService.GetDrinkDetailsbyID(drinkId).ToList();
        }
        public string GetCategoryFromUser(List<Category> categories)
        {
            var category = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Please select category")
                .AddChoices(categories.Select(x => x.StrCategory))
                .PageSize(categories.Count)
                .EnableSearch()
                );

            return category.Replace(' ', '_');
        }
        public string GetDrinkIdFromCategoryFromUser(List<Drink> drinksInCategory)
        {

            var drink = AnsiConsole.Prompt(
                new SelectionPrompt<Drink>()
                .Title("Please select a drink from list")
                .AddChoices(drinksInCategory)
                .UseConverter(x => x.StrDrink)
                .PageSize(drinksInCategory.Count)
                .EnableSearch()
                
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

            //printing leftover ingredients withou measure
            for (; index < drinkDetailDTO.Ingredients.Count; index++)
            {
                table.AddRow($"Ingredient {index + 1}", drinkDetailDTO.Ingredients[index]);
            }

            AnsiConsole.Write(table);
        }
        public bool ShouldContinue()
        {
            Console.WriteLine("Press 'ENTER' to continue or any other key to exit the app.");

            return Console.ReadKey(true).Key == ConsoleKey.Enter;
        }
       
        

        
        
    }
}
