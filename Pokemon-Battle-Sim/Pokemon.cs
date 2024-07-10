using System;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json.Linq;

namespace Pokemon_Battle_Sim
{
	public class Pokemon
	{
		HttpClient _client;
        string pokeURL = $"https://pokeapi.co/api/v2/";

		string _name;
		int _level;
		List<string> _typeList;
		List<int> _statList; //{HP, Attack, Defense, Special Attack, Special Defense, Speed}
		List<Move> _moveList;

		public Pokemon(HttpClient client)
		{
			_client = client;
		}

		public void IChooseYou(string name, int level)
		{
            var response = _client.GetStringAsync(pokeURL + $"pokemon/{name.ToLower()}").Result;

            var data = JObject.Parse(response);

			name = (string) data.GetValue("name");
			_level = level;

			var types = data.SelectToken("types");
			for(int i = 0; i < types.Count(); i++)
			{
				_typeList.Add((string)data.GetValue($"types[{i}].type.name"));
			}

            var stats = data.SelectToken("stats");
			for(int i = 0; i < stats.Count(); i++)
			{
				_statList.Add((int)data.GetValue($"stats[{i}].base_stat"));
			}
        }

		public void ChooseMoves(string m1, string m2, string m3, string m4)
		{
			var firstMove = JObject.Parse(_client.GetStringAsync(pokeURL + $"move/{m1}").Result);
			Move first = new Move();
			first.MoveName = (string)firstMove.SelectToken("name");
            first.MoveType = (string)firstMove.SelectToken("type.name");
            first.MoveDamage = (string)firstMove.SelectToken("damage_class.name");
            first.MovePower = (int)firstMove.SelectToken("power");
            first.MoveAccuracy = (int)firstMove.SelectToken("accuracy");
            first.MoveUses = (int)firstMove.SelectToken("pp");
            first.MovePriority = (int)firstMove.SelectToken("priority");
            _moveList.Add(first);

            var secMove = JObject.Parse(_client.GetStringAsync(pokeURL + $"move/{m2}").Result);
            Move sec= new Move();
            sec.MoveName = (string)secMove.SelectToken("name");
            sec.MoveType = (string)secMove.SelectToken("type.name");
            sec.MoveDamage = (string)secMove.SelectToken("damage_class.name");
            sec.MovePower = (int)secMove.SelectToken("power");
            sec.MoveAccuracy = (int)secMove.SelectToken("accuracy");
            sec.MoveUses = (int)secMove.SelectToken("pp");
            sec.MovePriority = (int)secMove.SelectToken("priority");
            _moveList.Add(sec);

            var thirdMove = JObject.Parse(_client.GetStringAsync(pokeURL + $"move/{m3}").Result);
            Move third = new Move();
            third.MoveName = (string)thirdMove.SelectToken("name");
            third.MoveType = (string)thirdMove.SelectToken("type.name");
            third.MoveDamage = (string)thirdMove.SelectToken("damage_class.name");
            third.MovePower = (int)thirdMove.SelectToken("power");
            third.MoveAccuracy = (int)thirdMove.SelectToken("accuracy");
            third.MoveUses = (int)thirdMove.SelectToken("pp");
            third.MovePriority = (int)thirdMove.SelectToken("priority");
            _moveList.Add(third);

            var fourMove = JObject.Parse(_client.GetStringAsync(pokeURL + $"move/{m4}").Result);
            Move four = new Move();
            four.MoveName = (string)fourMove.SelectToken("name");
            four.MoveType = (string)fourMove.SelectToken("type.name");
            four.MoveDamage = (string)fourMove.SelectToken("damage_class.name");
            four.MovePower = (int)fourMove.SelectToken("power");
            four.MoveAccuracy = (int)fourMove.SelectToken("accuracy");
            four.MoveUses = (int)fourMove.SelectToken("pp");
            four.MovePriority = (int)fourMove.SelectToken("priority");
            _moveList.Add(four);
        }
	}
}

