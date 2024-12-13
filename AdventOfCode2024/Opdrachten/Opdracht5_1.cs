namespace AdventOfCode2024;

class Opdracht5_1 : IOpdracht
{
    public virtual void Run()
    {
        int result = 0;
        int correctedResult = 0;
        List<String2> roulx = new List<String2>();
        StreamReader sr = new StreamReader("..\\..\\..\\Resources\\O5-1.txt");
        string line = sr.ReadLine();
        while (line != null && line != "")
        {
            roulx.Add(InitialiseRule(line));
            line = sr.ReadLine();
        }
        line = sr.ReadLine();
        while (line != null && line != "")
        {
            if(IsLineValid(line, roulx))
            {
                string[] splitLine = line.Split(',');
                result += int.Parse(splitLine[splitLine.Length / 2]);
            }
            else
            {
                correctedResult += CorrectMistakeInLine(line, roulx);
            }
            line = sr.ReadLine();
        }
        Console.WriteLine("Safe manuals: {0}", result);
        Console.WriteLine("Corrected manuals: {0}", correctedResult);
    }

    private String2 InitialiseRule(string input)
    {
        string[] numbers = input.Split('|');
        return new String2(numbers[0], numbers[1]);
    }

    private bool IsLineValid(string line, List<String2> rules)
    {
        bool valid = true;
        foreach (String2 rule in rules)
        {
            if (!ValidRule(line, rule))
            {
                valid = false;
                break;
            }
        }
        return valid;
    }

    private bool ValidRule(string line, String2 rule)
    {
        if(line.IndexOf(rule.X) != -1)
        {
            return line.IndexOf(rule.Y) == -1 || line.IndexOf(rule.Y) > line.IndexOf(rule.X);
        }
        return true;
    }

    private Int2 FindIndexOfInstances(string[] input, String2 rule)
    {
        Int2 output = new Int2(-1, -1);
        for (int i = 0, length = input.Length; i < length; i++)
        {
            if (input[i] == rule.X)
            {
                output.X = i;
                continue;
            }
            if (input[i] == rule.Y)
            {
                output.Y = i;
            }
        }
        return output;
    }

    private int CorrectMistakeInLine(string line, List<String2> rules)
    {
        string[] splitLine = line.Split(',');
        bool flawless;
        do
        {
            flawless = true;
            foreach(String2 rule in rules)
            {
                Int2 indexOutput = FindIndexOfInstances(splitLine, rule);
                if(MistakeDetected(indexOutput))
                {
                    string holder;
                    holder = splitLine[indexOutput.X];
                    splitLine[indexOutput.X] = splitLine[indexOutput.Y];
                    splitLine[indexOutput.Y] = holder;
                    flawless = false;
                }
            }
        }
        while (!flawless);
        return int.Parse(splitLine[splitLine.Length / 2]);
    }

    private bool MistakeDetected(Int2 input)
    {
        if(input.X == -1 || input.Y == -1)
        {
            return false;
        }
        return input.X > input.Y;
    }
}
