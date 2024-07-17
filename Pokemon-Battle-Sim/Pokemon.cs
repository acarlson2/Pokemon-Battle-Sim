using System;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace Pokemon_Battle_Sim
{
	public class Pokemon
	{
		HttpClient _client;
        string pokeURL = $"https://pokeapi.co/api/v2/";

		public string Name { get; set; } //Pokemon's name

        public string Nature { get; set; } //A nature can effect certain stats (i.e. Modest: increase Special Attack, decrease Attack)

		public int Level { get; set; } //The level a Pokemon is at. Levels are between 1 and 100

        public double Weight { get; set; } //A Pokemon's weight can sometimes affect the power of certain moves (i.e. Grass Knot)

        public double Height { get; set; } //The Pokemon's height (for fun I guess?)

        public List<string> TypeList { get; set; } = new List<string>(); //A Pokemon can have up to 2 unique types

        public List<double> StatList { get; set; } = new List<double>(); //{HP, Attack, Defense, Special Attack, Special Defense, Speed}

        public List<Move> MoveList { get; set; } = new List<Move>(); //Each Pokemon can have up to 4 unique moves

        //Constructor
		public Pokemon(HttpClient client)
		{
			_client = client;
		}

        //Gets the basic data for a Pokemon by passing in the name of the Pokemon
		public void IChooseYou(string name, int level, string nature)
		{
            var response = _client.GetStringAsync(pokeURL + $"pokemon/{name.ToLower()}").Result;

            var data = JObject.Parse(response);

			Name = name;
			Level = level;
            Weight = (double)data.GetValue("weight") / 10; //weight in kg
            Height = (double)data.GetValue("height") / 10; //height in m
            Nature = nature;

			var types = data.SelectToken("types");
			for(int i = 0; i < types.Count(); i++)
			{
                if (types[i].SelectToken("type") == null)
                {
                    Console.WriteLine("Type is null");
                 
                }
                else
                {
                    string type = types[i].SelectToken("type").SelectToken("name").ToString();
                    TypeList.Add(type);
                }
			}

            var stats = data.SelectToken("stats");
			for(int i = 0; i < stats.Count(); i++)
			{
                if (stats[i].SelectToken("base_stat") == null)
                {
                    Console.WriteLine("Stat is null");
                }
                else
                {
                    int stat = (int)stats[i].SelectToken("base_stat");
                    StatList.Add(stat);
                }
				
			}
        }

        //Creates 4 Move objects using API data and parameters
		public void ChooseMoves(string m1, string m2, string m3, string m4)
		{
			var firstMove = JObject.Parse(_client.GetStringAsync(pokeURL + $"move/{m1}").Result);
            var firstEffects = firstMove.SelectToken("effect_entries");
			Move first = new Move();
			first.MoveName = (string)firstMove.SelectToken("name");
            first.MoveType = (string)firstMove.SelectToken("type.name");
            first.MoveDamage = (string)firstMove.SelectToken("damage_class.name");
            first.MoveDesc = (string)firstEffects[0].SelectToken("short_effect");
            first.MovePower = (int)firstMove.SelectToken("power");
            first.MoveAccuracy = (int)firstMove.SelectToken("accuracy");
            first.MoveUses = (int)firstMove.SelectToken("pp");
            first.MovePriority = (int)firstMove.SelectToken("priority");
            MoveList.Add(first);

            var secMove = JObject.Parse(_client.GetStringAsync(pokeURL + $"move/{m2}").Result);
            var secEffects = secMove.SelectToken("effect_entries");
            Move sec= new Move();
            sec.MoveName = (string)secMove.SelectToken("name");
            sec.MoveType = (string)secMove.SelectToken("type.name");
            sec.MoveDamage = (string)secMove.SelectToken("damage_class.name");
            sec.MoveDesc = (string)secEffects[0].SelectToken("short_effect");
            sec.MovePower = (int)secMove.SelectToken("power");
            sec.MoveAccuracy = (int)secMove.SelectToken("accuracy");
            sec.MoveUses = (int)secMove.SelectToken("pp");
            sec.MovePriority = (int)secMove.SelectToken("priority");
            MoveList.Add(sec);

            var thirdMove = JObject.Parse(_client.GetStringAsync(pokeURL + $"move/{m3}").Result);
            var thirdEffects = thirdMove.SelectToken("effect_entries");
            Move third = new Move();
            third.MoveName = (string)thirdMove.SelectToken("name");
            third.MoveType = (string)thirdMove.SelectToken("type.name");
            third.MoveDamage = (string)thirdMove.SelectToken("damage_class.name");
            third.MoveDesc = (string)thirdEffects[0].SelectToken("short_effect");
            third.MovePower = (int)thirdMove.SelectToken("power");
            third.MoveAccuracy = (int)thirdMove.SelectToken("accuracy");
            third.MoveUses = (int)thirdMove.SelectToken("pp");
            third.MovePriority = (int)thirdMove.SelectToken("priority");
            MoveList.Add(third);

            var fourMove = JObject.Parse(_client.GetStringAsync(pokeURL + $"move/{m4}").Result);
            var fourEffects = fourMove.SelectToken("effect_entries");
            Move four = new Move();
            four.MoveName = (string)fourMove.SelectToken("name");
            four.MoveType = (string)fourMove.SelectToken("type.name");
            four.MoveDamage = (string)fourMove.SelectToken("damage_class.name");
            four.MoveDesc = (string)fourEffects[0].SelectToken("short_effect");
            four.MovePower = (int)fourMove.SelectToken("power");
            four.MoveAccuracy = (int)fourMove.SelectToken("accuracy");
            four.MoveUses = (int)fourMove.SelectToken("pp");
            four.MovePriority = (int)fourMove.SelectToken("priority");
            MoveList.Add(four);
        }

        //Calculates the stats of a Pokemon based on level, iv's, and ev's (you don't want to know the math, trust me)
        public void StatCalculation(int[] iv, int[] ev)
        {
            var natureResponse = _client.GetStringAsync(pokeURL + $"nature/{Nature.ToLower()}").Result;
            var natureData = JObject.Parse(natureResponse);

            StatList[0] = (((2 * StatList[0] + iv[0] + (ev[0] / 4)) * Level) / 100) + Level + 10;

            for(int i = 1; i < StatList.Count; i++)
            {
                double stat = ((((2 * StatList[i] + iv[i] + (ev[i] / 4)) * Level) / 100) + 5);
                StatList[i] = stat;
            }

            switch((string)natureData.SelectToken("increased_stat.name"))
            {
                case "attack":
                    StatList[1] = Math.Floor(StatList[1] * 1.1);
                    break;
                case "defense":
                    StatList[2] = Math.Floor(StatList[2] * 1.1);
                    break;
                case "special-attack":
                    StatList[3] = Math.Floor(StatList[3] * 1.1);
                    break;
                case "special-defense":
                    StatList[4] = Math.Floor(StatList[4] * 1.1);
                    break;
                case "speed":
                    StatList[5] = Math.Floor(StatList[5] * 1.1);
                    break;
            }

            switch ((string)natureData.SelectToken("decreased_stat.name"))
            {
                case "attack":
                    StatList[1] = Math.Floor(StatList[1] * 0.9);
                    break;
                case "defense":
                    StatList[2] = Math.Floor(StatList[2] * 0.9);
                    break;
                case "special-attack":
                    StatList[3] = Math.Floor(StatList[3] * 0.9);
                    break;
                case "special-defense":
                    StatList[4] = Math.Floor(StatList[4] * 0.9);
                    break;
                case "speed":
                    StatList[5] = Math.Floor(StatList[5] * 0.9);
                    break;
            }
        }
	}
}

