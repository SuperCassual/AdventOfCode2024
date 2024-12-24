namespace AdventOfCode2024;

class ClawMachine
{
    private readonly Long2 _aMove;
    private readonly Long2 _bMove;
    private readonly Long2 _prizeLocation;

    public ClawMachine(Long2 aMove, Long2 bMove, Long2 prizeLocation)
    {
        _aMove = aMove;
        _bMove = bMove;
        _prizeLocation = prizeLocation;
    }

    public bool TryGetPrize(out long tokenCost)
    {
        long maxBPresses = _prizeLocation.X / _bMove.X;

        for (; maxBPresses >= 0; maxBPresses--)
        {
            Long2 position = maxBPresses * _bMove;
            position = _prizeLocation - position;
            if (position.X % _aMove.X != 0 || position.Y % _aMove.Y != 0)
            {
                continue;
            }
            long aPresses = position.X / _aMove.X;
            Long2 aPosition = aPresses * _aMove;

            if (aPosition == position)
            {
                tokenCost = aPresses * 3 + maxBPresses;
                return true;
            }


        }

        tokenCost = 0;
        return false;
    }
}
