namespace Aoc2024
{
    class Day14 : IAocDay
    {
        // count where robots will be after X time, given startpos and move direction/speed
        // ""The robots outside the actual bathroom are in a space which is 101 tiles wide and 103 tiles tall""
        string[] inputs;
        readonly int gridHeight = 103;
        readonly int gridWidth = 101;
        public Day14(string input)
        {
            inputs = input.Split('\n');
        }
        // read robot data, figure out where it will be, add to quadrant 
        public long Part1()
        {
            int time = 100;
            int[] quadrantCount = { 0, 0, 0, 0 };
            foreach (string line in inputs)
            {
                (Position, Position) robot = ParseLine(line);
                Position movedPos = moveRobotTimes(robot, time);
                switch (detectQuadrant(movedPos))
                {
                    case 0:
                        quadrantCount[0]++;
                        break;
                    case 1:
                        quadrantCount[1]++;
                        break;
                    case 2:
                        quadrantCount[2]++;
                        break;
                    case 3:
                        quadrantCount[3]++;
                        break;
                    case -1:
                        break;
                }
            }
            long result = 1;
            foreach (int count in quadrantCount)
            {
                result *= count;
            }
                return result;
        }
        // move robots until christmas tree pattern made
        public long Part2()
        {
            List<(Position, Position)> robots = new List<(Position, Position)>();
            foreach (string line in inputs)
            {
                robots.Add(ParseLine(line));
            }


            int seconds = 0;
            while (!isUniquePattern(robots))
            {
                for (int i = 0; i < robots.Count; i++)
                {
                    robots[i] = (moveRobotTimes(robots[i], 1), robots[i].Item2);
                }
                seconds++;
            }
            return seconds;
        }

        // NOT A CHRISTMAS TREE PATTERN CHECK
       // check for a "unique" pattern, where every robot is in unique pos
        bool isUniquePattern( List<(Position, Position)> robots)
        {
            HashSet<Position> uniquePos = new HashSet<Position>();
            foreach (var pos in robots)
            {
                if (!uniquePos.Add(pos.Item1))
                {
                    return false;
                }
            }
            return true;
        }
        Position moveRobotTimes((Position,Position) robot,int time)
        {
            Position startPos = robot.Item1;
            Position movement = robot.Item2;
            
            // avoid negative endpos by adding gridwidth again
            int endX = ((startPos.X + movement.X*time) % gridWidth + gridWidth) % gridWidth;
            int endY = ((startPos.Y + movement.Y * time) % gridHeight + gridHeight) % gridHeight;
            Position afterMovement = new Position(endX,endY);
            
            return afterMovement;
        }
        int detectQuadrant(Position endPosition)
        {
            // grid 103 tall, 101 wide
            // middle is null position
            int middleHorizontal = (gridHeight - 1) / 2;
            int middleVertical = (gridWidth - 1) / 2;
            if (endPosition.X > middleVertical && endPosition.Y > middleHorizontal) //bottom right
                return 3;
            if (endPosition.X > middleVertical && endPosition.Y < middleHorizontal) //top right
                return 1;
            if (endPosition.X < middleVertical && endPosition.Y > middleHorizontal) //bottom left
                return 2;
            if (endPosition.X < middleVertical && endPosition.Y < middleHorizontal) //top left
                return 0;
            return -1;
        }
        // returns Starting position and movement direction (represented as position aswell since its just a tuple)
        (Position, Position) ParseLine(string line)
        {
            string[] parts = line.Split([' ', ',', '='], StringSplitOptions.RemoveEmptyEntries);
            Position startpos = new Position(int.Parse(parts[1]), int.Parse(parts[2]));
            Position movement = new Position(int.Parse(parts[4]), int.Parse(parts[5]));
            return (startpos, movement);
        }
    }
}
