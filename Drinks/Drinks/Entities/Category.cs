using System.Text.Json.Serialization;

namespace Drinks
{
    internal class CategoryResponse
    {
        [JsonPropertyName("drinks")]
        public List<Category> CategoryList { get; set; }
    }
    internal class Category
    {
        string StrCategory { get; set; }
    }
}
