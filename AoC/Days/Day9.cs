using System;
using System.ComponentModel;
using System.Text;

namespace AoC.Days
{
    public class Day9
    {
        const string filename = "/Users/ratkoristic/Desktop/AoC/AoC/AoC/Files/AoC_D9.txt";
        const string output = "/Users/ratkoristic/Desktop/AoC/AoC/AoC/Files/RopeMovement.txt";
        public Day9()
        {
        }

        public void Task1()
        {
            Rope Rope = new Rope(0, 0, 1, 2);

            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string currentLine;
                while ((currentLine = streamReader.ReadLine()) != null)
                {
                    Rope.Move(currentLine);
                }

                Console.WriteLine(Rope.BodyParts.Last().Visited.Count());
            }
        }

        public void Task2()
        {
            Rope Rope = new Rope(0, 0, 1, 10);

            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string currentLine;
                while ((currentLine = streamReader.ReadLine()) != null)
                {
                    Rope.Move(currentLine);
                }

                Console.WriteLine(Rope.BodyParts.Last().Visited.Count());
                /*System.IO.File.WriteAllLines(output, Rope.Lines);
                Console.WriteLine("Lines written to file successfully.");*/
            }
        }
    }
    public class BodyPart
    {
        public HashSet<string> Visited { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public BodyPart(int x, int y, bool isTail)
        {
            X = x;
            Y = y;
            Visited = new HashSet<string>();
            Visited.Add($"{X} {Y}");
        }
    }
    public class Rope : BodyPart
    {
        public char[,] grid = new char[50, 50];
        public List<string> Lines = new List<string>();
        public List<BodyPart> BodyParts { get; set; } = new List<BodyPart>();

        public Rope(int x, int y, int currentPart, int numberOfParts) : base(x, y, false)
        {
            X = x;
            Y = y;
            for (int i = 1; i <= numberOfParts; i++)
            {
                if (currentPart != numberOfParts)
                {
                    BodyParts.Add(new BodyPart(x, y, i == numberOfParts));
                }
            }
        }

        public void Move(string command)
        {
            string[] parts = command.Split(' ');

            for (int i = 0; i < int.Parse(parts.Last()); i++)
            {
                if (parts[0].Equals("r", StringComparison.InvariantCultureIgnoreCase))
                {
                    BodyParts[0].Y += 1;
                }
                else if (parts[0].Equals("l", StringComparison.InvariantCultureIgnoreCase))
                {
                    BodyParts[0].Y -= 1;
                }
                else if (parts[0].Equals("u", StringComparison.InvariantCultureIgnoreCase))
                {
                    BodyParts[0].X -= 1;
                }
                else if (parts[0].Equals("d", StringComparison.InvariantCultureIgnoreCase))
                {
                    BodyParts[0].X += 1;
                }
                for (int x = 1; x < BodyParts.Count(); x++)
                {
                    FollowPart(BodyParts[x - 1], BodyParts[x]);
                }
                /*
                for (int gridY = 0; gridY < 50; gridY++)
                {
                    for (int gridX = 0; gridX < 50; gridX++)
                    {
                        if (BodyParts.Any(x => x.X == gridX - 25 && x.Y == gridY - 25))
                        {
                            var part = BodyParts.FirstOrDefault(x => x.X == gridX - 25 && x.Y == gridY - 25);
                            var index = BodyParts.IndexOf(part);
                            grid[gridX, gridY] = index.ToString()[0];
                        }
                        else
                        {
                            grid[gridX, gridY] = '.';
                        }
                    }
                }
                for (int r = 0; r < 50; r++)
                {
                    string line = "";
                    for (int c = 0; c < 50; c++)
                    {
                        line += grid[r, c].ToString();
                    }
                    Lines.Add(line);
                }
                Lines.Add("--------------------------------------------------------------------------------");*/
            }
        }

        public void FollowPart(BodyPart previousPart, BodyPart currentPart)
        {
            // previously i made a mistake of ust moving one axis as it should while the other was
            // just other coordinate = previous part which is bad since if its misaligned i need to check both
            // And also need to move current part by 1/-1 and not previous part +1/-1
            if (Math.Abs(previousPart.Y - currentPart.Y) > 1 || Math.Abs(previousPart.X - currentPart.X) > 1)
            {
                var stepx = previousPart.X - currentPart.X;
                var stepy = previousPart.Y - currentPart.Y;
                if (stepx < 0)
                {
                    currentPart.X += -1;
                }
                else if (stepx > 0)
                {
                    currentPart.X += 1;
                }
                if (stepy < 0)
                {
                    currentPart.Y += -1;
                }
                else if (stepy > 0)
                {
                    currentPart.Y += 1;
                }
            }
            currentPart.Visited.Add($"{currentPart.X} {currentPart.Y}");
        }
    }


    // Didnt work with Task2 
    /*
    public class PartTask1
    {
        public HashSet<string> Visited { get; set; }
        public PartTask1 Tail { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public PartTask1(int x, int y, bool isTail)
        {
            X = x;
            Y = y;
            if (!isTail)
            {
                Tail = new PartTask1(x, y, true);
            }
            else
            {
                Visited = new HashSet<string>();
                Visited.Add($"{X} {Y}");
            }
        }

        public void Move(string command)
        {
            string[] parts = command.Split(' ');

            for (int i = 0; i < int.Parse(parts.Last()); i++)
            {
                if (parts[0].Equals("r", StringComparison.InvariantCultureIgnoreCase))
                {
                    Y += 1;
                }
                else if (parts[0].Equals("l", StringComparison.InvariantCultureIgnoreCase))
                {
                    Y -= 1;
                }
                else if (parts[0].Equals("u", StringComparison.InvariantCultureIgnoreCase))
                {
                    X -= 1;
                }
                else if (parts[0].Equals("d", StringComparison.InvariantCultureIgnoreCase))
                {
                    X += 1;
                }
                MoveTail();
            }
        }

        public void MoveTail()
        {
            // This one did not work for multiples, because the other coordinate also needed to move instead of just sticking
            if (Math.Abs(X - Tail.X) > 1)
            {
                if (Tail.X > X)
                {
                    Tail.X = X + 1;
                }
                else
                {
                    Tail.X = X - 1;
                }
                Tail.Y = Y;
            }
            else if (Math.Abs(Y - Tail.Y) > 1)
            {
                if (Tail.Y > Y)
                {
                    Tail.Y = Y + 1;
                }
                else
                {
                    Tail.Y = Y - 1;
                }
                Tail.X = X;
            }

            Tail.Visited.Add($"{Tail.X} {Tail.Y}");
        }
    }*/
}

