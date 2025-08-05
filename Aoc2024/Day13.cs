namespace Aoc2024
{
    class Day13 : IAocDay
    {
        string []input;
        public Day13(string input)
        {
            this.input = input.Split('\n');
        }
        public long Part1()
        {
            (long, long) A;
            (long, long) B;
            (long, long) Prize;

            long Abutt = 0;
            long Bbutt = 0;
            long result = 0;
            for (int i = 0; i < input.Length; i += 2)
            {
                A = ParseButton(input[i]);
                B = ParseButton(input[++i]);
                Prize = ParseGoal(input[++i]);
                // System.Console.WriteLine("A:" + A + "B:" + B + "goal:" + Prize);

                if(calculateCheapestPrize(A, B, Prize, ref Abutt, ref Bbutt))
                {
                    result += Abutt * 3 + Bbutt;  
                }
            }
            return result;
        }
        public long Part2()
        {
            (long, long) A;
            (long, long) B;
            (long, long) Prize;

            long Abutt = 0;
            long Bbutt = 0;
            long result = 0;
            for (int i = 0; i < input.Length; i += 2)
            {
                A = ParseButton(input[i]);
                B = ParseButton(input[++i]);
                Prize = ParseGoal(input[++i]);
                Prize.Item1 += 10000000000000;
                Prize.Item2 += 10000000000000;
                // System.Console.WriteLine("A:" + A + "B:" + B + "goal:" + Prize);

                if (calculateCheapestPrize(A, B, Prize, ref Abutt, ref Bbutt))
                {
                    result += Abutt * 3 + Bbutt;
                }
            }
            return result;
        }
        // counts how many button presses
        // using matrix math, kramers method (below explain)
        //  1 2   8 ->  8 8  1 2    
        //  3 4   8 ->  3 4  8 8
        bool calculateCheapestPrize((long, long) A, (long, long) B, (long, long) goal, ref long Abutt, ref long Bbutt)
        {
            //matrix
            long ax = A.Item1; long ay = A.Item2;
            long bx = B.Item1; long by = B.Item2;
            long goalx = goal.Item1; long goaly = goal.Item2;

            long det = ax * by - ay * bx;
            if (det == 0)
                return false;

            long AbuttNum = goalx * by - goaly * bx;
            long BbuttNum = goaly * ax - goalx * ay;
            if (AbuttNum % det != 0 || BbuttNum % det != 0)
                return false;

            Abutt = AbuttNum / det;
            Bbutt = BbuttNum / det;

            return true;
        }
        (long, long) ParseButton(string line) {
            (long, long) result;
            string[] parts = line.Split([':', '+', ' ',','], StringSplitOptions.RemoveEmptyEntries);
            long X = long.Parse(parts[3]);
            long Y = long.Parse(parts[5]);
            result = (X,Y);
            return result;
        }
        (long, long) ParseGoal(string line)
        {
             (long, long) result;
            string[] parts = line.Split([':', '=', ' ',','], StringSplitOptions.RemoveEmptyEntries);
            long X = long.Parse(parts[2]);
            long Y = long.Parse(parts[4]);
            result = (X,Y);
            return result;
        }
    }
}
