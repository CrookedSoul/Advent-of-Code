using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days
{
    internal class Day10
    {
        const string filename = "C:\\Users\\CrookedSoul\\Desktop\\AoC\\AoC\\AoC\\Files\\AoC_D10.txt";
        public Day10()
        {

        }

        public void Task1()
        {
            List<int> neededCycles = new List<int>();
            int cycle = 0;
            int valueX = 1;

            // This text is added only once to the file.
            if (!File.Exists(filename)) return;

            // Open the file to read from.
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                if (line.StartsWith("addx", StringComparison.InvariantCultureIgnoreCase))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        cycle++;

                        CheckCycle(cycle, valueX, neededCycles);
                    }

                    var number = int.Parse(line.Split(' ').Last().Replace("-", ""));
                    if (line.Contains("-"))
                    {
                        valueX -= number;
                    }
                    else
                    {
                        valueX += number;
                    }
                }
                else if (line.Equals("noop", StringComparison.InvariantCultureIgnoreCase))
                {
                    cycle++;

                    CheckCycle(cycle, valueX, neededCycles);
                }
            }

            Console.Write($"Result is {neededCycles.Sum()}");
        }

        int currWidth = 0;
        int currHeight = 0;
        public void Task2()
        {
            int[] pixelPosition = new int[3] { 0, 1, 2 };
            string[,] crt = new string[6, 40];
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 40; y++)
                {
                    crt[x, y] = ".";
                }
            }
            List<int> neededCycles = new List<int>();
            int cycle = 0;
            int valueX = 1;

            if (!File.Exists(filename)) return;

            // Open the file to read from.
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                if (line.StartsWith("addx", StringComparison.InvariantCultureIgnoreCase))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        DrawSprite(crt, pixelPosition);
                        cycle++;
                        CheckCycle(cycle, valueX, neededCycles);
                    }

                    var number = int.Parse(line.Split(' ').Last().Replace("-", ""));
                    if (line.Contains("-"))
                    {
                        valueX -= number;
                    }
                    else
                    {
                        valueX += number;
                    }

                    pixelPosition[0] = valueX - 1;
                    pixelPosition[1] = valueX;
                    pixelPosition[2] = valueX + 1;
                }
                else if (line.Equals("noop", StringComparison.InvariantCultureIgnoreCase))
                {
                    DrawSprite(crt, pixelPosition);
                    cycle++;
                    CheckCycle(cycle, valueX, neededCycles);
                }
            }

            List<string> lineroni = new List<string>();
            for (int x = 0; x < 6; x++)
            {
                string line = "";
                for (int y = 0; y < 40; y++)
                {
                    line += crt[x, y];
                }
                lineroni.Add(line);
                Console.WriteLine(line);
            }
        }

        private void CheckCycle(int cycle, int valueX, List<int> neededCycles)
        {
            if (cycle == 20 || cycle == 60 || cycle == 100 || cycle == 140 || cycle == 180 || cycle == 220)
            {
                neededCycles.Add(cycle * valueX);
            }
        }

        private void DrawSprite(string[,] crt, int[] pixelPosition)
        {
            if (currWidth >= 40)
            {
                currWidth = 0;
                currHeight += 1;
            }

            if (pixelPosition.Contains(currWidth))
            {
                crt[currHeight, currWidth] = "#";
            }

            currWidth++;
        }
    }
}
