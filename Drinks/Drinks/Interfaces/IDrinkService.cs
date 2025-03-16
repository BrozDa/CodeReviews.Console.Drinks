
namespace Drinks
{
    internal interface IDrinkService
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Drink> GetDrinksInCategory(string category);
        IEnumerable<DrinkDetail> GetDrinkDetailsbyID(string drinkID);
        T DeserializeResponse<T>(string JsonData);
    }
}
