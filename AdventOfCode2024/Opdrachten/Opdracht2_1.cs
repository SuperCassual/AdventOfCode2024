using AdventOfCode2024.Interfaces;

namespace AdventOfCode2024.Opdrachten
{
    class Opdracht2_1 : IOpdracht
    {
        public void Run()
        {
            StreamReader sr = new StreamReader("..\\..\\..\\Resources\\O2-1.txt");
            string line = sr.ReadLine();
            int safeReports = 0;
            int problemDampenerSafeReports = 0;

            while (line != null && line != "")
            {
                string[] sequenceString = line.Split(' ');
                List<int> sequence = new List<int>();
                for(int i = 0, length = sequenceString.Length; i < length; i++)
                {
                    sequence.Add(int.Parse(sequenceString[i]));
                }

                if (SafetyTest(sequence))
                {
                    safeReports++;
                }
                if(ProblemDampenerSafetyTest(sequence))
                {
                    problemDampenerSafeReports++;
                }
           
                line = sr.ReadLine();
            }

            Console.WriteLine("Safe reports: {0}", safeReports);
            Console.WriteLine("Safe reports with assistance of the Problem Dampener: {0}", problemDampenerSafeReports);
        }

        private bool SafetyTest(List<int> sequence)
        {
            return IsThisSafe(sequence, out int unused);
        }

        private bool ProblemDampenerSafetyTest(List<int> sequence)
        {
            int indexOfFault;
            if(IsThisSafe(sequence, out indexOfFault))
            {
                return true;
            }
            List<int> sequenceClone = new List<int>(sequence);
            sequenceClone.RemoveAt(indexOfFault);
            if(IsThisSafe(sequenceClone, out indexOfFault))
            {
                return true;
            }
            sequence.Reverse();
            IsThisSafe(sequence, out indexOfFault);
            sequence.RemoveAt(indexOfFault);
            return IsThisSafe(sequence, out indexOfFault);
        }


        public bool IsThisSafe(List<int> sequence, out int indexOfFault)
        {
            indexOfFault = -1;
            int modus;
            try
            {
                modus = (sequence[1] - sequence[0]) / Math.Abs(sequence[1] - sequence[0]);
            }
            catch (DivideByZeroException)
            {
                indexOfFault = 0;
                return false; //means both numbers are the same and therefore unsafe
            }
            catch (ArgumentOutOfRangeException)
            {
                return true; //means there's only one number which is technically safe
            }

            for (int i = 0, length = sequence.Count; i + 1 < length; i++)
            {
                int difference = (sequence[i + 1] - sequence[i]) * modus;
                if (1 > difference || difference > 3)
                {
                    indexOfFault = i + 1;
                    return false;
                }
            }
            return true;
        }
    }
}
