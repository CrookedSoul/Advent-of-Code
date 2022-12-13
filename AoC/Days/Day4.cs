using System.Text;

namespace AoC.Days
{
    public class Day4
    {
        const string filename = "C:\\Users\\CrookedSoul\\Desktop\\AoC\\AoC\\AoC\\Files\\AoC_D4.txt";
        public Day4()
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
                        var ranges = currentLine.Split(",");
                        List<int> firstRange = ranges.First().Split("-").Select(x => int.Parse(x)).ToList();
                        List<int> secondRange = ranges.Last().Split("-").Select(x => int.Parse(x)).ToList();
                        if ((firstRange.First() <= secondRange.First() && firstRange.Last() >= secondRange.Last()) ||
                            (secondRange.First() <= firstRange.First() && secondRange.Last() >= firstRange.Last()))
                        {
                            result += 1;
                        }
                    }
                }
            }

            Console.WriteLine($"Result is {result}");
        }
        public void Task2()
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
                        var ranges = currentLine.Split(",");
                        List<int> firstRange = ranges.First().Split("-").Select(x => int.Parse(x)).ToList();
                        List<int> secondRange = ranges.Last().Split("-").Select(x => int.Parse(x)).ToList();
                        if ((firstRange.First() <= secondRange.First() && firstRange.Last() >= secondRange.Last()) ||
                            (secondRange.First() <= firstRange.First() && secondRange.Last() >= firstRange.Last()) ||
                            (firstRange.Last() >= secondRange.First() && firstRange.Last() <= secondRange.Last()) ||
                            (firstRange.First() >= secondRange.First() && firstRange.First() <= secondRange.Last()) ||
                            (secondRange.Last() >= firstRange.First() && secondRange.Last() <= firstRange.Last()) ||
                            (secondRange.First() >= firstRange.First() && secondRange.First() <= firstRange.Last()))
                        {
                            result += 1;
                        }
                    }
                }
            }

            Console.WriteLine($"Result is {result}");
        }
    }
}
