namespace AdventOfCode2024;

class Plant
{
    private List<bool> _fences = new List<bool>();
    private char _plantType;
    private Field _belongsToField;
    private Int2 _position;

    public Plant(char plantType, int x, int y)
    {
        _plantType = plantType;
        _fences = new List<bool> { false, false, false, false };
        _position = new Int2(x,y);
    }

    public void AddFence(Direction direction)
    {
        _fences[(int)direction] = true;
    }

    public void RemoveFence(Direction direction)
    {
        _fences[(int)direction] = false;
    }

    public int FenceScore()
    {
        int result = 0;
        foreach (bool fence in _fences)
        {
            result += Convert.ToInt32(fence);
        }
        return result;
    }

    public bool HasFence(Direction direction)
    {
        return _fences[(int)direction];
    }

    public Field BelongsToField { get { return _belongsToField; } set { _belongsToField = value; } }
    public char PlantType { get { return _plantType; } }
    public Int2 Position { get { return _position; } }
}
