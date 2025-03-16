namespace Drinks
{
    /// <summary>
    /// Provides methods for retrieving drink-related data from an API.
    /// </summary>
    internal interface IDrinkService
    {
        /// <summary>
        /// Retrieves a list of drink categories available in the API.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> containing <see cref="Category"/> objects.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="drinkID"/> is null or empty.</exception>
        IEnumerable<Category> GetCategories();

        /// <summary>
        /// Retrieves a list of drink within category available in the API.
        /// </summary>
        /// <param name="category"><see cref="string"/> value representing category</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing <see cref="Drink"/> objects.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="drinkID"/> is null or empty.</exception>
        IEnumerable<Drink> GetDrinksInCategory(string category);

        /// <summary>
        /// Retrieves a list of drink details available in the API.
        /// </summary>
        /// <param name="category"><see cref="string"/> value representing drink ID</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing <see cref="DrinkDetail"/> objects.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="drinkID"/> is null or empty.</exception>
        IEnumerable<DrinkDetail> GetDrinkDetailsbyID(string drinkID);

        /// <summary>
        /// Deserializes JSON data into a specified object type.
        /// </summary>
        /// <typeparam name="T">The type to which the JSON data should be deserialized.</typeparam>
        /// <param name="JsonData">The JSON string to deserialize.</param>
        /// <returns>An instance of type <typeparamref name="T"/>.</returns>
        /// <exception cref="JsonException">Thrown if deserialization fails due to invalid JSON format.</exception>
        T DeserializeResponse<T>(string JsonData);
    }
}