using AdventOfCode2024.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Opdrachten
{
    class Opdracht1_1 : IOpdracht
    {
        public void Run()
        {
            StreamReader sr = new StreamReader("..\\..\\..\\Resources\\O1-1.txt");
            List<int> listLeft = new List<int>();
            List<int> listRight = new List<int>();
            string line = sr.ReadLine();

            while(line != null && line != "")
            {
                string[] duoInput = line.Split("   ");
                listLeft.Add(int.Parse(duoInput[0]));
                listRight.Add(int.Parse(duoInput[1]));
                line = sr.ReadLine();
            }
            listLeft.Sort();
            listRight.Sort();

            int distance = CalculateDistanceNumber(listLeft, listRight);
            Console.WriteLine("Distance: {0}", distance);

            int similarityScore = CalculateSimilarityScore(listLeft, listRight);
            Console.WriteLine("Similarity: {0}", similarityScore);
        }

        private int CalculateDistanceNumber(List<int> listLeft, List<int> listRight)
        {
            int distance = 0;
            for (int i = 0, length = listLeft.Count; i < length; i++)
            {
                distance += Math.Max(listLeft[i], listRight[i]) - Math.Min(listLeft[i], listRight[i]);
            }
            return distance;
        }

        private int CalculateSimilarityScore(List<int> listLeft, List<int> listRight)
        {
            int similarityScore = 0;

            for(int leftPivot = 0, rightPivot = 0, listCount = listLeft.Count; leftPivot < listCount; leftPivot++)
            {
                int appearances = 0;
                while(rightPivot < listCount && listLeft[leftPivot] >= listRight[rightPivot])
                {
                    if(listLeft[leftPivot] == listRight[rightPivot])
                    {
                        appearances++;
                    }
                    rightPivot++;
                }
                similarityScore += listLeft[leftPivot] * appearances;
            }
            return similarityScore;
        }

    }
}
