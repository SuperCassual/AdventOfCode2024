using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2024.Interfaces;
using System.Text.RegularExpressions;
using System.Net;

namespace AdventOfCode2024.Opdrachten
{
    class Opdracht3_1 : IOpdracht
    {
        const string regexMatch = @"mul\(([0-9]{1,3}),([0-9]{1,3})\)";

        public void Run()
        {
            StreamReader sr = new StreamReader("..\\..\\..\\Resources\\O3-1.txt");
            string line = sr.ReadLine();
            string jumble = "";

            while (line != null && line != "")
            {
                jumble += line;
                line = sr.ReadLine();
            }

            Console.WriteLine("Total mult: {0}", MatchMults(jumble));
            Console.WriteLine("Total test logicmult: {0}, should be 48", SimonSaysMatchMults("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"));
            Console.WriteLine("Total logicmult: {0}", SimonSaysMatchMults(jumble));
        }

        private int MatchMults(string input)
        {
            int result = 0;
            MatchCollection matches = Regex.Matches(input, regexMatch);

            foreach (Match match in matches)
            {
                result += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
            }
            return result;
        }

        private int SimonSaysMatchMults(string input)
        {
            int result = 0;
            List<int> positionOfDos = FillDos(input);
            List<int> positionOfDonts = FillDonts(input);
            bool aListHasEnded = false;

            foreach (Match match in Regex.Matches(input, regexMatch))
            {
                while (match.Index > positionOfDos[0] && match.Index > positionOfDonts[0] && !aListHasEnded)
                {
                    if(positionOfDos[0] < positionOfDonts[0])
                    {
                        if(positionOfDos.Count > 1)
                        {
                            positionOfDos.RemoveAt(0);
                        }
                        else
                        {
                            aListHasEnded = true;
                        }                       
                    }
                    else
                    {
                        if (positionOfDonts.Count > 1)
                        {
                            positionOfDonts.RemoveAt(0);
                        }
                        else
                        {
                            aListHasEnded = true;
                        }
                    }
                }

                if((positionOfDos[0] < positionOfDonts[0] && !aListHasEnded) ||
                    (aListHasEnded && positionOfDos[0] > positionOfDonts[0]))
                {
                    result += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                }
            }
            return result;
        }

        private List<int> FillDos(string input)
        {
            List<int> dos = new List<int>();
            string pattern = @"do\(\)";
            dos.Add(0); //mults start enabled
            RegexMatchPositionToList(ref dos, input, pattern);
            return dos;
        }

        private List<int> FillDonts(string input)
        {
            List<int> donts = new List<int>();
            string pattern = @"don't\(\)";
            RegexMatchPositionToList(ref donts, input, pattern);
            return donts;
        }

        private void RegexMatchPositionToList(ref List<int> indexList, string input, string pattern)
        {
            MatchCollection matches = Regex.Matches(input, pattern);
            foreach (Match match in matches)
            {
                indexList.Add(match.Index);
            }
        }
    }
}
