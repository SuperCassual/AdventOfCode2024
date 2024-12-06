using AdventOfCode2024.Enums;

namespace AdventOfCode2024.Classes
{
    class GhostGuard : Guard
    {
        private char[,] personalHell;
        private Int2 startPosition, startObstacle;
        private Direction startDirection;
        private bool _looping = false;
        private bool _gone = false;
        private List<Int2> turns = new List<Int2>();

        public GhostGuard(char[,] grid, Int2 startPosition, Direction startDirection, string name = "Ghost Cynthia")
            : base(startPosition, startDirection, name)
        {
            this.startPosition = startPosition;
            this.startDirection = startDirection;
            personalHell = (char[,])grid.Clone();
            int turnCount = 0;
            while (PathObstructed(personalHell))
            {
                turnCount++;
                Turn();
                if (turnCount > 3)
                {
                    Console.WriteLine($"{name} somehow got herself stuck at {_position}");
                    break;
                }
            }
            try
            {
                if(personalHell[GetFacingPos().X, GetFacingPos().Y] == 'X')
                {
                    _gone = true;
                }
                else 
                {
                    personalHell[GetFacingPos().X, GetFacingPos().Y] = 'O';
                    startObstacle = GetFacingPos();
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine($"{name} saw the light");
            }
        }

        public void Step()
        {
            Int2 nextPos = Move(personalHell);
            if (IsOutOfBounds(personalHell, nextPos))
            {
                _gone = true;
            }
            else if (startPosition == _position && startDirection == _facingDirection)
            {
                _looping = true;
            }

        }

        protected override void Turn()
        {
            base.Turn();
            for (int i = 0, count = turns.Count - 1; i < count; i++) //skip the last one, we could turn twice at a junction
            {
                if (turns[i] == _position)
                {
                    _looping = true;
                }
            }
            turns.Add(_position);
        }

        public bool IsOutOfBounds(char[,] grid, Int2 pos)
        {
            try
            {
                _ = grid[pos.X, pos.Y];
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        public void PrintGrid()
        {
            Console.WriteLine($"Torure placed at {startObstacle}");
            for (int j = 0; j < 10; j++)
            {
                string line = "";
                for (int i = 0; i < 10; i++)
                {
                    line += personalHell[i, j];
                }
                Console.WriteLine(line);
            }
        }

        public bool IsInALoop { get { return _looping; } }
        public bool HasLeftTheBuilding { get { return _gone; } }
        public Int2 StartObstacle { get { return startObstacle; } }
    }
}
