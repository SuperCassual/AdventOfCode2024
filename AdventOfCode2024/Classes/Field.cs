namespace AdventOfCode2024;

class Field
{
    private List<Plant> _plants;
    private int _bulkFenceScore;
    public Field()
    {
        _plants = new List<Plant>();
        _bulkFenceScore = 0;
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

    public int CalculateBulkFenceScore()
    {
        int sides = 0;
        List<Plant> bulkPlants = new List<Plant>(_plants);
        
        while(bulkPlants.Count > 0)
        {
            Plant plant = bulkPlants.First();
            int i = 0;
            List<Plant> usedPlants = new List<Plant>();
            usedPlants.Add(plant);
            while (i < 4)
            {
                if(plant.HasFence((Direction)i))
                {
                    sides += WalkFencePerimiter(plant, (Direction)i, bulkPlants, out usedPlants);
                    break;
                }
                i++;
            }
            bulkPlants = bulkPlants.Except(usedPlants).ToList();
        }
        
        return sides * _plants.Count;
    }

    private int WalkFencePerimiter(Plant first, Direction startDirection, List<Plant> referenceList, out List<Plant> usedPlants)
    {
        usedPlants = new List<Plant>();
        int sides = 0;
        Direction currentDirection = startDirection;
        Plant currentPlant = first;
        do
        {
            usedPlants.Add(currentPlant);
            if(currentPlant.HasFence(currentDirection.GetNextDirection()))
            {
                sides++;
                currentDirection = currentDirection.GetNextDirection();
                continue;
            }

            var findPlant = _plants.Where(Plant => currentPlant.Position + currentDirection.GetNextDirection().GetCoordinates() == Plant.Position);
            if(findPlant.Count() == 1)
            {
                Plant foundPlant = findPlant.First();
                if(foundPlant.HasFence(currentDirection))
                {
                    currentPlant = foundPlant;
                    continue;
                }
            }
            findPlant = _plants.Where(Plant => currentPlant.Position + currentDirection.GetNextDirection().GetCoordinates() + currentDirection.GetCoordinates() == Plant.Position);
            if(findPlant.Count() == 1)
            {
                Plant foundPlant = findPlant.First();
                if(foundPlant.HasFence(currentDirection.GetNextDirection(3)))
                {
                    sides++;
                    currentDirection = currentDirection.GetNextDirection(3);
                    currentPlant = foundPlant;
                    continue;
                }
            }

            throw new Exception("Plant is pissing on the moon");
        }
        while (currentDirection != startDirection || currentPlant != first);
        return sides;
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
        donorField.BulkFenceScore += _bulkFenceScore;
        _bulkFenceScore = 0;
    }
    
    public int BulkFenceScore { get { return _bulkFenceScore; } set { _bulkFenceScore = value; } }
}
