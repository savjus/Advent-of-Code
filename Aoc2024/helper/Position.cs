class Position
{
    public int X;
    public int Y;

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
    public Position Move(int x, int y)
    {
        return new Position(X + x, Y + y);
    }
    public bool OutOfBounds(int bound)
    {
        return X < 0 || Y < 0 || X >= bound || Y >= bound;
    }
    public override bool Equals(object? obj)
    {
        if (obj is Position other)
            return X == other.X && Y == other.Y;
        return false;
    }
    public override int GetHashCode()
    {
        return X * 213 ^ Y;
    }
    public override string ToString()
    {
        return $"X:{X}  Y:{Y}";
    }
}
