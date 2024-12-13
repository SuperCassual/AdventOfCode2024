namespace AdventOfCode2024;

class Field
{
    private List<Plant> _plants;
    public Field()
    {
        _plants = new List<Plant>();
    }

    public int CalculateFenceScore()
    {
        int perimeter = 0;
        foreach (Plant plant in _plants)
        {
            perimeter += plant.FenceScore();
        }
        return perimeter * _plants.Count;
    }

    public void AddPlant(Plant plant)
    {
        _plants.Add(plant);
        plant.BelongsToField = this;
    }

    public void MergeFields(Field donorField)
    {
        if(this == donorField)
        {
            return;
        }
        while(_plants.Count > 0)
        {
            donorField.AddPlant(_plants[0]);
            _plants.RemoveAt(0);
        }
    }
}
