namespace AdventOfCode2024;

class Opdracht10_1 : IOpdracht
{
    int[,] grid;
    Int2 gridsize;
    Int2 result = new Int2(0,0);

    public void Run()
    {
        grid = FillGrid();
        for (int j = 0; j < gridsize.Y; j++)
        {
            for (int i = 0; i < gridsize.X; i++)
            {
                if(grid[i, j] == 0)
                {
                    result += FindTrailheads(i, j);
                }
            }
        }
        Console.WriteLine($"Trailhead scores: {result.X}");
        Console.WriteLine($"Trailhead ratings: {result.Y}");
    }

    private int[,] FillGrid()
    {
        StreamReader sr = new StreamReader("..\\..\\..\\Resources\\O10-1.txt");
        List<string> rawInput = new List<string>();
        string line = sr.ReadLine();
        while (line != null && line != "")
        {
            rawInput.Add(line);
            line = sr.ReadLine();
        }
        int x = rawInput[0].Length;
        int y = rawInput.Count;
        gridsize = new Int2(x, y);

        int[,] gridOutput = new int[x, y];
        for (int j = 0; j < y; j++)
        {
            for (int i = 0; i < x; i++)
            {
                gridOutput[i, j] = int.Parse(rawInput[j][i].ToString());
            }
        }
        return gridOutput;
    }

    private Int2 FindTrailheads(int x, int y)
    {
        List<Int2> nines = new List<Int2>();
        Trailblazing(nines, 0, x, y);
        Int2 results = new Int2(0,0);
        results.X = nines.Distinct().ToList().Count; //use this for part 1
        results.Y = nines.Count; //use this for part 2
        return results;
    }

    private void Trailblazing(List<Int2> nines, int height, int x, int y)
    {
        for(int dir = 0; dir < 4; dir++)
        {
            Direction direction = (Direction)dir;
            if(!IsOutOfBounds(x, y, direction))
            {
                Int2 nextPos = new Int2(x + direction.GetCoordinates().X, y + direction.GetCoordinates().Y);
                if (grid[nextPos.X, nextPos.Y] == height + 1)
                {
                    if(height + 1 == 9)
                    {
                        nines.Add(nextPos);
                    }
                    else
                    {
                        Trailblazing(nines, height + 1, nextPos.X, nextPos.Y);
                    }
                }
            }
        }
    }

    private bool IsOutOfBounds(int x, int y, Direction direction)
    {
        if(x + direction.GetCoordinates().X < 0 || y+direction.GetCoordinates().Y < 0)
        {
            return true;
        }
        if(x + direction.GetCoordinates().X >= gridsize.X || y + direction.GetCoordinates().Y >= gridsize.Y)
        {
            return true;
        }
        return false;
    }
}
