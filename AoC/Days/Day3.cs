using System.Text;

namespace AoC.Days
{
    public class Day3
    {
        const string filename = "C:\\Users\\CrookedSoul\\Desktop\\AoC\\AoC\\AoC\\Files\\AoC_D3.txt";
        public Day3()
        {
        }

        public void Task1()
        {
            int result = 0;
            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string currentLine;
                while ((currentLine = streamReader.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(currentLine))
                    {
                    }
                    else
                    {
                        string firsthalf = currentLine.Substring(0, currentLine.Length / 2);
                        string secondHalf = currentLine.Substring((currentLine.Length / 2));
                        if (firsthalf.Count() == secondHalf.Count())
                        {
                            Console.WriteLine("even");
                            string common = twoStrings(firsthalf, secondHalf);
                            PriorityEnum priority = (PriorityEnum)Enum.Parse(typeof(PriorityEnum), common);
                            result += (int)priority;

                            Console.WriteLine($"Added {(int)priority} which is {priority}");
                        }
                        else
                        {
                            Console.WriteLine("uneven");
                        }
                    }
                }
            }

            Console.WriteLine($"Priority is {result}");
        }

        public void Task2()
        {
            List<int> result = new List<int>();
            List<string> group = new List<string>();
            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string currentLine;
                while ((currentLine = streamReader.ReadLine()) != null)
                {
                    group.Add(currentLine);
                    if (group.Count() == 3)
                    {
                        string common = threeStrings(group);
                        PriorityEnum priority = (PriorityEnum)Enum.Parse(typeof(PriorityEnum), common);
                        result.Add((int)priority);

                        Console.WriteLine($"Added {(int)priority} which is {priority}");
                        group = new List<string>();
                    }
                }
            }

            Console.WriteLine($"Priority is {result.Sum()}");
        }
        static string threeStrings(List<string> group)
        {
            string firstElf = group[0];
            string secondElf = group[1];
            string thirdElf = group[2];

            string commonBetween2 = "";
            string common = "";

            commonBetween2 = twoStrings(firstElf, secondElf);
            common = twoStrings(commonBetween2, thirdElf);

            return common;
        }

        static string twoStrings(string firstHalf, string secondHalf)
        {
            string common = "";

            for (int i = 0; i < firstHalf.Length; i++)
            {
                if (secondHalf.Contains(firstHalf[i]) && !common.Contains(firstHalf[i]))
                {
                    common += firstHalf[i];
                }
            }

            return common;
        }
    }

    enum PriorityEnum
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
        A = 27,
        B = 28,
        C = 29,
        D = 30,
        E = 31,
        F = 32,
        G = 33,
        H = 34,
        I = 35,
        J = 36,
        K = 37,
        L = 38,
        M = 39,
        N = 40,
        O = 41,
        P = 42,
        Q = 43,
        R = 44,
        S = 45,
        T = 46,
        U = 47,
        V = 48,
        W = 49,
        X = 50,
        Y = 51,
        Z = 52,
    }
}
