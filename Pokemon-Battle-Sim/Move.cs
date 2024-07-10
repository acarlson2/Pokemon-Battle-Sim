using System;
namespace Pokemon_Battle_Sim
{
	public class Move
	{
		public string MoveName { get; set; }
		public string MoveType { get; set; }
		public string MoveDamage { get; set; }
		public int MovePower { get; set; }
		public int MoveAccuracy { get; set; }
		public int MoveUses { get; set; }
		public int MovePriority { get; set; }
		public Move()
		{
			
		}
	}
}

