namespace AdventOfCode2024;

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

    public Int2()
    {
        _value1 = 0;
        _value2 = 0;
    }

    public static bool operator==(Int2 firstInput, Int2 secondInput)
    {
        return firstInput.X == secondInput.X && firstInput.Y == secondInput.Y;
    }

    public static bool operator!=(Int2 firstInput, Int2 secondInput)
    {
        return firstInput.X != secondInput.X || firstInput.Y != secondInput.Y;
    }

    public static Int2 operator+(Int2 firstInput, Int2 secondInput)
    {
        return new Int2(firstInput.X + secondInput.X, firstInput.Y + secondInput.Y);
    }

    public static Int2 operator-(Int2 firstInput, Int2 secondInput)
    {
        return new Int2(firstInput.X - secondInput.X, firstInput.Y - secondInput.Y);
    }

    public static Int2 operator*(int integer, Int2 doubleInt)
    {
        return new Int2(integer * doubleInt.X, integer * doubleInt.Y);
    }
    public static Int2 operator *(Int2 doubleInt, int integer)
    {
        return integer * doubleInt;
    }

    public override string ToString()
    {
        return $"X:{this.X}, Y:{this.Y}";
    }

    public int A { get { return _value1; } set { _value1 = value; } }
    public int B { get { return _value2; } set { _value2 = value; } }
    public int X { get { return _value1; } set { _value1 = value; } }
    public int Y { get { return _value2; } set { _value2 = value; } }
}

struct Long2
{
    private long _value1;
    private long _value2;

    public Long2(long one, long two)
    {
        _value1 = one;
        _value2 = two;
    }

    public Long2()
    {
        _value1 = 0;
        _value2 = 0;
    }

    public static bool operator ==(Long2 firstInput, Long2 secondInput)
    {
        return firstInput.X == secondInput.X && firstInput.Y == secondInput.Y;
    }

    public static bool operator !=(Long2 firstInput, Long2 secondInput)
    {
        return firstInput.X != secondInput.X || firstInput.Y != secondInput.Y;
    }

    public static Long2 operator +(Long2 firstInput, Long2 secondInput)
    {
        return new Long2(firstInput.X + secondInput.X, firstInput.Y + secondInput.Y);
    }

    public static Long2 operator -(Long2 firstInput, Long2 secondInput)
    {
        return new Long2(firstInput.X - secondInput.X, firstInput.Y - secondInput.Y);
    }

    public static Long2 operator *(long integer, Long2 doubleInt)
    {
        return new Long2(integer * doubleInt.X, integer * doubleInt.Y);
    }
    public static Long2 operator *(Long2 doubleInt, long integer)
    {
        return integer * doubleInt;
    }

    public override string ToString()
    {
        return $"X:{this.X}, Y:{this.Y}";
    }

    public long A { get { return _value1; } set { _value1 = value; } }
    public long B { get { return _value2; } set { _value2 = value; } }
    public long X { get { return _value1; } set { _value1 = value; } }
    public long Y { get { return _value2; } set { _value2 = value; } }
}
