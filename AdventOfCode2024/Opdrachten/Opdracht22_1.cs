namespace AdventOfCode2024;

class Opdracht22_1 : IOpdracht
{
    public void Run()
    {
        List<long> numbers = new List<long>();
        long result = 0;
        StreamReader sr = new StreamReader("..\\..\\..\\Resources\\T22-1.txt");
        string line = sr.ReadLine();
        while (line != null && line != "")
        {
            numbers.Add(long.Parse(line));
            line = sr.ReadLine();
        }

        foreach (long number in numbers)
        {
            result += TwoThousandSecretNumbers(number);
        }
        Console.WriteLine(result);

        Brute(numbers);

    }

    private void Brute(List<long> numbers)
    {
        long result = 0;
        for (int a = -9; a < 10; a++)
        {
            for (int b = -9; b < 10; b++)
            {
                if(a + b >= -9 && a + b <= 9)
                {
                    for (int c = -9; c < 10; c++)
                    {
                        if (b + c >= -9 && b + c <= 9 && a + b + c >= -9 && a + b + c <= 9)
                        {
                            for (int d = -9; d < 10; d++)
                            {
                                if(c + d >= -9 && c + d <= 9 && a + b + c + d >= -9 && a + b + c + d <= 9)
                                {
                                    result = Math.Max(GetBanana(a, b, c, d, numbers), result);
                                    if (result == 24)
                                    {
                                        bool yes = true;
                                    }
                                }                               
                            }
                        }
                    }
                }
            }
        }
        Console.WriteLine(result);
    }

    private long GetBanana(int a, int b, int c, int d, List<long> numbers)
    {
        long bananas = 0;

        foreach (long number in numbers)
        {
            bananas += TwoThousandSecretNumbersWithStyle(a, b, c, d, number);
        }

        return bananas;
    }

    private long TwoThousandSecretNumbersWithStyle(int a, int b, int c, int d, long intput)
    {
        List<int> changes = new List<int>();
        for (int i = 0; i < 2000; i++)
        {
            long result = Multi64(intput);
            result = Div32(result);
            result = Multi2048(result);

            Console.WriteLine(Convert.ToString(result, 2));

            changes.Add((int)((result % 10) - (intput % 10)));
            if(changes.Count >= 5)
            {
                if (changes[1] == a && changes[2] == b && changes[3] == c && changes[4] == d)
                {
                    return result % 10;
                }
                changes.RemoveAt(0);
            }
            intput = result;
        }
        return 0;
    }

    private long TwoThousandSecretNumbers(long intput)
    {
        for (int i = 0; i < 2000; i++)
        {
            
            intput = Multi64(intput);
            intput = Div32(intput);
            intput = Multi2048(intput);
        }
        return intput;
    }

    private long Multi64(long intput)
    {
        long newSecret = intput * 64;
        intput = Mix(intput, newSecret);
        return Prune(intput);
    }

    private long Div32(long intput)
    {
        long newSecret = intput / 32;
        intput = Mix(intput, newSecret);
        return Prune(intput);
    }

    private long Multi2048(long intput)
    {
        long newSecret = intput * 2048;
        intput = Mix(intput, newSecret);
        return Prune(intput);
    }

    private long Mix(long intput, long secret)
    {
        return intput^secret;
    }

    private long Prune(long intput)
    {
        return intput % 16777216;
    }
}
