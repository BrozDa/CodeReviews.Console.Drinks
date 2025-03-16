using RestSharp;
using System.Text.Json;
using System.Web;

namespace Drinks
{
    internal class DrinkService : IDrinkService
    {
        public RestClient restClient { get; }

        public DrinkService(RestClient restClient)
        {
            this.restClient = restClient;
        }


        private string ExecuteGetRequest(string url)
        {
            var request = new RestRequest(url);

            try
            {
                var response = restClient.ExecuteAsync(request);

                if (response.Result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new HttpRequestException("HTTP request was not successfull, HTTP status code: " + response.Result.StatusCode);
                }
                if (response.Result.Content == "missing data" || response.Result.Content == "{\"drinks\":null}")
                {
                    throw new InvalidOperationException("Reponse is empty");
                }

                return response.Result.Content!;

            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException($"Operation Failed due to network error:{ex.Message}");
            }
            catch (InvalidOperationException)
            {
                throw new ApplicationException("Reponse is empty; re-check the request url");
            }
        }


        public IEnumerable<Category> GetCategories()
        {

            try
            {
                string response = ExecuteGetRequest($"list.php?c=list");

                var deserializedResponse = DeserializeResponse<CategoryResponse>(response);

                return deserializedResponse.CategoryList ?? new List<Category>();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (JsonException)
            {
                throw;
            }
        }
        public IEnumerable<Drink> GetDrinksInCategory(string category)
        {
            try
            {
                string response = ExecuteGetRequest($"filter.php?c={category}");

                var deserializedResponse = DeserializeResponse<DrinkResponse>(response);

                return deserializedResponse.DrinkList ?? new List<Drink>();
            }
            catch(ApplicationException)
            {
                throw;
            }
            catch(JsonException)
            {
                throw;
            }
        }
        public IEnumerable<DrinkDetail> GetDrinkDetailsbyID(string drinkID)
        {
            try
            {
                string response = ExecuteGetRequest($"lookup.php?i={drinkID}");

                var deserializedResponse = DeserializeResponse<DrinkDetailResponse>(response);

                return deserializedResponse.DrinkDetailList ?? new List<DrinkDetail>();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (JsonException)
            {
                throw;
            }
        }

        public T DeserializeResponse<T>(string JsonData)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.PropertyNameCaseInsensitive = true;

                var deserialized = JsonSerializer.Deserialize<T>(JsonData, options);
                return deserialized!;
            }
            catch(JsonException ex)
            {
                throw new JsonException($"JSON deserialization failed: {ex}");
            }
            
        }
    }
}
