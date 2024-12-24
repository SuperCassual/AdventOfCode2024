namespace AdventOfCode2024;

class Opdracht13_1 : IOpdracht
{
    public void Run()
    {
        StreamReader sr = new StreamReader("..\\..\\..\\Resources\\O13-1.txt");
        List<ClawMachine> machines = new List<ClawMachine>();
        string line = sr.ReadLine();
        while (line != null && line != "")
        {
            Long2 AMove = Parsethings(line);
            Long2 BMovie = Parsethings(sr.ReadLine());
            Long2 PrizeLocation = Parsethings(sr.ReadLine(), '=');
            machines.Add(new ClawMachine(AMove, BMovie, PrizeLocation + new Long2(10000000000000, 10000000000000)));
            line = sr.ReadLine();
            line = sr.ReadLine();
        }

        long totalTokens = 0;
        foreach (ClawMachine machine in machines)
        {
            if(machine.TryGetPrize(out long tokens))
            {
                totalTokens += tokens;
            }
        }

        Console.WriteLine(totalTokens);
    }

    private Long2 Parsethings(string line, char c = '+')
    {
        string[] coords = line.Split(c);
        for (int i = 0; i < coords[1].Length; i++)
        {
            if (coords[1][i] == ',')
            {
                return new Long2(long.Parse(coords[1].Substring(0, i)), long.Parse(coords[2]));
            }
        }
        return new Long2();
    }
}
