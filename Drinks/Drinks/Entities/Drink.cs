using System.Text.Json.Serialization;

namespace Drinks
{
    internal class DrinkResponse
    {
        [JsonPropertyName("drinks")]
        public List<Drink> DrinkList { get; set; } = new List<Drink>();
    }
    internal class Drink
    {
        public required string StrDrink {  get; set; }
        public string? StrDrinkThumb { get; set; }
        public required string IdDrink {  get; set; }  
    }
}
