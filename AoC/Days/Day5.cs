using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AoC.Days
{
    public class Day5
    {
        const string filename = "C:\\Users\\CrookedSoul\\Desktop\\AoC\\AoC\\AoC\\Files\\AoC_D5.txt";
        const string stackPattern = "[1] [2] [3] [4] [5] [6] [7] [8] [9]";
        public Day5()
        {
        }

        public void Task1()
        {
            int line = 0;
            bool reversed = false;
            List<CrateStack> stacks = CrateStack.GetInitialCrateStacksTask1(9);
            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string currentLine;
                while ((currentLine = streamReader.ReadLine()) != null)
                {
                    if (currentLine.Contains('[') && currentLine.Contains(']'))
                    {
                        List<string> crateStacks = new List<string>();
                        for (int i = 1; i <= 9; i++)
                        {
                            int index = stackPattern.IndexOf(i.ToString());
                            string crate = currentLine[index].ToString();
                            if (!string.IsNullOrWhiteSpace(crate))
                            {
                                stacks[i - 1].Crates.Add(crate);
                            }
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(currentLine) && !reversed)
                    {
                        for (int i = 1; i <= 9; i++)
                        {
                            stacks[i - 1].Crates.Reverse();
                        }
                        reversed = true;
                    }
                    else if (currentLine.Contains("move", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string[] parts = currentLine.Split(' ');
                        List<int> intParts = new List<int>();
                        foreach (string part in parts)
                        {
                            if (int.TryParse(part, out int result))
                            {
                                intParts.Add(result);
                            };
                        }

                        var stacksToMoveFrom = stacks[intParts[1] - 1].Crates;
                        var stacksToMoveTo = stacks[intParts[2] - 1].Crates;
                        for (int i = 0; i < intParts[0]; i++)
                        {
                            string crateToMove = stacksToMoveFrom.Last();
                            // remove doesnt seem to work so hells yeah for wasting my time on this shit
                            stacksToMoveFrom.RemoveAt(stacksToMoveFrom.Count - 1);
                            stacksToMoveTo.Add(crateToMove);
                        }
                        line++;
                    }
                }
            }
            var res = stacks.Select(x => x.Crates).Select(x => x.Last());
            Console.WriteLine($"Result is {string.Join("", res.Select(x => x))} and index is {line}");
        }
        public void Task2()
        {
            int line = 0;
            bool reversed = false;
            List<CrateStack> stacks = CrateStack.GetInitialCrateStacksTask1(9);
            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string currentLine;
                while ((currentLine = streamReader.ReadLine()) != null)
                {
                    if (currentLine.Contains('[') && currentLine.Contains(']'))
                    {
                        List<string> crateStacks = new List<string>();
                        for (int i = 1; i <= 9; i++)
                        {
                            int index = stackPattern.IndexOf(i.ToString());
                            string crate = currentLine[index].ToString();
                            if (!string.IsNullOrWhiteSpace(crate))
                            {
                                stacks[i - 1].Crates.Add(crate);
                            }
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(currentLine) && !reversed)
                    {
                        for (int i = 1; i <= 9; i++)
                        {
                            stacks[i - 1].Crates.Reverse();
                        }
                        reversed = true;
                    }
                    else if (currentLine.Contains("move", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string[] parts = currentLine.Split(' ');
                        List<int> intParts = new List<int>();
                        foreach (string part in parts)
                        {
                            if (int.TryParse(part, out int result))
                            {
                                intParts.Add(result);
                            };
                        }

                        var stacksToMoveFrom = stacks[intParts[1] - 1].Crates;
                        var stacksToMoveTo = stacks[intParts[2] - 1].Crates;
                        List<string> cratesToMove = new List<string>();
                        for (int i = 0; i < intParts[0]; i++)
                        {
                            cratesToMove.Add(stacksToMoveFrom.Last());
                            // remove doesnt seem to work so hells yeah for wasting my time on this shit
                            stacksToMoveFrom.RemoveAt(stacksToMoveFrom.Count - 1);
                        }
                        cratesToMove.Reverse();
                        stacksToMoveTo.AddRange(cratesToMove);
                        line++;
                    }
                }
            }
            var res = stacks.Select(x => x.Crates).Select(x => x.Last());
            Console.WriteLine($"Result is {string.Join("", res.Select(x => x))} and index is {line}");
        }
    }
}

public class CrateStack
{
    public List<string> Crates { get; set; }
    public static List<CrateStack> GetInitialCrateStacksTask1(int count)
    {
        List<CrateStack> crates = new List<CrateStack>();
        for (int i = 1; i <= count; i++)
        {
            crates.Add(new CrateStack() { Crates = new List<string>() });
        }
        return crates;
    }
}
