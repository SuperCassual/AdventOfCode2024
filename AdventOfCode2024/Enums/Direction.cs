namespace AdventOfCode2024;

enum Direction
{
    North = 0,
    East = 1,
    South = 2,
    West = 3
}

static class DirectionExtensions
{
    public static Int2 GetCoordinates(this Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                return new Int2(0, -1);
            case Direction.East:
                return new Int2(1, 0);
            case Direction.South:
                return new Int2(0, 1);
            case Direction.West:
                return new Int2(-1, 0);
            default:
                Console.WriteLine("Entity is pissing on the moon");
                return new Int2(0, 0);
        }
    }
}
