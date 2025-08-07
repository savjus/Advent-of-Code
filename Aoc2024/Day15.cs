namespace Aoc2024
{
    class Day15 : IAocDay
    {
        List<Position> boxes = new List<Position>();
        List<DoubleNode> boxesBig = new List<DoubleNode>();
        List<Position> walls = new List<Position>();
        List<DoubleNode> wallsBig = new List<DoubleNode>();
        Position robot = new Position(0,0);
        List<char> moves = new List<char>();
        Dictionary<char, Position> directions = new Dictionary<char, Position>
        {
            ['<'] = new Position(-1,0),
            ['>'] = new Position(1,0),
            ['v'] = new Position(0,1),
            ['^'] = new Position(0,-1)
        };
        string input;
        public Day15(string input)
        {
            this.input = input;
        }

        public long Part1()
        {
            ParseInputPart1(input);
            foreach (char move in moves)
            {
                Position moveDir = directions[move];

                Position robotMoved = Move(robot, moveDir);
                if (walls.Contains(robotMoved))
                {
                    continue;
                }
                if (boxes.Contains(robotMoved))
                {
                    Position nextMove = Move(robotMoved, moveDir);
                    while (boxes.Contains(nextMove))
                    {
                        nextMove = Move(nextMove, moveDir);
                    }
                    if (walls.Contains(nextMove))
                    {
                        continue;
                    }
                    boxes.Remove(robotMoved);
                    boxes.Add(nextMove);
                }
                robot = robotMoved;
            }
            long result = countEndScore(boxes);
            return result;
        }
        public long Part2()
        {
            ParseInputPart2(input);
            foreach (char move in moves)
            {
                Position moveDir = directions[move];
                Position robotMoved = Move(robot, moveDir);

                if (wallsBig.Any(w => w.Contains(robotMoved)))
                {
                    continue;
                }

                DoubleNode? nextBox = boxesBig.FirstOrDefault(b => b.Contains(robotMoved));
                bool canMove = true;

                if (nextBox != null)
                {
                    var boxesToMove = new HashSet<DoubleNode> { nextBox };
                    var queue = new Queue<DoubleNode>();
                    queue.Enqueue(nextBox);

                    while (queue.Count > 0 && canMove)
                    {
                        DoubleNode box = queue.Dequeue();
                        Position leftbox = Move(box.left, moveDir);
                        Position rightbox = Move(box.right, moveDir);

                        if (wallsBig.Any(w => w.Contains(leftbox)) || wallsBig.Any(w => w.Contains(rightbox)))
                        {
                            canMove = false;
                            break;
                        }
                        DoubleNode? nextLeftBox = boxesBig.FirstOrDefault(b => b.Contains(leftbox) && !boxesToMove.Contains(b));
                        if (nextLeftBox != null && boxesToMove.Add(nextLeftBox))
                        {
                            queue.Enqueue(nextLeftBox);
                        }

                        DoubleNode? nextRightBox = boxesBig.FirstOrDefault(b => b.Contains(rightbox) && !boxesToMove.Contains(b));
                        if (nextRightBox != null && boxesToMove.Add(nextRightBox))
                        {
                            queue.Enqueue(nextRightBox);
                        }
                    }
                    if (canMove)
                    {
                        foreach (DoubleNode box in boxesToMove)
                        {
                            boxesBig.Remove(box);
                            boxesBig.Add(new DoubleNode(Move(box.left, moveDir), Move(box.right, moveDir)));
                        }
                        robot = robotMoved;
                    }
                }
                else
                {
                    robot = robotMoved;
                }

            }
            long result = countEndScore(boxesBig.Select(b => b.left).ToList());
            return result;
        }
        
        private Position Move(Position robot, Position direction)
        {
            return new Position(robot.X + direction.X, robot.Y + direction.Y);
        }
        private long countEndScore(List<Position> boxes)
        {
            long result = 0;
            foreach (Position box in boxes)
            {
                result += box.Y * 100 + box.X;
            }
            return result;
        }
        private void ParseInputPart2(string input)
        {
            boxesBig.Clear();
            wallsBig.Clear();
            moves.Clear();

            string[] lines = input.Split('\n');
            int i;
            for (i = 0; lines[i] != string.Empty; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '#')
                    {
                        DoubleNode wall = new DoubleNode(new Position(j * 2, i), new Position(j * 2 + 1, i));
                        wallsBig.Add(wall);
                    }
                    else if (lines[i][j] == 'O')
                    {
                        DoubleNode box = new DoubleNode(new Position(j * 2, i), new Position(j * 2 + 1, i));
                        boxesBig.Add(box);
                    }
                    else if (lines[i][j] == '@')
                    {
                        robot = new Position(j*2, i);
                    }
                }
            }
            i++;
            for (int j = i; j < lines.Length; j++)
            {
                moves.AddRange(lines[j].ToCharArray().ToList());
            }
        }
        private void ParseInputPart1(string input)
        {
            boxes.Clear();
            walls.Clear();
            moves.Clear();

            string[] lines = input.Split('\n');
            int i;
            for (i = 0; lines[i] != string.Empty; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '#')
                    {
                        walls.Add(new Position(j, i));
                    }
                    else if (lines[i][j] == 'O')
                    {
                        boxes.Add(new Position(j, i));
                        continue;
                    }
                    else if (lines[i][j] == '@')
                    {
                        robot = new Position(j, i);
                        continue;
                    }
                }
            }
            i++;
            for (int j = i; j < lines.Length; j++)
            {
                moves.AddRange(lines[j].ToCharArray().ToList());
            }
        }
    }
}
