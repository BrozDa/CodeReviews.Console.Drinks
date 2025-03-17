using RestSharp;
using System.Text.Json;

namespace Drinks
{
    /// <summary>
    /// Service for retrieving drink-related data from an external API using REST requests.
    /// Implements <see cref="IDrinkService"/>.
    /// </summary>
    internal class DrinkService : IDrinkService
    {
        public RestClient RestClient { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DrinkService"/> class.
        /// </summary>
        /// <param name="restClient">The <see cref="RestSharp.RestClient"/> instance used for making API calls.</param>
        public DrinkService(RestClient restClient)
        {
            this.RestClient = restClient;
        }

        /// <summary>
        /// Executes a GET request to the specified API endpoint and returns the response content.
        /// </summary>
        /// <param name="url">The API endpoint URL.</param>
        /// <returns>The JSON response content as a string.</returns>
        /// <exception cref="HttpRequestException">Thrown when the HTTP request fails.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the API response is empty.</exception>
        private string ExecuteGetRequest(string url)
        {
            var request = new RestRequest(url);

            try
            {
                var response = RestClient.ExecuteAsync(request);

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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public IEnumerable<Drink> GetDrinksInCategory(string category)
        {
            try
            {
                string response = ExecuteGetRequest($"filter.php?c={category}");

                var deserializedResponse = DeserializeResponse<DrinkResponse>(response);

                return deserializedResponse.DrinkList ?? new List<Drink>();
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public T DeserializeResponse<T>(string JsonData)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.PropertyNameCaseInsensitive = true;

                var deserialized = JsonSerializer.Deserialize<T>(JsonData, options);
                return deserialized!;
            }
            catch (JsonException ex)
            {
                throw new JsonException($"JSON deserialization failed: {ex}");
            }
        }
    }
}