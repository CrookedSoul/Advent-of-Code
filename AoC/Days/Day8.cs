using System;
using System.Text;

namespace AoC.Days
{
    public class Day8
    {
        const string filename = "/Users/ratkoristic/Desktop/AoC/AoC/AoC/Files/AoC_D8.txt";
        public Day8()
        {
        }

        public void Task1()
        {
            int result = 0;
            int innerLayer = 0;
            List<string> lines = new List<string>();
            int width = 0;
            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string currentLine;
                while ((currentLine = streamReader.ReadLine()) != null)
                {
                    lines.Add(currentLine);
                    width = currentLine.Length;
                }
            }

            int height = lines.Count();
            int outerLayer = height * 2 + (width - 2) * 2;

            // we only need the inner layer so we ignore the outer layer
            for (int x = 1; x < height - 1; x++)
            {
                for (int y = 1; y < width - 1; y++)
                {
                    bool[] isVisible = new bool[4] { true, true, true, true };
                    int middleTree = int.Parse(lines[x][y].ToString());
                    for (int up = x + 1; up < height; up++)
                    {
                        int treeUp = int.Parse(lines[up][y].ToString());
                        if (middleTree <= treeUp)
                        {
                            isVisible[0] = false;
                        }
                    }
                    for (int down = x - 1; down >= 0; down--)
                    {
                        int treeDown = int.Parse(lines[down][y].ToString());
                        if (middleTree <= treeDown)
                        {
                            isVisible[1] = false;
                        }
                    }
                    for (int right = y + 1; right < width; right++)
                    {
                        int treeRight = int.Parse(lines[x][right].ToString());
                        if (middleTree <= treeRight)
                        {
                            isVisible[2] = false;
                        }
                    }
                    for (int left = y - 1; left >= 0; left--)
                    {
                        int treeLeft = int.Parse(lines[x][left].ToString());
                        if (middleTree <= treeLeft)
                        {
                            isVisible[3] = false;
                        }
                    }

                    if (isVisible.Any(x => x))
                    {
                        innerLayer += 1;
                    }
                }
            }
            result = outerLayer + innerLayer;
            Console.WriteLine($"Result is {result}");
        }

        public void Task2()
        {
            int result = 0;
            List<int> ScenicScores = new List<int>();
            List<string> lines = new List<string>();
            int width = 0;
            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string currentLine;
                while ((currentLine = streamReader.ReadLine()) != null)
                {
                    lines.Add(currentLine);
                    width = currentLine.Length;
                }
            }

            int height = lines.Count();
            int outerLayer = height * 2 + (width - 2) * 2;

            // we only need the inner layer so we ignore the outer layer
            for (int x = 1; x < height - 1; x++)
            {
                for (int y = 1; y < width - 1; y++)
                {
                    bool[] isVisible = new bool[4] { true, true, true, true };
                    int[] scenicScore = new int[4] { 0, 0, 0, 0 };
                    int middleTree = int.Parse(lines[x][y].ToString());
                    for (int up = x + 1; up < height; up++)
                    {
                        if (!isVisible[0]) continue;
                        int treeUp = int.Parse(lines[up][y].ToString());
                        if (middleTree <= treeUp)
                        {
                            isVisible[0] = false;
                            scenicScore[0] += 1;
                        }
                        else
                        {
                            scenicScore[0] += 1;
                        }
                    }
                    for (int down = x - 1; down >= 0; down--)
                    {
                        if (!isVisible[1]) continue;
                        int treeDown = int.Parse(lines[down][y].ToString());
                        if (middleTree <= treeDown)
                        {
                            isVisible[1] = false;
                            scenicScore[1] += 1;
                        }
                        else
                        {
                            scenicScore[1] += 1;
                        }
                    }
                    for (int right = y + 1; right < width; right++)
                    {
                        if (!isVisible[2]) continue;
                        int treeRight = int.Parse(lines[x][right].ToString());
                        if (middleTree <= treeRight)
                        {
                            isVisible[2] = false;
                            scenicScore[2] += 1;
                        }
                        else
                        {
                            scenicScore[2] += 1;
                        }
                    }
                    for (int left = y - 1; left >= 0; left--)
                    {
                        if (!isVisible[3]) continue;
                        int treeLeft = int.Parse(lines[x][left].ToString());
                        if (middleTree <= treeLeft)
                        {
                            isVisible[3] = false;
                            scenicScore[3] += 1;
                        }
                        else
                        {
                            scenicScore[3] += 1;
                        }
                    }

                    ScenicScores.Add(scenicScore[0] * scenicScore[1] * scenicScore[2] * scenicScore[3]);
                }
            }
            Console.WriteLine($"Result is {ScenicScores.Max()}");
        }
    }
}

