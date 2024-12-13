namespace AdventOfCode2024;

class Guard
{
    protected Direction _facingDirection;
    protected string _name;
    protected Int2 _position;

    public Guard(Int2 startPosition, Direction startDirection = Direction.North, string name = "Cynthia")
    {
        _name = name;
        _facingDirection = startDirection;
        _position = startPosition;
    }

    public Int2 Move(char[,] labGrid)
    {
        while (PathObstructed(labGrid))
        {
            Turn();
        }
        labGrid[_position.X, _position.Y] = 'X';
        Int2 newPosition = GetFacingPos();
        _position = newPosition;
        return newPosition;
    }

    protected virtual bool PathObstructed(char[,] grid)
    {
        try
        {
            return GetFacingTile(grid) == '#' || GetFacingTile(grid) == 'O';
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
    }

    protected virtual void Turn()
    {
        int directionAsInt = (int)_facingDirection;
        directionAsInt = (directionAsInt + 1) % 4;
        _facingDirection = (Direction)directionAsInt;
    }

    protected char GetFacingTile(char[,] grid)
    {
        return grid[_position.X + _facingDirection.GetCoordinates().X, _position.Y + _facingDirection.GetCoordinates().Y];
    }

    protected Int2 GetFacingPos()
    {
        return new Int2(_position.X + _facingDirection.GetCoordinates().X, _position.Y + _facingDirection.GetCoordinates().Y);
    }

    public Direction GetFacingDirection()
    {
        return _facingDirection;
    }

    public Int2 CurrentPosition { get { return _position; } }
}
