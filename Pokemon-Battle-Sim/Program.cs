namespace Pokemon_Battle_Sim
{
    public class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();

            BattleSimulator sim = new BattleSimulator(client);

            Console.WriteLine("Choose a Pokemon:");
            string name = Console.ReadLine();

            sim.RequestPokemon(name);
        }
    }
}

