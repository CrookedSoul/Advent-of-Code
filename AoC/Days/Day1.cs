using System.Text;

namespace AoC.Days
{
    public class Day1
    {
        const string filename = "/Users/ratkoristic/Desktop/AoC/AoC/AoC/Files/AoC_D1.txt";

        public Day1()
        {
        }

        public int CheckAndReturnCalories()
        {
            List<ElfModel> Elves = new List<ElfModel>();
            ElfModel currentElf = new ElfModel();
            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string currentLine;
                while ((currentLine = streamReader.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(currentLine))
                    {
                        Elves.Add(currentElf);
                        currentElf = new ElfModel();
                        Console.WriteLine("- " + "New elf");
                    }
                    else if (int.TryParse(currentLine, out int result))
                    {
                        currentElf.Calories += result;
                        Console.WriteLine("- " + currentElf.Calories);
                    }
                }
            }

            // Task1
            //return Elves.Select(x => x.Calories).Max();

            // Task2
            return Elves.OrderByDescending(x => x.Calories).Take(3).Select(x => x.Calories).Sum();
        }
    }
    public class ElfModel
    {
        public int Calories { get; set; }
    }
}

