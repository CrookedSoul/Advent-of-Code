using System;
using System.Text;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace AoC.Days
{
    public class Day12
    {
        const string filename = "C:\\Users\\CrookedSoul\\Desktop\\AoC\\AoC\\AoC\\Files\\AoC_D12.txt";

        public Day12()
        {
        }

        public void Task1()
        {
            List<Tile> Visited = new List<Tile>();
            List<Tile> NotVisited = new List<Tile>();

            int[] startingPosition = new int[2];
            int[] targetPosition = new int[2];
            var lines = File.ReadAllLines(filename);
            var widthX = lines.First().Length;
            var heightY = lines.Count();
            Tile[,] Grid = new Tile[widthX, heightY];
            int height = 0;
            foreach (var line in lines)
            {
                int width = 0;
                foreach (var character in line)
                {
                    if (character.Equals('S'))
                    {
                        startingPosition[0] = width;
                        startingPosition[1] = height;
                        Grid[width, height] = new Tile(width, height, (int)Enum.Parse<ElevationEnum>(new ReadOnlySpan<char>('a')));
                    }
                    else if (character.Equals('E'))
                    {
                        targetPosition[0] = width;
                        targetPosition[1] = height;
                        Grid[width, height] = new Tile(width, height, (int)Enum.Parse<ElevationEnum>(new ReadOnlySpan<char>('z')));
                    }
                    else
                    {
                        Grid[width, height] = new Tile(width, height, (int)Enum.Parse<ElevationEnum>(character.ToString().ToLower()));
                    }
                    width++;
                }
                height++;
            }
            NotVisited.Add(Grid[startingPosition[0], startingPosition[1]]);

            while (NotVisited.Count() > 0 || !Visited.Any(tile => tile.X == targetPosition.First() && tile.Y == targetPosition.Last()))
            {
                Tile currentTile = NotVisited.First();

                // up
                if (currentTile.Y > 0 &&
                    (!NotVisited.Contains(Grid[currentTile.X, currentTile.Y - 1])) &&
                    (!Visited.Contains(Grid[currentTile.X, currentTile.Y - 1])) &&
                    Grid[currentTile.X, currentTile.Y - 1].Elevation - currentTile.Elevation <= 1)
                {
                    Grid[currentTile.X, currentTile.Y - 1].SetParent(Grid[currentTile.X, currentTile.Y]);
                    NotVisited.Add(Grid[currentTile.X, currentTile.Y - 1]);
                }
                // down
                if (currentTile.Y + 1 < heightY &&
                    (!NotVisited.Contains(Grid[currentTile.X, currentTile.Y + 1])) &&
                    (!Visited.Contains(Grid[currentTile.X, currentTile.Y + 1])) &&
                    Grid[currentTile.X, currentTile.Y + 1].Elevation - currentTile.Elevation <= 1)
                {
                    Grid[currentTile.X, currentTile.Y + 1].SetParent(Grid[currentTile.X, currentTile.Y]);
                    NotVisited.Add(Grid[currentTile.X, currentTile.Y + 1]);
                }
                // left
                if (currentTile.X > 0 &&
                    (!NotVisited.Contains(Grid[currentTile.X - 1, currentTile.Y])) &&
                    (!Visited.Contains(Grid[currentTile.X - 1, currentTile.Y])) &&
                    Grid[currentTile.X - 1, currentTile.Y].Elevation - currentTile.Elevation <= 1)
                {
                    Grid[currentTile.X - 1, currentTile.Y].SetParent(Grid[currentTile.X, currentTile.Y]);
                    NotVisited.Add(Grid[currentTile.X - 1, currentTile.Y]);
                }
                // right
                if (currentTile.X + 1 < widthX &&
                    (!NotVisited.Contains(Grid[currentTile.X + 1, currentTile.Y])) &&
                    (!Visited.Contains(Grid[currentTile.X + 1, currentTile.Y])) &&
                    Grid[currentTile.X + 1, currentTile.Y].Elevation - currentTile.Elevation <= 1)
                {
                    Grid[currentTile.X + 1, currentTile.Y].SetParent(Grid[currentTile.X, currentTile.Y]);
                    NotVisited.Add(Grid[currentTile.X + 1, currentTile.Y]);
                }

                Visited.Add(currentTile);
                NotVisited.RemoveAt(NotVisited.IndexOf(currentTile));
            }

            List<Tile> SimulatedPath = new List<Tile>();
            bool pathComplete = false;
            // We go from target back to start position where parent is null hopefully if i did it right... oh god;
            Tile tileToCheck = Grid[targetPosition.First(), targetPosition.Last()];
            while (!pathComplete)
            {
                if (tileToCheck.Parent == null)
                {
                    //hopefull
                    pathComplete = true;
                }
                else
                {
                    SimulatedPath.Add(tileToCheck.Parent);
                    tileToCheck = tileToCheck.Parent;
                }
            }

            Console.WriteLine(SimulatedPath.Count());
        }

        // Im an idiot but oh well brute force it is
        public void Task2()
        {
            List<Tile> Visited = new List<Tile>();
            List<Tile> NotVisited = new List<Tile>();

            List<int[]> startingPositions = new List<int[]>();
            int[] targetPosition = new int[2];
            var lines = File.ReadAllLines(filename);
            var widthX = lines.First().Length;
            var heightY = lines.Count();
            Tile[,] Grid = new Tile[widthX, heightY];

            List<int> trips = new List<int>();
            int height = 0;
            foreach (var line in lines)
            {
                int width = 0;
                foreach (var character in line)
                {
                    if (character.Equals('a') || character.Equals('S'))
                    {
                        startingPositions.Add(new int[2] { width, height });
                    }
                    width++;
                }
                height++;
            }

            foreach (int[] startingPosition in startingPositions)
            {
                Grid = new Tile[widthX, heightY];
                Visited = new List<Tile>();
                NotVisited = new List<Tile>();

                height = 0;
                foreach (var line in lines)
                {
                    int width = 0;
                    foreach (var character in line)
                    {
                        if (character.Equals('a') || character.Equals('S'))
                        {
                            Grid[width, height] = new Tile(width, height, (int)Enum.Parse<ElevationEnum>(new ReadOnlySpan<char>('a')));
                        }
                        else if (character.Equals('E'))
                        {
                            targetPosition[0] = width;
                            targetPosition[1] = height;
                            Grid[width, height] = new Tile(width, height, (int)Enum.Parse<ElevationEnum>(new ReadOnlySpan<char>('z')));
                        }
                        else
                        {
                            Grid[width, height] = new Tile(width, height, (int)Enum.Parse<ElevationEnum>(character.ToString().ToLower()));
                        }
                        width++;
                    }
                    height++;
                }

                NotVisited.Add(Grid[startingPosition[0], startingPosition[1]]);

                while (true)
                {
                    // For some weird reason putting this negative in while did not work....
                    if (NotVisited.Count == 0 || Visited.Any(tile => tile.X == targetPosition.First() && tile.Y == targetPosition.Last()))
                    {
                        break;
                    }
                    Tile currentTile = NotVisited.First();

                    // up
                    if (currentTile.Y > 0 &&
                        (!NotVisited.Contains(Grid[currentTile.X, currentTile.Y - 1])) &&
                        (!Visited.Contains(Grid[currentTile.X, currentTile.Y - 1])) &&
                        Grid[currentTile.X, currentTile.Y - 1].Elevation - currentTile.Elevation <= 1)
                    {
                        Grid[currentTile.X, currentTile.Y - 1].SetParent(Grid[currentTile.X, currentTile.Y]);
                        NotVisited.Add(Grid[currentTile.X, currentTile.Y - 1]);
                    }
                    // down
                    if (currentTile.Y + 1 < heightY &&
                        (!NotVisited.Contains(Grid[currentTile.X, currentTile.Y + 1])) &&
                        (!Visited.Contains(Grid[currentTile.X, currentTile.Y + 1])) &&
                        Grid[currentTile.X, currentTile.Y + 1].Elevation - currentTile.Elevation <= 1)
                    {
                        Grid[currentTile.X, currentTile.Y + 1].SetParent(Grid[currentTile.X, currentTile.Y]);
                        NotVisited.Add(Grid[currentTile.X, currentTile.Y + 1]);
                    }
                    // left
                    if (currentTile.X > 0 &&
                        (!NotVisited.Contains(Grid[currentTile.X - 1, currentTile.Y])) &&
                        (!Visited.Contains(Grid[currentTile.X - 1, currentTile.Y])) &&
                        Grid[currentTile.X - 1, currentTile.Y].Elevation - currentTile.Elevation <= 1)
                    {
                        Grid[currentTile.X - 1, currentTile.Y].SetParent(Grid[currentTile.X, currentTile.Y]);
                        NotVisited.Add(Grid[currentTile.X - 1, currentTile.Y]);
                    }
                    // right
                    if (currentTile.X + 1 < widthX &&
                        (!NotVisited.Contains(Grid[currentTile.X + 1, currentTile.Y])) &&
                        (!Visited.Contains(Grid[currentTile.X + 1, currentTile.Y])) &&
                        Grid[currentTile.X + 1, currentTile.Y].Elevation - currentTile.Elevation <= 1)
                    {
                        Grid[currentTile.X + 1, currentTile.Y].SetParent(Grid[currentTile.X, currentTile.Y]);
                        NotVisited.Add(Grid[currentTile.X + 1, currentTile.Y]);
                    }

                    Visited.Add(currentTile);
                    NotVisited.RemoveAt(NotVisited.IndexOf(currentTile));
                }

                List<Tile> SimulatedPath = new List<Tile>();
                bool pathComplete = false;
                // We go from target back to start position where parent is null hopefully if i did it right... oh god;
                Tile tileToCheck = Grid[targetPosition.First(), targetPosition.Last()];
                while (!pathComplete)
                {
                    if (tileToCheck.Parent == null)
                    {
                        //hopefull
                        pathComplete = true;
                    }
                    else
                    {
                        SimulatedPath.Add(tileToCheck.Parent);
                        tileToCheck = tileToCheck.Parent;
                    }
                }

                if (SimulatedPath.Count() != 0)
                {
                    trips.Add(SimulatedPath.Count());
                }
            }

            Console.WriteLine(trips.Min());
        }
    }

    class Tile
    {
        public int X;
        public int Y;
        public int Elevation;
        public Tile Parent;

        public Tile(int x, int y, int elev)
        {
            X = x;
            Y = y;
            Elevation = elev;
        }

        public void SetParent(Tile parent)
        {
            Parent = parent;
        }
    }

    enum ElevationEnum
    {
        a = 1,
        b = 2,
        c = 3,
        d = 4,
        e = 5,
        f = 6,
        g = 7,
        h = 8,
        i = 9,
        j = 10,
        k = 11,
        l = 12,
        m = 13,
        n = 14,
        o = 15,
        p = 16,
        q = 17,
        r = 18,
        s = 19,
        t = 20,
        u = 21,
        v = 22,
        w = 23,
        x = 24,
        y = 25,
        z = 26,
        end = 999,
    }
}