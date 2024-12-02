using AdventOfCode2024.Interfaces;
using AdventOfCode2024.Opdrachten;

namespace AdventOfCode2024
{
    class MainScreen
    {
        static void Main(string[] args)
        {
            IOpdracht opdracht;
            opdracht = new Opdracht2_1();

            if (opdracht != null)
            {
                opdracht.Run();
                return;
            }

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Select opdracht");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1-1":
                        opdracht = new Opdracht1_1();
                        opdracht.Run();
                        break;
                    default:
                        exit = true;
                        break;
                }
            }
        }
    }
}