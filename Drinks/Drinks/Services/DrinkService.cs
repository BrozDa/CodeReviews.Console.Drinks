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
        public List<Drink> GetDrinksInCategory(string category)
        {
            var request = new RestRequest($"filter.php?c={category}");
            var response = restClient.ExecuteAsync(request);

            return DeserializeJSON<DrinkResponse>(response.Result.Content).DrinkList;

        }
        public List<DrinkDetail> GetDrinkDetailsbyID(string drinkID)
        {
            var request = new RestRequest($"lookup.php?i={drinkID}");
            var response = restClient.ExecuteAsync(request);

            return DeserializeJSON<DrinkDetailResponse>(response.Result.Content).DrinkDetailList;
        }

        public T DeserializeJSON<T>(string JsonData)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            return JsonSerializer.Deserialize<T>(JsonData, options);
        }

    }
}
