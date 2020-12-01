using Kneat.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kneat.UI
{
    class Program
    {
        static bool isNumber = false;
        static decimal distance;
        static readonly string API_BASE = "https://localhost:5001/api/StarShips/";

        static string applicationHeader = @".        .               .       .     .            .
   .           .        .                     .        .            .
             .               .    .          .              .   .         .
               _________________ ____         __________
.       .    /                 |    /    \    .  |          \
     .       /    ______ _____| . /      \      |    ___    |     .     .
             \    \    |   |       /   /\   \     |   |___>   |
           .  \    \   |   |      /   /__\   \  . |         _/               .
 .     ________>    |  |   | .   /            \   |   |\    \_______.
      |            /   |   |    /    ______    \  |   | \           |
      |___________/    |___|   /____/      \____\ |___|  \__________|    .
  .     ____ __._____   ____.__________._________
       \    \  /  \  /    /  /    \       |          \    /         |      .
        \    \/    \/    /  /      \      |    ___    |  /    ______|  .
         \              /  /   /\   \ .   |   |___>   |  \    \
   .      \            /  /   /__\   \    |         _/.   \    \            +
           \    /\    /  /            \   |   |\    \______>    |   .
            \  /  \  /  /    ______    \  |   | \              /          .
 .       .   \/    \/  /____/      \____\ |___|  \____________/  LS
                               .                                        .
     .                           .         .               .                 .
                .                                   .            .";
        /// <summary>
        /// Main method of application console
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Welcome to the Star Wars application to calculate resupplies.");
                Console.WriteLine(applicationHeader);
                Console.WriteLine("Initializing...");
                var starShipsCount = await Initialize();

                if (!string.IsNullOrEmpty(starShipsCount))
                {
                    Console.WriteLine($"{starShipsCount} star ships has been found.");
                    Console.WriteLine($"Please, enter the distance to be traveled on MGLT");

                    do
                    {
                        var distanceInput = Console.ReadLine();
                        isNumber = decimal.TryParse(distanceInput, out distance);
                        if (!isNumber || distance < 0) { Console.WriteLine("Please enter a valid number"); distance = -1; }
                    } while (!isNumber || distance < 0);

                    Console.WriteLine($"Running calculations...");

                    var response = await SendRequest($"RunCalculations?distance={distance}");
                    var parsedResponse = JsonConvert.DeserializeObject<IEnumerable<StarShipResponse>>(response);

                    Console.WriteLine("Calculations are done.");
                    PrintResult(parsedResponse);
                }
                else
                {
                    Console.WriteLine("There has been an error, during initializing");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error has ocurred.");
                Console.WriteLine($"Error:{ex.Message}");

            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }
        /// <summary>
        /// It will initialize the application by getting all the starships
        /// </summary>
        /// <returns>Number of starships found</returns>
        private static async Task<string> Initialize()
        {
            string result = "";
            try
            {
                var response = await SendRequest("GetAll");
                result = JsonConvert.DeserializeObject<string>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            return result;
        }
        /// <summary>
        /// Method to send request
        /// </summary>
        /// <param name="uri">path of the API</param>
        /// <returns>Response on string</returns>
        private static async Task<string> SendRequest(string uri)
        {
            string result;
            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(API_BASE+uri);
                    result = await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }
        /// <summary>
        /// Method will print all the response on the console.
        /// </summary>
        /// <param name="starShipResponses">List of starShips</param>
        private static void PrintResult(IEnumerable<StarShipResponse> starShipResponses)
        {
            foreach (var starShip in starShipResponses)
            {
                Console.WriteLine($"{starShip.Name}: {starShip.StopsNeeded}");
                Console.WriteLine("--------------------------");
            }
        }
    }


}
