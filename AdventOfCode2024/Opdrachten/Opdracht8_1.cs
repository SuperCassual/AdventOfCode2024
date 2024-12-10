using AdventOfCode2024.Interfaces;
using AdventOfCode2024.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AdventOfCode2024.Opdrachten
{
    class Opdracht8_1 : IOpdracht
    {
        public void Run()
        {
            StreamReader sr = new StreamReader("..\\..\\..\\Resources\\O8-1.txt");
            string line = sr.ReadLine();
            Hashtable interferences = new Hashtable();
            int xCoord = 0;
            int yCoord = 0;
            for(; line != "" && line != null; line = sr.ReadLine(), yCoord++)
            {
                for(xCoord = 0; xCoord < line.Length; xCoord++)
                {
                    if (line[xCoord] != '.')
                    {
                        AddValueToHashtable(interferences, line[xCoord], new Int2(xCoord, yCoord));
                    }
                }
            }
            List<Int2> antiNodes = new List<Int2>();
            foreach(DictionaryEntry interferenceGroup in interferences)
            {
                List<Int2> coordinates = (List<Int2>)interferenceGroup.Value;
                while(coordinates.Count > 1)
                {
                    antiNodes.Add(coordinates[0]);
                    for(int i = 1; i < coordinates.Count; i++)
                    {
                        Int2 outputcoord = OutPutCoords(coordinates[0], coordinates[i]);
                        HarmonicResonantAdd(xCoord, yCoord, antiNodes, outputcoord, coordinates[0]);
                        outputcoord = OutPutCoords(coordinates[i], coordinates[0]);
                        HarmonicResonantAdd(xCoord, yCoord, antiNodes, outputcoord, coordinates[i]);
                    }
                    coordinates.RemoveAt(0);
                }
                antiNodes.Add(coordinates[0]);
            }
            var noDupes = antiNodes.Distinct().ToList();
            Console.WriteLine(noDupes.Count);
        }

        private void HarmonicResonantAdd(int xCoord, int yCoord, List<Int2> antiNodes, Int2 outputcoord, Int2 previouscoord)
        {
            if (0 <= outputcoord.X && outputcoord.X < xCoord && 0 <= outputcoord.Y && outputcoord.Y < yCoord)
            {
                antiNodes.Add(outputcoord);
                Int2 newinputcoord = OutPutCoords(outputcoord, previouscoord);
                HarmonicResonantAdd(xCoord, yCoord, antiNodes, newinputcoord, outputcoord);
                Console.WriteLine(outputcoord);
            }
        }

        private Int2 OutPutCoords(Int2 one, Int2 two)
        {
            int xDifference = one.X - two.X;
            int yDifference = one.Y - two.Y;
            return new Int2(one.X + xDifference, one.Y + yDifference);
        }

        private void AddValueToHashtable(Hashtable interferences, char key, Int2 coordinate)
        {
            if (!interferences.ContainsKey(key))
            {
                interferences.Add(key, new List<Int2>());
            }
            (interferences[key] as List<Int2>).Add(coordinate);
        }
    }
}
