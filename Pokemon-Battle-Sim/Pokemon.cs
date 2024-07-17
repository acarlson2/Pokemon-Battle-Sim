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

		string _name;
        string _nature;
		int _level;
        double _weight;
        double _height;
		List<string> _typeList;
		List<double> _statList; //{HP, Attack, Defense, Special Attack, Special Defense, Speed}
		List<Move> _moveList;

		public Pokemon(HttpClient client)
		{
			_client = client;
		}

		public void IChooseYou(string name, int level, string nature)
		{
            var response = _client.GetStringAsync(pokeURL + $"pokemon/{name.ToLower()}").Result;

            var data = JObject.Parse(response);

			_name = name;
			_level = level;
            _weight = (double)data.GetValue("weight") / 10; //weight in kg
            _height = (double)data.GetValue("height") / 10; //height in m
            _nature = nature;

			var types = data.SelectToken("types");
			for(int i = 0; i < types.Count(); i++)
			{
				_typeList.Add((string)data.GetValue($"types[{i}].type.name"));
			}

            var stats = data.SelectToken("stats");
			for(int i = 0; i < stats.Count(); i++)
			{
				_statList.Add((double)data.GetValue($"stats[{i}].base_stat"));
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

        public void StatCalculation(int[] iv, int[] ev)
        {
            var natureResponse = _client.GetStringAsync(pokeURL + $"natures/{_nature.ToLower()}").Result;
            var natureData = JObject.Parse(natureResponse);

            _statList[0] = (((2 * _statList[0] + iv[0] + (ev[0] / 4)) * _level) / 100) + _level + 10;

            for(int i = 1; i < _statList.Count; i++)
            {
                _statList[i] = ((2 * _statList[i] + iv[i] + (ev[i] / 4) * _level) / 100) + 5;
            }

            switch((string)natureData.SelectToken("increased_stat.name"))
            {
                case "attack":
                    _statList[1] *= 1.1;
                    break;
                case "defense":
                    _statList[2] *= 1.1;
                    break;
                case "special-attack":
                    _statList[3] *= 1.1;
                    break;
                case "special-defense":
                    _statList[4] *= 1.1;
                    break;
                case "speed":
                    _statList[5] *= 1.1;
                    break;
            }

            switch ((string)natureData.SelectToken("decreased_stat.name"))
            {
                case "attack":
                    _statList[1] *= 0.9;
                    break;
                case "defense":
                    _statList[2] *= 0.9;
                    break;
                case "special-attack":
                    _statList[3] *= 0.9;
                    break;
                case "special-defense":
                    _statList[4] *= 0.9;
                    break;
                case "speed":
                    _statList[5] *= 0.9;
                    break;
            }
        }
	}
}

