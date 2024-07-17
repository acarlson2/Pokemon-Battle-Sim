namespace Pokemon_Battle_Sim
{
    public class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();

            Pokemon mon = new Pokemon(client);

            mon.IChooseYou("swampert", 100, "adamant");

            mon.ChooseMoves("liquidation", "earthquake", "avalanche", "flip-turn");

            int[] iv = {31, 31, 31, 31, 31, 31};
            int[] ev = {252, 252, 0, 0, 6, 0};

            mon.StatCalculation(iv, ev);

            Console.WriteLine(mon.Name + "\n");
            
            Console.WriteLine(mon.Level + "\n");
            

            foreach(string type in mon.TypeList)
            {
                Console.WriteLine(type);
            }
            Console.WriteLine("");

            Console.WriteLine($"{mon.Weight} kg \n");
            Console.WriteLine($"{mon.Height} m \n");
            
            Console.WriteLine(mon.Nature + "\n");

            string[] statOrder = { "HP", "Attack", "Defense", "Special Attack", "Special Defense", "Speed" };
            for(int i = 0; i < statOrder.Length; i++)
            {
                Console.WriteLine($"{statOrder[i]}: {mon.StatList[i]}");
            }
            Console.WriteLine("");

            foreach(Move move in mon.MoveList)
            {
                Console.WriteLine(move.MoveName);
                Console.WriteLine(move.MoveDesc);
                Console.WriteLine($"\tType: {move.MoveType}");
                Console.WriteLine($"\tCategory: {move.MoveDamage}");
                Console.WriteLine($"\tPower: {move.MovePower}");
                Console.WriteLine($"\tAccuracy: {move.MoveAccuracy}");
                Console.WriteLine($"\tPP: {move.MoveUses}");
                if(move.MovePriority != 0)
                {
                    Console.WriteLine($"\tPriority: {move.MovePriority}");
                }
                Console.WriteLine("");
            }
        }
    }
}

