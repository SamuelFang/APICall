using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using WebAPICLient;
using System.Xml.Linq;

namespace WebAPICLient
{
    class Joke
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("setup")]
        public string Setup { get; set; }
        [JsonProperty("punchline")]
        public string Punchline { get; set; }
        [JsonProperty("id")]
        public int ID { get; set; }
    }
}

class Program
{
    private static readonly HttpClient client = new HttpClient();
    private static async Task ProcessRepositories()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Press enter to hear a joke, or enter \"exit\" to exit");
                var input = Console.ReadLine();
                if (input == "exit")
                {
                    Console.WriteLine("Goodbye! Hope you enjoyed the jokes!");
                    break;
                }
                var result = await client.GetAsync("https://official-joke-api.appspot.com/random_joke");
                var resultRead = await result.Content.ReadAsStringAsync();

                var joke = JsonConvert.DeserializeObject<Joke>(resultRead);

                Console.WriteLine("Joke #" + joke.ID + " | Type: " + joke.Type);
                Console.WriteLine(joke.Setup);
                input = Console.ReadLine();
                Console.WriteLine(joke.Punchline);
                Console.WriteLine("-------------------------------------------------------------");
            }
            catch (Exception)
            {
                Console.WriteLine("Error, joke not found");
            }
        }
    }
    static async Task Main(string[] args)
    {
        await ProcessRepositories();
    }
}