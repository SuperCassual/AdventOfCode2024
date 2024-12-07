using AdventOfCode2024.Interfaces;

namespace AdventOfCode2024.Opdrachten
{
    class Opdracht7_1 : IOpdracht
    {
        public void Run()
        {
            long result = 0;
            long result2 = 0;
            StreamReader sr = new StreamReader("..\\..\\..\\Resources\\O7-1.txt");
            string line = sr.ReadLine();

            while (line != null && line != "")
            {
                string[] splitLine = line.Split(": ");
                long product = long.Parse(splitLine[0]);
                splitLine = splitLine[1].Split(' ');
                List<long> elements = new List<long>();
                for (int i = 0, count = splitLine.Length; i < count; i++)
                {
                    elements.Add(int.Parse(splitLine[i]));
                }
                result += CanElementsMakeProductResult(product, elements);
                result2 += CanElementsMakeProductResult(product, elements, true);

                line = sr.ReadLine();
            }

            Console.WriteLine(result);
            Console.WriteLine(result2);
        }

        private long CanElementsMakeProductResult(long product, List<long> elements, bool useConcatenation = false)
        {
            List<List<long>> elementsSequences = new List<List<long>>();
            elementsSequences.Add(elements);
            while (elementsSequences.Count > 0)
            {
                elements = elementsSequences[0];
                if (elements.Count == 1 && elements[0] == product)
                {
                    return product;
                }
                if (elements.Count > 1)
                {
                    if (elements[0] * elements[1] <= product)
                    {
                        List<long> multOperationAdd = new List<long>(elements);
                        multOperationAdd[1] = elements[0] * elements[1];
                        multOperationAdd.RemoveAt(0);
                        elementsSequences.Add(multOperationAdd);
                    }
                    if (elements[0] + elements[1] <= product)
                    {
                        List<long> additionOperationAdd = new List<long>(elements);
                        additionOperationAdd[1] = elements[0] + elements[1];
                        additionOperationAdd.RemoveAt(0);
                        elementsSequences.Add(additionOperationAdd);
                    }
                    if (useConcatenation)
                    {
                        string longAsString = elements[0].ToString() + elements[1].ToString();
                        long concatenationLong = long.Parse(longAsString);
                        if(concatenationLong <= product)
                        {
                            List<long> additionOperationAdd = new List<long>(elements);
                            additionOperationAdd[1] = concatenationLong;
                            additionOperationAdd.RemoveAt(0);
                            elementsSequences.Add(additionOperationAdd);
                        }
                    }
                }
                elementsSequences.RemoveAt(0);
            }
            return 0;
        }
    }
}
