using AdventOfCode2024.Interfaces;
using AdventOfCode2024.Opdrachten;

namespace AdventOfCode2024
{
    class MainScreen
    {
        static void Main(string[] args)
        {
            IOpdracht opdracht;
            opdracht = new Opdracht10_1();

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
                    case "2-1":
                        opdracht = new Opdracht2_1();
                        opdracht.Run();
                        break;
                    case "3-1":
                        opdracht = new Opdracht3_1();
                        opdracht.Run();
                        break;
                    case "4-1":
                        opdracht = new Opdracht4_1();
                        opdracht.Run();
                        break;
                    case "5-1":
                        opdracht = new Opdracht5_1();
                        opdracht.Run();
                        break;
                    case "6-1":
                        opdracht = new Opdracht6_1();
                        opdracht.Run();
                        break;
                    case "7-1":
                        opdracht = new Opdracht7_1();
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