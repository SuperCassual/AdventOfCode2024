using System.ComponentModel.Design;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2024;

class Opdracht16_1 : IOpdracht
{
    Int2 gridSize;
    char[,] grid;
    Int2 start;
    Int2 endPoint;
    List<ReinNode> openNodes;
    List<ReinNode> closedNodes;

    public void Run()
    {
        grid = FillGrid();
        openNodes = new List<ReinNode> { new ReinNode(null, start, Direction.East, endPoint) };
        closedNodes = new List<ReinNode>();
        PathFinder();
    }

    private void PathFinder()
    {
        List<ReinNode> found = new List<ReinNode>();
        while (found.Count > 0 ? LowestOpenNode().H <= found.First().H : true)
        {
            ReinNode mostestClosest = LowestOpenNode();
            for (int i = 0; i < 4; i++)
            {
                Direction direction = (Direction)i;
                Int2 newPosition = mostestClosest.Position + direction.GetCoordinates();
                if (grid[newPosition.X, newPosition.Y] == '#')
                {
                    continue;
                }
                if (ExistsInClosedList(newPosition))
                {
                    continue;
                }
                ReinNode newnode = new ReinNode(mostestClosest, newPosition, (Direction)i, endPoint);
                
                if(ExistsInOpenList(newnode.Position, out ReinNode outNode))
                {
                    outNode.AddParent(mostestClosest);
                }
                if(newnode.Position == endPoint)
                {
                    found.Add(newnode);
                }
                else
                {
                    openNodes.Add(newnode);
                }
            }
        }
        if(found.Count > 0)
        {
            Console.WriteLine(found.First().F);
        }
        foreach(ReinNode node in found)
        {
            ReverseCourse(node);
        }

        WriteGrid();
        Console.WriteLine(SeatingOptions());

    }

    private int SeatingOptions()
    {
        int result = 0;
        foreach(char c in grid)
        {
            if(c == 'O')
            {
                result++;
            }
        }
        return result;
    }

    private void WriteGrid()
    {
        for (int j = 0; j < gridSize.Y; j++)
        {
            string line = "";
            for (int i = 0; i < gridSize.X; i++)
            {
                line += grid[i, j];
            }
            Console.WriteLine(line);
        }
    }

    private void ReverseCourse(ReinNode endNode)
    {
        grid[endNode.Position.X, endNode.Position.Y] = 'O';
        foreach (ReinNode parent in endNode.parents)
        {
            ReverseCourse(parent);
        }
    }

    private bool ExistsInOpenList(Int2 position, out ReinNode existing)
    {
        foreach (ReinNode node in closedNodes)
        {
            if (node.Position == position)
            {
                existing = node;
                return true;
            }
        }
        existing = null;
        return false;
    }

    private bool ExistsInClosedList(Int2 position)
    {
        foreach (ReinNode node in closedNodes)
        {
            if (node.Position == position)
            {
                return true;
            }
        }
        return false;
    }

    private ReinNode LowestOpenNode()
    {
        ReinNode outNode = openNodes.First();
        foreach(ReinNode node in openNodes)
        {
            if(outNode.H > node.H)
            {
                outNode = node;
            }
        }
        openNodes.Remove(outNode);
        closedNodes.Add(outNode);
        return outNode;
    }

    private char[,] FillGrid()
    {
        StreamReader sr = new StreamReader("..\\..\\..\\Resources\\O16-1.txt");
        List<string> rawInput = new List<string>();
        string line = sr.ReadLine();
        while (line != null && line != "")
        {
            rawInput.Add(line);
            line = sr.ReadLine();
        }
        gridSize = new Int2(rawInput[0].Length, rawInput.Count);

        char[,] gridOutput = new char[gridSize.X, gridSize.Y];
        for (int j = 0; j < gridSize.Y; j++)
        {
            for (int i = 0; i < gridSize.X; i++)
            {
                gridOutput[i, j] = rawInput[j][i];
                if (gridOutput[i, j] == 'S')
                {
                    start = new Int2(i, j);
                }
                else if(gridOutput[i, j] == 'E')
                {
                    endPoint = new Int2(i, j);
                }
            }
        }
        return gridOutput;
    }
}

class ReinNode
{
    Int2 _position;
    Int2 _endPoint;
    Direction _direction;
    public int f;
    public int g;
    public List<ReinNode> parents;

    public ReinNode(ReinNode parent, Int2 position, Direction direction, Int2 endPoint)
    {
        parents = new List<ReinNode>();
        if(parent != null)
        {
            parents.Add(parent);
        }
        _position = position;
        _direction = direction;
        _endPoint = endPoint;
        CalculateF(parent);
        CalculateG();
    }

    private void CalculateF(ReinNode parent)
    {
        if(parent == null)
        {
            f = 0;
            return;
        }
        f = parent.f + 1;
        if(parent.Direction != _direction)
        {
            f += 1000;
        }
    }

    private void CalculateG()
    {
        Int2 difference = _endPoint - _position;
        g = Math.Abs(difference.X) + Math.Abs(difference.Y);
        if(_direction == Direction.West || _direction == Direction.South)
        {
            g += 2000;
        }
        else if(difference.X != 0 && difference.Y != 0)
        {
            g += 1000;
        }
    }

    public void AddParent(ReinNode parent)
    {
        parents.Add(parent);
    }

    public int F {  get { return f; } }
    public int G { get { return g; } }
    public int H { get { return f + g; } }
    public Direction Direction { get { return _direction; } set { _direction = value; } }
    public Int2 Position { get { return _position; } }
}
