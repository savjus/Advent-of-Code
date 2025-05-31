namespace Aoc2023;

public class Day11 : IAocDay
{
    private List<Coordinate2D> Stars;
    private List<int> EmptyRows;
    private List<int> EmptyCols;
    public Day11(string input) 
    {
       
        Stars = new List<Coordinate2D>();
        EmptyRows = new List<int>();
        EmptyCols = new List<int>();
        string[] lines = input.Split('\n');
        for(int i = 0; i < lines.Length; i++)
        {
            // stars
            for (int j = 0; j < lines[i].Length; j++)
                if (lines[i][j] == '#')
                    Stars.Add(new Coordinate2D(i, j));
            //Empty rows
            if (!lines[i].Contains("#"))
                EmptyRows.Add(i);
        }

        //Empty columns
        for(int i = 0;i < lines[0].Length; i++)
        {
            bool star = false;
            for(int j = 0; j< lines.Length-1; j++)
            {
                if (lines[j][i] == '#')
                {
                    star = true;
                    break;
                }
            }
            if (!star)
                EmptyCols.Add(i);
        }
       
        //foreach(int EmptyRow in EmptyRows) 
        //    Console.WriteLine(EmptyRow);
        //foreach(int EmptyRow in EmptyCols) 
        //    Console.WriteLine(EmptyRow);
    }
    public long Part1()
    {
        return GetTotalDistance(1);
    }
    public long Part2()
    {
        return GetTotalDistance(999999);
    }
   
    private long GetTotalDistance(int step)
    {
        long total = 0;
        for(int i =0; i < Stars.Count; i++)
            for(int j =i;j<Stars.Count; j++)
                total += FindDistance(Stars[i], Stars[j],step);
       

        return total;
    }
    private long FindDistance(Coordinate2D S1, Coordinate2D S2, int step)
    {
        long dist = 0;
        int Xmin = Math.Min(S1.X, S2.X);
        int Xmax = Math.Max(S1.X, S2.X);

        int Ymin = Math.Min(S1.Y, S2.Y);
        int Ymax = Math.Max(S1.Y, S2.Y);
        //find normal distance
        dist = (Xmax - Xmin) + (Ymax - Ymin);

        // add the distance of expansion from empty rows/cols
        for (int i = 0; i < EmptyRows.Count; i++)
            if (Xmax > EmptyRows[i] && EmptyRows[i] > Xmin)
                dist += step;

        for (int i = 0; i < EmptyCols.Count; i++)
            if (Ymax > EmptyCols[i] && EmptyCols[i] > Ymin)
                dist += step;

        return dist;
    }
}

