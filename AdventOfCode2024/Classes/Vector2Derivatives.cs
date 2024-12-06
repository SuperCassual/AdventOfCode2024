namespace AdventOfCode2024.Classes
{
    struct String2
    {
        private string _value1;
        private string _value2;

        public String2(string one, string two)
        {
            _value1 = one;
            _value2 = two;
        }

        public string X { get { return _value1; } set { _value1 = value; } }
        public string Y { get { return _value2; } set { _value2 = value; } }
    }

    struct Int2
    {
        private int _value1;
        private int _value2;

        public Int2(int one, int two)
        {
            _value1 = one;
            _value2 = two;
        }

        public static bool operator==(Int2 firstInput, Int2 secondInput)
        {
            return firstInput.X == secondInput.X && firstInput.Y == secondInput.Y;
        }

        public static bool operator!=(Int2 firstInput, Int2 secondInput)
        {
            return firstInput.X != secondInput.X || firstInput.Y != secondInput.Y;
        }

        public override string ToString()
        {
            return $"X:{this.X}, Y:{this.Y}";
        }

        public int X { get { return _value1; } set { _value1 = value; } }
        public int Y { get { return _value2; } set { _value2 = value; } }
    }
}
