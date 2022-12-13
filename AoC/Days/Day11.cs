using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC.Days
{
    internal class Day11
    {
        const string filename = "C:\\Users\\CrookedSoul\\Desktop\\AoC\\AoC\\AoC\\Files\\AoC_D11.txt";
        public Day11()
        {

        }

        public void Task1()
        {
            int currMonkeyId = 0;
            List<Monkey> monkeyList = new List<Monkey>();
            if (!File.Exists(filename)) return;

            // Open the file to read from.
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string currLine = line.TrimStart();
                if (line.StartsWith("Monkey "))
                {
                    currMonkeyId = int.Parse(line.Substring("Monkey ".Count() - 1).TrimEnd(':'));
                    if (!monkeyList.Any(x => x.ID == currMonkeyId))
                    {
                        monkeyList.Add(new Monkey() { ID = currMonkeyId });
                    }
                }
                else
                {
                    var currentMonkey = monkeyList.First(x => x.ID == currMonkeyId);
                    if (currLine.StartsWith("Starting items: "))
                    {
                        currentMonkey.Items_Task1.AddRange(currLine.Substring("Starting items: ".Count() - 1).Split(',').Select(x => int.Parse(x.Trim())).ToList());
                    }
                    else if (currLine.StartsWith("Operation: new = "))
                    {
                        currentMonkey.MathExpression = currLine.Substring("Operation: new = ".Count() - 1);
                    }
                    else if (currLine.StartsWith("Test: divisible by "))
                    {
                        currentMonkey.DivisibleBy = int.Parse(currLine.Substring("Test: divisible by ".Count() - 1));
                    }
                    else if (currLine.StartsWith("If true:"))
                    {
                        currentMonkey.MonkeyIdIfTrue = int.Parse(currLine.Substring("If true: throw to monkey ".Count() - 1));
                    }
                    else if (currLine.StartsWith("If false:"))
                    {
                        currentMonkey.MonkeyIdIfFalse = int.Parse(currLine.Substring("If false: throw to monkey ".Count() - 1));
                    }
                }
            }

            for (int i = 0; i < 20; i++)
            {
                foreach (var monkey in monkeyList)
                {
                    Console.Write($"Monkey {monkey.ID}: \n");
                    for (int x = 0; x < monkey.Items_Task1.Count(); x++)
                    {
                        monkey.Items_Task1[x] = monkey.GetNewWorryLevel_Task1(monkey.Items_Task1[x]);
                        monkey.AddInspection();
                        if (monkey.IsDivisible_Task1(x))
                        {
                            Console.Write($"Item with worry level {monkey.Items_Task1[x]} is thrown to monkey {monkey.MonkeyIdIfTrue}. \n");
                            monkeyList[monkey.MonkeyIdIfTrue].Items_Task1.Add(monkey.Items_Task1[x]);
                        }
                        else
                        {
                            Console.Write($"Item with worry level {monkey.Items_Task1[x]} is thrown to monkey {monkey.MonkeyIdIfFalse}. \n");
                            monkeyList[monkey.MonkeyIdIfFalse].Items_Task1.Add(monkey.Items_Task1[x]);
                        }
                    }
                    monkey.Items_Task1 = new List<int>();
                }
            }

            monkeyList = monkeyList.OrderByDescending(x => x.GetTimesInspected()).Take(2).ToList();
            Console.WriteLine($"Result is {monkeyList.First().GetTimesInspected() * monkeyList.Last().GetTimesInspected()}");
        }

        public void Task2()
        {
            int currMonkeyId = 0;
            List<Monkey> monkeyList = new List<Monkey>();
            if (!File.Exists(filename)) return;

            // Open the file to read from.
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string currLine = line.TrimStart();
                if (line.StartsWith("Monkey "))
                {
                    currMonkeyId = int.Parse(line.Substring("Monkey ".Count() - 1).TrimEnd(':'));
                    if (!monkeyList.Any(x => x.ID == currMonkeyId))
                    {
                        monkeyList.Add(new Monkey() { ID = currMonkeyId });
                    }
                }
                else
                {
                    var currentMonkey = monkeyList.First(x => x.ID == currMonkeyId);
                    if (currLine.StartsWith("Starting items: "))
                    {
                        currentMonkey.Items_Task2.AddRange(currLine.Substring("Starting items: ".Count() - 1).Split(',').Select(x => long.Parse(x.Trim())).ToList());
                    }
                    else if (currLine.StartsWith("Operation: new = "))
                    {
                        currentMonkey.MathExpression = currLine.Substring("Operation: new = ".Count() - 1);
                    }
                    else if (currLine.StartsWith("Test: divisible by "))
                    {
                        currentMonkey.DivisibleBy = int.Parse(currLine.Substring("Test: divisible by ".Count() - 1));
                    }
                    else if (currLine.StartsWith("If true:"))
                    {
                        currentMonkey.MonkeyIdIfTrue = int.Parse(currLine.Substring("If true: throw to monkey ".Count() - 1));
                    }
                    else if (currLine.StartsWith("If false:"))
                    {
                        currentMonkey.MonkeyIdIfFalse = int.Parse(currLine.Substring("If false: throw to monkey ".Count() - 1));
                    }
                }
            }


            var product = 1;
            foreach (var monkey in monkeyList)
            {
                product *= monkey.DivisibleBy;
            }

            for (int i = 0; i < 10000; i++)
            {
                foreach (var monkey in monkeyList)
                {
                    for (int x = 0; x < monkey.Items_Task2.Count(); x++)
                    {
                        monkey.Items_Task2[x] = monkey.GetNewWorryLevel_Task2(monkey.Items_Task2[x], product);
                        // Task 1if (monkey.IsDivisible_Task1(x))
                        if (monkey.IsDivisible_Task2(x))
                        {
                            monkeyList[monkey.MonkeyIdIfTrue].Items_Task2.Add(monkey.Items_Task2[x]);
                        }
                        else
                        {
                            monkeyList[monkey.MonkeyIdIfFalse].Items_Task2.Add(monkey.Items_Task2[x]);
                        }
                    }
                    monkey.Items_Task2 = new List<long>();
                }

                if (i == 0)
                {
                    var test = 0;
                }
                if (i == 19)
                {
                    var test = 0;
                }
                if (i == 999)
                {
                    var test = 0;
                }
            }

            monkeyList = monkeyList.OrderByDescending(x => x.GetTimesInspected()).Take(2).ToList();
            Console.WriteLine($"Result is {monkeyList.First().GetTimesInspected() * monkeyList.Last().GetTimesInspected()}");
        }


    }
    public class Monkey
    {
        public int ID { get; set; }
        public List<int> Items_Task1 { get; set; } = new List<int>();
        public List<long> Items_Task2 { get; set; } = new List<long>();
        public string MathExpression { get; set; }
        public int DivisibleBy { get; set; }
        public decimal DivisibleBy_Task2 => decimal.Parse(DivisibleBy.ToString());
        public int MonkeyIdIfTrue { get; set; }
        public int MonkeyIdIfFalse { get; set; }

        public int GetNewWorryLevel_Task1(int old)
        {
            Console.Write($"Monkey inspects an item with a worry level of {old}. \n");
            var newWorryLevel = (int)new System.Data.DataTable().Compute(MathExpression.Replace("old", old.ToString()), "");
            Console.Write($"MonkeyWorry level increases to {newWorryLevel}, by using {MathExpression.Replace("old", old.ToString())}. \n");
            // He gets bored we divide by 3

            int result = newWorryLevel / 3;
            Console.Write($"Monkey gets bored with item. Worry level is divided by 3 to {result}. \n");
            return result;
        }
        public long GetNewWorryLevel_Task2(long old, Int64 product)
        {
            AddInspection();

            long newWorryLevel = old;
            Regex isTimes = new Regex(@"old \* [0-999]");
            Regex isAddition = new Regex(@"old \+ [0-999]");

            if (MathExpression.Contains(" * old"))
            {
                newWorryLevel = old * old;
            }
            else if (isTimes.Match(MathExpression).Success)
            {
                newWorryLevel = old * int.Parse(MathExpression.Split(' ').Last().Trim());
            }
            else if (isAddition.Match(MathExpression).Success)
            {
                newWorryLevel = old + int.Parse(MathExpression.Split(' ').Last().Trim());
            }

            newWorryLevel %= product;
            return newWorryLevel;
        }

        public bool IsDivisible_Task1(int index)
        {
            return Items_Task1[index] % DivisibleBy == 0;
        }
        public bool IsDivisible_Task2(int index)
        {
            return Items_Task2[index] % DivisibleBy == 0;
        }

        private long TimesInspected = 0;
        public void AddInspection()
        {
            TimesInspected += long.Parse(1.ToString());
        }
        public long GetTimesInspected()
        {
            return TimesInspected;
        }
    }

}
