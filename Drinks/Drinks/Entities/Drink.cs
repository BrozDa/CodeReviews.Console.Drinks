using System.Text.Json.Serialization;

namespace Drinks
{
    internal class DrinkResponse
    {
        [JsonPropertyName("drinks")]
        public List<Drink> DrinkList { get; set; }
    }
    internal class Drink
    {
        public string StrDrink {  get; set; }
        public string StrDrinkThumb { get; set; }
        public string IdDrink {  get; set; }  
    }
}
