using RestSharp;

namespace Drinks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RestClient restClient = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");

            DrinkService svc = new DrinkService(restClient);
            UserInteraction UI = new UserInteraction(svc);

            UI.RunService();

            
        }
    }
}
