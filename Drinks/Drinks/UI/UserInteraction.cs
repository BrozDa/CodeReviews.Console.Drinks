using Spectre.Console;

namespace Drinks
{
    internal class UserInteraction
    {
        public DrinkService DrinkService { get; }

        private List<Category> _categories = new List<Category>();

        /// <summary>
        /// Initializes new instance of the <see cref="UserInteraction"/> class.
        /// </summary>
        /// <param name="drinkService"></param>
        public UserInteraction(DrinkService drinkService)
        {
            DrinkService = drinkService;
        }

        /// <summary>
        /// Runs the application, allowing users to select drinks and view details.
        /// </summary>
        public void RunApp()
        {
            bool isAppRunning = true;

            SetCategories();

            while (isAppRunning)
            {
                try
                {
                    string category = GetCategoryFromUser(_categories);

                    List<Drink> drinksInCategory = GetDrinksInCategory(category);
                    string drinkID = GetDrinkIdFromCategoryFromUser(drinksInCategory);

                    var drinkDetail = GetDrinkDetails(drinkID);

                    if (drinkDetail == null)
                    {
                        throw new Exception("Drink was not successfully retrieved");
                    }

                    PrintDrinkDetailDTO(new DrinkDetailDTO(drinkDetail));
                    isAppRunning = ShouldContinue();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Somethin went wrong");
                    Console.WriteLine($"Details: {ex.Message}");

                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// Retrieves and stores the list of drink categories.
        /// Exits the application if the request fails.
        /// </summary>
        private void SetCategories()
        {
            try
            {
                _categories = GetCategories();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Somethin went wrong");
                Console.WriteLine($"Details: {ex.Message}");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Retrieves the list of available drink categories.
        /// </summary>
        /// <returns>A list of <see cref="Category"/> objects.</returns>
        public List<Category> GetCategories() => DrinkService.GetCategories().ToList();

        /// <summary>
        /// Retrieves a list of drinks available in the specified category.
        /// </summary>
        /// <param name="category">The drink category name.</param>
        /// <returns>A list of <see cref="Drink"/> objects in the given category.</returns>
        public List<Drink> GetDrinksInCategory(string category) => DrinkService.GetDrinksInCategory(category).ToList();

        /// <summary>
        /// Retrieves details for a specific drink based on its ID.
        /// </summary>
        /// <param name="drinkId">The ID of the drink.</param>
        /// <returns>A <see cref="DrinkDetail"/> object.</returns>
        public DrinkDetail? GetDrinkDetails(string drinkId) => DrinkService.GetDrinkDetailsbyID(drinkId).FirstOrDefault();

        /// <summary>
        /// Prompts the user to select a drink category from the available list.
        /// </summary>
        /// <param name="categories">List of available drink categories.</param>
        /// <returns>The selected category name.</returns>
        public string GetCategoryFromUser(List<Category> categories)
        {
            var category = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Please select category")
                .AddChoices(categories.Select(x => x.StrCategory))
                .PageSize(categories.Count)
                .EnableSearch()
                .WrapAround()
                );

            return category.Replace(' ', '_');
        }

        /// <summary>
        /// Prompts the user to select a drink from the chosen category.
        /// </summary>
        /// <param name="drinksInCategory">List of drinks available in the selected category.</param>
        /// <returns>The ID of the selected drink.</returns>
        public string GetDrinkIdFromCategoryFromUser(List<Drink> drinksInCategory)
        {
            var drink = AnsiConsole.Prompt(
                new SelectionPrompt<Drink>()
                .Title("Please select a drink from list")
                .AddChoices(drinksInCategory)
                .UseConverter(x => x.StrDrink)
                .PageSize(drinksInCategory.Count)
                .EnableSearch()
                .WrapAround()
                );

            return drink.IdDrink;
        }

        /// <summary>
        /// Displays detailed information about the selected drink in a formatted table.
        /// </summary>
        /// <param name="drinkDetailDTO">The drink details to display.</param>
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

            for (; index < drinkDetailDTO.Measures.Count; index++)
            {
                table.AddRow($"Ingredient {index + 1}", drinkDetailDTO.Measures[index] + " " + drinkDetailDTO.Ingredients[index]);
            }

            //printing leftover ingredients withou measure
            for (; index < drinkDetailDTO.Ingredients.Count; index++)
            {
                table.AddRow($"Ingredient {index + 1}", drinkDetailDTO.Ingredients[index]);
            }

            AnsiConsole.Write(table);
        }

        /// <summary>
        /// Asks the user whether they want to continue using the app.
        /// </summary>
        /// <returns><c>true</c> if the user wants to continue; otherwise, <c>false</c>.</returns>
        public bool ShouldContinue()
        {
            Console.WriteLine("Press 'ENTER' to continue or any other key to exit the app.");
            Console.CursorVisible = false;

            return Console.ReadKey(true).Key == ConsoleKey.Enter;
        }
    }
}