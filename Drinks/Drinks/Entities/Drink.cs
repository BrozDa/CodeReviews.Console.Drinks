using System.Text.Json.Serialization;

namespace Drinks
{
    /// <summary>
    /// Represents the response object containing a list of drinks with basic information in specific category.
    /// </summary>
    internal class DrinkResponse
    {
        [JsonPropertyName("drinks")]
        public List<Drink> DrinkList { get; set; } = new List<Drink>();
    }

    /// <summary>
    /// Represents a single drink with basic information
    /// </summary>
    internal class Drink
    {
        public required string StrDrink { get; set; }
        public string? StrDrinkThumb { get; set; }
        public required string IdDrink { get; set; }
    }
}