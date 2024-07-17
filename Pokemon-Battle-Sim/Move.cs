using System;
namespace Pokemon_Battle_Sim
{
	public class Move
	{
		public string MoveName { get; set; } //The move's name

		public string MoveType { get; set; } //The move's type (if a move's type matches the type of the user, it gets a 1.5x boost!)

		public string MoveDamage { get; set; } //The moves damage category (Physical, Special, or Status)

		public string MoveDesc { get; set; } //Each move can have their own effects. Those effects are shown in the move's description

		public int MovePower { get; set; } //A moves base power (if it's a status move, this will be null

		public int MoveAccuracy { get; set; } //A move's accuracy (if a move always hits, this will be null

		public int MoveUses { get; set; } //The number of uses a move has

		public int MovePriority { get; set; } //Some moves, like Quick Attack, have priority. The highest priority goes first, regardless of speed

		public Move()
		{
			
		}
	}
}

