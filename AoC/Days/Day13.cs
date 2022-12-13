using System;

namespace AoC.Days
{
    public class Day13
    {
        List<KeyValuePair<Packet, Packet>> Pairs = new List<KeyValuePair<Packet, Packet>>();
        const string filename = "C:\\Users\\CrookedSoul\\Desktop\\AoC\\AoC\\AoC\\Files\\AoC_D13.txt";
        public Day13()
        {
        }

        public void BothTasks()
        {
            var FirstPart = new Packet();
            var SecondPart = new Packet();
            var lines = File.ReadAllLines(filename);
            int index = 1;
            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    if (index % 2 == 0)
                    {
                        SecondPart = ParseLists(line, true);
                    }
                    else
                    {
                        FirstPart = ParseLists(line, true);
                    }
                    index++;
                }
                if (index > 2)
                {
                    index = 1;
                    var kvp = new KeyValuePair<Packet, Packet>(FirstPart, SecondPart);
                    Pairs.Add(kvp);
                    FirstPart = new Packet();
                    SecondPart = new Packet();
                }
            }

            List<Packet> goodPackets = new List<Packet>();
            List<int> verticesInRightOrder = new List<int>();
            for (int i = 1; i <= Pairs.Count; i++)
            {
                if (GetVerticesInRightOrder(Pairs[i - 1].Key, Pairs[i - 1].Value) < 0)
                {
                    verticesInRightOrder.Add(i);
                }
                goodPackets.Add(Pairs[i - 1].Key);
                goodPackets.Add(Pairs[i - 1].Value);
            }
            Console.WriteLine($"Task 1: {verticesInRightOrder.Sum()}");

            Packet numv1 = new Packet();
            numv1.Children.Add(2);
            numv1.IsDivider = true;
            goodPackets.Add(numv1);
            Packet numv2 = new Packet();
            numv2.Children.Add(6);
            numv2.IsDivider = true;
            goodPackets.Add(numv2);
            goodPackets.Sort(GetVerticesInRightOrder);

            int first = goodPackets.IndexOf(goodPackets.First(x => x.IsDivider)) + 1;
            int second = goodPackets.IndexOf(goodPackets.Last(x => x.IsDivider)) + 1;
            var result = first * second;
            Console.WriteLine($"Task 2: {first * second}");
        }

        int GetVerticesInRightOrder(Packet first, Packet second)
        {
            List<int> maxCount = new List<int>() { first.Children.Count, second.Children.Count() };
            for (int i = 0; i < maxCount.Max(); i++)
            {
                if (first.Children.Count() <= i || second.Children.Count() <= i)
                {
                    return CompareInts(first.Children.Count(), second.Children.Count());
                }
                else if (first.Children[i] is int && second.Children[i] is int)
                {
                    int state = CompareInts((int)first.Children[i], (int)second.Children[i]);
                    if (state != 0)
                    {
                        return state;
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (first.Children[i] is Packet && second.Children[i] is Packet)
                {
                    int state = GetVerticesInRightOrder((Packet)first.Children[i], (Packet)second.Children[i]);
                    if (state != 0)
                    {
                        return state;
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (first.Children[i] is int || second.Children[i] is int)
                {
                    Packet num1 = new Packet();
                    if (first.Children[i] is Packet)
                    {
                        num1 = (Packet)first.Children[i];
                    }
                    else if (first.Children[i] is int)
                    {
                        num1 = new Packet();
                        num1.Children.Add((int)first.Children[i]);
                    }

                    Packet num2 = new Packet();
                    if (second.Children[i] is Packet)
                    {
                        num2 = (Packet)second.Children[i];
                    }
                    else if (second.Children[i] is int)
                    {
                        num2 = new Packet();
                        num2.Children.Add((int)second.Children[i]);
                    }

                    int state = GetVerticesInRightOrder(num1, num2);
                    if (state != 0)
                    {
                        return state;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return 0;
        }

        int CompareInts(int first, int second)
        {
            if (first < second)
            {
                // its in right order
                // -1 was 1 but i did not know i could use it for sorting so after googling alot i found out i can sort it this way and just changed this...
                // (IT TOOK ME 1 WHOLE HOUR I WANNA CRY)
                return -1;
            }
            else if (first == second)
            {
                // Go next
                return 0;
            }
            else
            {
                // its not in right order
                return 1;
            }
        }

        int currentIndex = 1;
        Packet ParseLists(string currentLine, bool restartIndex = false)
        {
            if (restartIndex)
            {
                currentIndex = 1;
            }
            var m = new Packet();

            var currentNumber = "";
            for (int i = currentIndex; i < currentLine.Length; i++)
            {
                if (i < currentIndex)
                {
                    continue;
                }
                if (currentLine[i] == '[')
                {
                    currentIndex++;
                    var mm = ParseLists(currentLine);
                    m.Children.Add(mm);
                    currentIndex--;
                }
                else if (currentLine[i] == ']')
                {
                    currentIndex++;
                    if (!string.IsNullOrEmpty(currentNumber) && int.TryParse(currentNumber.ToString(), out int fullNumber))
                    {
                        m.Children.Add(fullNumber);
                        currentNumber = "";
                    }
                    return m;
                }
                else if (int.TryParse(currentLine[i].ToString(), out int result))
                {
                    currentNumber += currentLine[i];
                }
                else if (currentLine[i] == ',' && !string.IsNullOrEmpty(currentNumber) && int.TryParse(currentNumber.ToString(), out int fullNumber))
                {
                    m.Children.Add(fullNumber);
                    currentNumber = "";
                }

                currentIndex++;
            }

            return m;
        }
    }
    public class Packet
    {
        public bool IsDivider { get; set; } = false;
        public List<object> Children { get; set; } = new List<object>();
    }
}

