using AdventOfCode2024.Classes;
using AdventOfCode2024.Interfaces;

namespace AdventOfCode2024.Opdrachten
{
    class Opdracht6_1 : IOpdracht
    {
        public void Run()
        {
            int ghostsStuckInLimbo = 0;
            Int2 startpos, currentpos;
            char[,] labgrid = FillGrid(out startpos);
            Guard guard = new Guard(startpos);
            List<GhostGuard> ghosts = new List<GhostGuard>();

            currentpos = startpos;
            do
            {
                if (!IsOutOfBounds(labgrid, currentpos))
                {
                    GhostGuard ghost = new GhostGuard(labgrid, currentpos, guard.GetFacingDirection());
                    ghosts.Add(ghost);
                    currentpos = guard.Move(labgrid);
                }
                else
                {
                    Console.WriteLine($"Cynthia has finished walking, there are {ghosts.Count} ghosts remaining");
                }
                for (int i = ghosts.Count - 1; i >= 0; i--)
                {
                    GhostGuard spookyGhost = ghosts[i];
                    spookyGhost.Step();
                    if(spookyGhost.IsInALoop)
                    {
                        ghostsStuckInLimbo++;
                        ghosts.RemoveAt(i);
                        Console.WriteLine($"Ghost was trapped; obstacle at {spookyGhost.StartObstacle}");
                        continue;
                    }
                    if(spookyGhost.HasLeftTheBuilding)
                    {
                        ghosts.RemoveAt(i);
                        Console.WriteLine("Ghost breached containment");
                    }
                }
            }
            while (!IsOutOfBounds(labgrid, currentpos) || ghosts.Count > 0);
            int result = 0;
            foreach (char c in labgrid)
            {
                if (c == 'X')
                {
                    result++;
                }
            }
            Console.WriteLine($"Visited squares: {result}");
            Console.WriteLine($"Ghosts doomed to wander forever: {ghostsStuckInLimbo}");
        }
        private char[,] FillGrid(out Int2 guardStartPosition)
        {
            guardStartPosition = new Int2(-1, -1);
            StreamReader sr = new StreamReader("..\\..\\..\\Resources\\O6-1.txt");
            List<string> rawInput = new List<string>();
            string line = sr.ReadLine();
            while (line != null && line != "")
            {
                rawInput.Add(line);
                line = sr.ReadLine();
            }
            int x = rawInput[0].Length;
            int y = rawInput.Count;

            char[,] gridOutput = new char[x, y];
            for (int j = 0; j < y; j++)
            {
                for (int i = 0; i < x; i++)
                {
                    gridOutput[i, j] = rawInput[j][i];
                    if (rawInput[j][i] == '^')
                    {
                        guardStartPosition = new Int2(i, j);
                    }
                }
            }
            return gridOutput;
        }

        public bool IsOutOfBounds(char[,] grid, Int2 pos)
        {
            try
            {
                _ = grid[pos.X, pos.Y];
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }
    }
}
