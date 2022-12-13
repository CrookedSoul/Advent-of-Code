using System;
using System.Text;

namespace AoC.Days
{
	public class Day6
    {
        const string filename = "/Users/ratkoristic/Desktop/AoC/AoC/AoC/Files/AoC_D6.txt";
        public Day6()
		{
		}

		public void Task1()
		{
            string fullString = "";
            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string currentLine;
                while ((currentLine = streamReader.ReadLine()) != null)
                {
                    fullString += currentLine;
                }
            }

            int characters = 0;
            string marker = "";
            foreach (char ch in fullString)
            {
                if (marker.Length == 14)
                {
                    Console.WriteLine($"Result is {characters}");
                    return;
                }
                if (!marker.Contains(ch))
                {
                    marker += ch;
                }
                else {
                    marker = marker.Substring(marker.IndexOf(ch) + 1) + ch;
                }
                characters++;
            }
            Console.WriteLine($"Result is {characters}");
        }
	}
}

