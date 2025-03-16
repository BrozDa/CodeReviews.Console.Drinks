using System.Text.Json.Serialization;

namespace Drinks
{
    /// <summary>
    /// Represents the response object containing a list of drink categories.
    /// </summary>
    internal class CategoryResponse
    {
        [JsonPropertyName("drinks")]
        public List<Category> CategoryList { get; set; } = new List<Category>();
    }

    /// <summary>
    /// Represents a single drink category.
    /// </summary>
    internal class Category
    {
        public required string StrCategory { get; set; }
    }
}