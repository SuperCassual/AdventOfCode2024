namespace AdventOfCode2024;

class Plant
{
    private List<bool> _fences = new List<bool>();
    private char _plantType;
    private Field _belongsToField;

    public Plant(char plantType)
    {
        _plantType = plantType;
        _fences = new List<bool> { false, false, false, false };
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

    public Field BelongsToField { get { return _belongsToField; } set { _belongsToField = value; } }
    public char PlantType { get { return _plantType; } }
}
