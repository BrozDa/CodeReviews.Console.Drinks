using System.Text.Json.Serialization;

namespace Drinks
{
    internal class CategoryResponse
    {
        [JsonPropertyName("drinks")]
        public List<Category> CategoryList { get; set; } = new List<Category>();
    }
    internal class Category
    {
        public required string StrCategory { get; set; }
    }
}
