using System;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json.Linq;

namespace Pokemon_Battle_Sim
{
	public class BattleSimulator
	{
		private HttpClient _client;

		public BattleSimulator(HttpClient client)
		{
			_client = client;
		}

		public void DisplayPokemonData(string name)
		{
			var pokeURL = $"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}";

			var response = _client.GetStringAsync(pokeURL).Result;

			var types = JObject.Parse(response).SelectToken("types");
			var abil = JObject.Parse(response).SelectToken("abilities");

            Console.WriteLine($"Here is the data for {name}:");

			Console.WriteLine("Type: ");
			for(int t = 0; t < types.Count(); t++)
			{
				Console.WriteLine("\t" + JObject.Parse(response).SelectToken($"types[{t}].type.name") + " ");
			}

			Console.WriteLine("Abilities: ");
			for(int a = 0; a < abil.Count(); a++)
			{
				if((bool)JObject.Parse(response).SelectToken($"abilities[{a}].is_hidden") == true)
				{
                    Console.WriteLine($"\t{a + 1}. " + JObject.Parse(response).SelectToken($"abilities[{a}].ability.name") +
						" (Hidden Ability)");
                }
				else
				{
					Console.WriteLine($"\t{a + 1}. " + JObject.Parse(response).SelectToken($"abilities[{a}].ability.name"));
                }
			}

			Console.WriteLine("Stats:");
			for(int s = 0; s < 6; s++)
			{
				Console.WriteLine($"{JObject.Parse(response).SelectToken($"stats[{s}].stat.name")}: " +
                    JObject.Parse(response).SelectToken($"stats[{s}].base_stat"));
			}
		}
	}
}

