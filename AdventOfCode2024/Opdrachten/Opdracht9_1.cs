using AdventOfCode2024.Classes;
using AdventOfCode2024.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Opdrachten
{
    class Opdracht9_1 : IOpdracht
    {
        public void Run()
        {
            StreamReader sr = new StreamReader("..\\..\\..\\Resources\\O9-1.txt");
            string thisVariableNameIsSuperLongToMatchTheGravitasOfTheLineThatIsBeingRead = sr.ReadLine();

            Dictionary<int, int> thisIsgoingToBeLongTooInnit = FillDictionary(thisVariableNameIsSuperLongToMatchTheGravitasOfTheLineThatIsBeingRead);
            DefragDictionary(thisIsgoingToBeLongTooInnit);
            long checkSum = CalculateCheckSum(thisIsgoingToBeLongTooInnit);
            Console.WriteLine(checkSum);

            LinkedFile startLinkedList = FillDictionaryBlocks(thisVariableNameIsSuperLongToMatchTheGravitasOfTheLineThatIsBeingRead);
            DefragDictionaryBlocks(startLinkedList);
            startLinkedList.WriteList();
            checkSum = CalculateCheckSumInBlocks(startLinkedList);
            Console.WriteLine(checkSum);
        }

        private Dictionary<int, int> FillDictionary(string inputstring)
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            for (int i = 0, dictionaryPivot = 0, length = inputstring.Length; i < length; i++)
            {
                string sizeString = inputstring.Substring(i, 1);
                int size = int.Parse(sizeString);
                int idNumber;
                if (i % 2 == 0)
                {
                    idNumber = i / 2;
                }
                else
                {
                    idNumber = -1;
                }
                for (int j = 0; j < size; j++, dictionaryPivot++)
                {
                    dictionary[dictionaryPivot] = idNumber;
                }
            }
            return dictionary;
        }

        private LinkedFile FillDictionaryBlocks(string inputstring)
        {
            LinkedFile startPoint = null, currentPoint = null;
            int size = 0;
            for (int i = 0, dictionaryPivot = 0, length = inputstring.Length; i < length; i++, dictionaryPivot += size)
            {
                string sizeString = inputstring.Substring(i, 1);
                size = int.Parse(sizeString);
                int idNumber;
                if (i % 2 == 0)
                {
                    idNumber = i / 2;
                }
                else
                {
                    idNumber = -1;
                }
                if (size > 0)
                {
                    if(startPoint == null)
                    {
                        startPoint = new LinkedFile(idNumber, dictionaryPivot, size);
                        currentPoint = startPoint;
                    }
                    else
                    {
                        currentPoint.AddAfter(new LinkedFile(idNumber, dictionaryPivot, size));
                        currentPoint = currentPoint.Next;
                    }
                }
            }
            return startPoint;
        }

        private void DefragDictionary(Dictionary<int, int> dictionary)
        {
            int minusonepivot = 0;
            int endoflinepivot = dictionary.Count - 1;
            while (minusonepivot < endoflinepivot)
            {
                if (dictionary[minusonepivot] == -1 && dictionary[endoflinepivot] != -1)
                {
                    dictionary[minusonepivot] = dictionary[endoflinepivot];
                    dictionary[endoflinepivot] = -1;

                }
                while (dictionary[minusonepivot] != -1 && minusonepivot < endoflinepivot)
                {
                    minusonepivot++;
                }
                while (dictionary[endoflinepivot] == -1 && minusonepivot < endoflinepivot)
                {
                    endoflinepivot--;
                }
            }
        }

        private void DefragDictionaryBlocks(LinkedFile start)
        {
            LinkedFile pivot = start.Last();
            while (pivot != start)
            {
                LinkedFile spaceFinder = start;
                LinkedFile prevPivot = pivot.Prev;
                while (spaceFinder != pivot)
                {
                    if(spaceFinder.Id == -1 && spaceFinder.Size >= pivot.Size)
                    {
                        pivot.Replace(new LinkedFile(-1, pivot.Position, pivot.Size));
                        pivot.AddThisInBetween(spaceFinder.Prev, spaceFinder.Next, spaceFinder);
                        break;
                    }
                    spaceFinder = spaceFinder.Next;
                }
                pivot = prevPivot;
            }
        }

        private long CalculateCheckSum(Dictionary<int, int> dictionary)
        {
            Console.WriteLine(dictionary.Count);
            long result = 0;
            int index = 0;
            foreach (var dr in dictionary)
            {

                if (dr.Value >= 0)
                {
                    if (index < dr.Key)
                    {
                        index = dr.Key;
                    }
                    result += dr.Key * dr.Value;
                }
            }
            return result;
        }

        private long CalculateCheckSumInBlocks(LinkedFile startPosition)
        {
            long result = 0;
            while (startPosition != null)
            {
                result += startPosition.CalculateCheckSum();
                startPosition = startPosition.Next;
            }
            return result;
        }
    }
}
