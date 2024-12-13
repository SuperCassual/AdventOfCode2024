using System.Numerics;

namespace AdventOfCode2024;

class Opdracht4_1 : IOpdracht
{
    private Vector2[] directions = new Vector2[] {  new Vector2( 0, 1 ), 
                                                    new Vector2(1,1), 
                                                    new Vector2(1,0), 
                                                    new Vector2(1, -1), 
                                                    new Vector2(0, -1),
                                                    new Vector2(-1, -1),
                                                    new Vector2(-1, 0),
                                                    new Vector2(-1, 1)};

    public void Run()
    {
        int xmasResult = 0;
        int x_MasResult = 0;
        char[,] grid = FillGrid();
        for (int j = 0; j < grid.GetLength(1); j++)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                if(grid[i,j] == 'X')
                {
                    xmasResult += CheckForXmas(grid, new Vector2(i, j));
                    continue;
                }
                if(grid[i,j] == 'A')
                {
                    if(CheckForMasInXShape(grid, new Vector2(i,j)))
                    {
                        x_MasResult++;
                    }
                }
            }
        }
        Console.WriteLine("XMAS number: {0}", xmasResult);
        Console.WriteLine("X-MAS number: {0}", x_MasResult);
    }

    private char[,] FillGrid()
    {
        StreamReader sr = new StreamReader("..\\..\\..\\Resources\\O4-1.txt");
        List<string> rawInput = new List<string>();
        string line = sr.ReadLine();
        while (line != null && line != "")
        {
            rawInput.Add(line);
            line = sr.ReadLine();
        }
        int x = rawInput[0].Length;
        int y = rawInput.Count;

        char[,] gridOutput = new char[x,y];
        for(int j = 0; j < y; j++)
        {
            for (int i = 0; i < x; i++)
            {
                gridOutput[j, i] = rawInput[j][i];
            }
        }
        return gridOutput;
    }

    private int CheckForXmas(char[,] grid, Vector2 coordinates)
    {
        int foundXmasses = 0;
        foreach (Vector2 direction in directions)
        {
            try
            {
                string shouldBeXmas = "";
                for (int i = 0; i < 4; i++)
                {
                    shouldBeXmas += grid[(int)(coordinates.X + (i * direction.X)), (int)(coordinates.Y + (i * direction.Y))];
                }

                if( shouldBeXmas == "XMAS")
                {
                    foundXmasses++;
                }
                    
            }
            catch(IndexOutOfRangeException)
            {
                continue;
            }               
        }
        return foundXmasses;
    }

    private bool CheckForMasInXShape(char[,] grid, Vector2 coordinates)
    {
        string fourCharCode = "";
        try
        {
            fourCharCode += grid[(int)coordinates.X - 1, (int)coordinates.Y + 1];
            fourCharCode += grid[(int)coordinates.X + 1, (int)coordinates.Y - 1];
            fourCharCode += grid[(int)coordinates.X + 1, (int)coordinates.Y + 1];
            fourCharCode += grid[(int)coordinates.X - 1, (int)coordinates.Y - 1];
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }

        for(int i = 0; i < 4; i++)
        {
            if (fourCharCode[i] != 'M' && fourCharCode[i] != 'S')
            {
                return false;
            }
        }
        return fourCharCode[0] != fourCharCode[1] && fourCharCode[2] != fourCharCode[3];
    }
}
