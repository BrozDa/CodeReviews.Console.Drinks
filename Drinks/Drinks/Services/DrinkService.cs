using RestSharp;
using System.Text.Json;
using System.Web;

namespace Drinks
{
    internal class DrinkService
    {
        public RestClient restClient { get; }

        public DrinkService(RestClient restClient)
        {
            this.restClient = restClient;
        }

        public List<Category> GetCategories()
        {
            var request = new RestRequest("list.php?c=list");
            var response = restClient.ExecuteAsync(request);

            return DeserializeJSON<CategoryResponse>(response.Result.Content).CategoryList;
        }

        public T DeserializeJSON<T>(string JsonData)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            return JsonSerializer.Deserialize<T>(JsonData, options);
        }

    }
}
