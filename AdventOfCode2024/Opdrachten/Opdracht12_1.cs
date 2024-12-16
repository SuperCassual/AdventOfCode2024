namespace AdventOfCode2024;

class Opdracht12_1 : IOpdracht
{
    List<Field> fields;
    public void Run()
    {
        int results = 0;
        int bulkresults = 0;

        fields = new List<Field>();
        StreamReader sr = new StreamReader("..\\..\\..\\Resources\\O12-1.txt");
        string line = sr.ReadLine();
        Plant[] previousLine = new Plant[line.Length];

        for (int y = 0; line != null && line != ""; y++)
        {
            for (int x = 0; x < previousLine.Length; x++)
            {
                Plant newPlant = new Plant(line[x], x, y);

                //check left
                if (x < 1)
                {
                    newPlant.AddFence(Direction.West);
                }
                else
                {
                    CheckLeft(newPlant, previousLine[x - 1]);
                }

                //check up
                CheckUp(newPlant, previousLine[x]);

                //check field
                AssignFieldIfUnassigned(newPlant);

                previousLine[x] = newPlant;

            }
            previousLine[previousLine.Length - 1].AddFence(Direction.East);
            line = sr.ReadLine();
        }
        foreach (Plant plant in previousLine)
        {
            plant.AddFence(Direction.South);
        }

        foreach(Field field in fields)
        {
            results += field.CalculateFenceScore();
            bulkresults += field.CalculateBulkFenceScore();
        }

        Console.WriteLine(results);
        Console.WriteLine(bulkresults);
    }

    private void AssignFieldIfUnassigned(Plant newPlant)
    {
        if (newPlant.BelongsToField == null)
        {
            Field field = new Field();
            field.AddPlant(newPlant);
            fields.Add(field);
        }
    }

    private void CheckLeft(Plant newPlant, Plant prevPlant)
    {
        if (prevPlant.PlantType == newPlant.PlantType)
        {
            prevPlant.BelongsToField.AddPlant(newPlant);
        }
        else
        {
            newPlant.AddFence(Direction.West);
            prevPlant.AddFence(Direction.East);
        }
    }

    private void CheckUp(Plant newPlant, Plant prevPlant)
    {
        if (prevPlant == null)
        {
            newPlant.AddFence(Direction.North);
            return;
        }

        if (prevPlant.PlantType == newPlant.PlantType)
        {
            if (newPlant.BelongsToField == null)
            {
                prevPlant.BelongsToField.AddPlant(newPlant);
            }
            else
            {
                prevPlant.BelongsToField.MergeFields(newPlant.BelongsToField);
            }
        }
        else
        {
            newPlant.AddFence(Direction.North);
            prevPlant.AddFence(Direction.South);
        }
    }
}
