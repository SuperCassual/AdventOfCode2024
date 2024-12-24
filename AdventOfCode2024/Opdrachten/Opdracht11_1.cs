using AdventOfCode2024.Classes;

namespace AdventOfCode2024;

class Opdracht11_1 : IOpdracht
{
    public void Run()
    {
        StreamReader sr = new StreamReader("..\\..\\..\\Resources\\O11-1.txt");
        string line = sr.ReadLine();
        string[] numbers = line.Split(' ');
        //Part1(numbers);
        Part2(numbers);
    }

    private void Part2(string[] numbers)
    {
        Dictionary<long, long> oldSituation = new Dictionary<long, long>();
        Dictionary<long, long> newSituation = new Dictionary<long, long>();
        FillDictionary(numbers, oldSituation);

        for(int i = 0; i < 75; i++)
        {
            foreach (long key in oldSituation.Keys)
            {
                Blink(key, oldSituation[key], newSituation);
            }
            Console.WriteLine($"after {i + 1} times, there are {newSituation.Count} unique stones, totalling {CountStones(newSituation)}");
            oldSituation = newSituation;
            newSituation = new Dictionary<long, long>();
        }
    }

    private long CountStones(Dictionary<long, long> dictionary)
    {
        long results = 0;
        foreach (long key in dictionary.Keys)
        {
            results += dictionary[key];
        }
        return results;
    }

    private void Blink(long key, long value, Dictionary<long, long> newSituation)
    {
        if (key == 0)
        {
            AddToDictionary(newSituation, 1, value);
        }
        else if (key.ToString().Length % 2 == 0)
        {
            string number = key.ToString();
            string firstPartNumber = number.Substring(0, number.Length / 2);
            string secondPartNumber = number.Substring(number.Length / 2, number.Length / 2);
            AddToDictionary(newSituation, long.Parse(firstPartNumber), value);
            AddToDictionary(newSituation, long.Parse(secondPartNumber), value);
        }
        else
        {
            AddToDictionary(newSituation, key * 2024, value);
        }
    }

    private static void FillDictionary(string[] numbers, Dictionary<long, long> oldSituation)
    {
        foreach (string number in numbers)
        {
            long numberAsLong = long.Parse(number);
            AddToDictionary(oldSituation, numberAsLong);
        }
    }

    private static void AddToDictionary(Dictionary<long, long> dictionary, long number, long amount = 1)
    {
        if (dictionary.ContainsKey(number))
        {
            dictionary[number] += amount;
        }
        else
        {
            dictionary.Add(number, amount);
        }
    }

    private void Part1(string[] numbers)
    {
        LinkedStone start;
        start = new LinkedStone(Convert.ToInt32(numbers[0]));
        LinkedStone pivot = start;

        for (int i = 1; i < numbers.Length; i++)
        {
            pivot.AddAfter(new LinkedStone(Convert.ToInt32(numbers[i])));
            pivot = pivot.Next;
        }

        for (int i = 0; i < 25; i++)
        {
            pivot = start;
            while (pivot != null)
            {
                pivot = pivot.Blink();
            }
            Console.WriteLine($"after {i + 1} times, there are {start.CountStonesInLine()} stones in line");
        }


        Console.WriteLine(start.CountStonesInLine());
    }
}

class LinkedStone
{
    private LinkedStone _prev;
    private LinkedStone _next;
    private long _number;

    public LinkedStone(long number)
    {
        _number = number;
    }

    public LinkedStone First()
    {
        LinkedStone output = this;
        while (output.Prev != null)
        {
            output = output.Prev;
        }
        return output;
    }

    public LinkedStone Last()
    {
        LinkedStone output = this;
        while (output.Next != null)
        {
            output = output.Next;
        }
        return output;
    }

    public LinkedStone Blink()
    {
        LinkedStone NextStone = _next;
        if(_number == 0)
        {
            _number++;
        }
        else if(_number.ToString().Length % 2 == 0)
        {
            Split();
            NextStone= _next.Next;
        }
        else
        {
            _number *= 2024;
        }

        return NextStone;
    }

    private void Split()
    {
        string number = _number.ToString();
        string firstPartNumber = number.Substring(0, number.Length / 2);
        string secondPartNumber = number.Substring(number.Length / 2, number.Length / 2);
        _number = long.Parse(firstPartNumber);
        AddAfter(new LinkedStone(Convert.ToInt32(secondPartNumber)));
    }

    public void AddAfter(LinkedStone newFile)
    {
        if(_next != null)
        {
            newFile.Next = _next;
            _next.Prev = newFile;
        }

        _next = newFile;
        newFile.Prev = this;
    }

    public long CountStonesInLine()
    {
        long result = 1;
        LinkedStone start = First();
        while (start.Next != null)
        {
            result++;
            start = start.Next;
        }
        return result;
    }

    public LinkedStone Prev { get { return _prev; } set { _prev = value; } }
    public LinkedStone Next { get { return _next; } set { _next = value; } }
}
