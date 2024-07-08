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

			var data = JObject.Parse(response);

			var types = data.SelectToken("types");
			var abil = data.SelectToken("abilities");
			var stats = data.SelectToken("stats");

            Console.WriteLine($"Here is the data for {name}:");

			Console.WriteLine("Type: ");
			for(int t = 0; t < types.Count(); t++)
			{
				Console.WriteLine("\t" + data.SelectToken($"types[{t}].type.name") + " ");
			}

			Console.WriteLine("Abilities: ");
			for(int a = 0; a < abil.Count(); a++)
			{
				if((bool)data.SelectToken($"abilities[{a}].is_hidden") == true)
				{
                    Console.WriteLine($"\t{a + 1}. " + data.SelectToken($"abilities[{a}].ability.name") +
						" (Hidden Ability)");
                }
				else
				{
					Console.WriteLine($"\t{a + 1}. " + data.SelectToken($"abilities[{a}].ability.name"));
                }
			}

			Console.WriteLine("Stats:");
			for(int s = 0; s < stats.Count(); s++)
			{
				Console.WriteLine($"{data.SelectToken($"stats[{s}].stat.name")}: " +
                    data.SelectToken($"stats[{s}].base_stat"));
			}
		}
	}
}

