using System.Numerics;

class Position
{
    public int X;
    public int Y;

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
    public Position()
    {
        X = 0;
        Y = 0;
    }
    public Position Move(int x, int y)
    {
        return new Position(X + x, Y + y);
    }
    public bool OutOfBounds(int bound)
    {
        return X < 0 || Y < 0 || X >= bound || Y >= bound;
    }
    public bool OutOfBounds(int height, int width)
    {
        return X < 0 || Y < 0 || X >= width || Y >= height;
    }
    
    public static Position operator +(Position a, Position b)
    {
        return new Position(a.X + b.X, a.Y + b.Y);
    }
    public static Position operator -(Position a, Position b)
    {
        return new Position(a.X - b.X, a.Y - b.Y);
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
