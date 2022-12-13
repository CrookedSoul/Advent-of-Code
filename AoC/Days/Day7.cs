using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Days
{
    public class Day7
    {
        int Result = 0;
        const string filename = "C:\\Users\\CrookedSoul\\Desktop\\AoC\\AoC\\AoC\\Files\\AoC_D7.txt";
        DirModel rootDirectory = new DirModel() { Directory = "/", Id = Guid.NewGuid().ToString() };
        public Day7()
        {
        }

        public void Task1()
        {
            DirModel currentDir = rootDirectory;

            bool isList = false;
            Regex file = new Regex("(\\s*[0-9]+)+");
            Regex dir = new Regex("(\\s*[dir]+)+");
            Regex goFurther = new Regex("\\s[a-z]$");
            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);

            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string currentLine;
                while ((currentLine = streamReader.ReadLine()) != null)
                {
                    if (currentLine.StartsWith("$ cd .."))
                    {
                        currentDir = GetDirectory(rootDirectory, currentDir.ParentId);
                    }
                    else if (currentLine.StartsWith("$ cd") && !currentLine.EndsWith("/"))
                    {
                        string letter = currentLine.Split(" ").Last();
                        currentDir = currentDir.Folders.First(x => x.Directory.Equals(letter.Trim()));
                    }
                    else if (currentLine.StartsWith("$ ls"))
                    {
                        isList = true;
                    }
                    else if (isList)
                    {
                        if (file.Match(currentLine).Success)
                        {
                            string[] parts = currentLine.Split(' ');
                            var filename = parts.Last().Trim();
                            var filesize = int.Parse(parts.First().Trim());

                            currentDir.Files.Add(new FileModel()
                            {
                                Name = filename,
                                Size = filesize,
                            });
                        }
                        else if (dir.Match(currentLine).Success)
                        {
                            string[] parts = currentLine.Split(' ');
                            var directory = parts.Last().Trim();
                            currentDir.Folders.Add(new DirModel() { Id = Guid.NewGuid().ToString(), Directory = directory, ParentId = currentDir.Id });
                        }
                    }
                    if (currentDir == null)
                    {
                        var test = 0;
                    }
                    UpdateDirectory(currentDir);
                }
            }

            CalculateSizesOfDirectories(rootDirectory);
            Console.WriteLine($"Result is {Result}");
        }
        int CalculateSizesOfDirectories(DirModel currentDir)
        {
            int currentDirectorySize = currentDir.Files.Select(x => x.Size).Sum();
            foreach (DirModel child in currentDir.Folders)
            {
                currentDirectorySize += CalculateSizesOfDirectories(child);
            }

            if (currentDirectorySize <= 100000)
            {
                Result += currentDirectorySize;
            }

            return currentDirectorySize;
        }

        public void Task2()
        {
            DirModel currentDir = rootDirectory;

            bool isList = false;
            Regex file = new Regex("(\\s*[0-9]+)+");
            Regex dir = new Regex("(\\s*[dir]+)+");
            Regex goFurther = new Regex("\\s[a-z]$");
            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);

            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string currentLine;
                while ((currentLine = streamReader.ReadLine()) != null)
                {
                    if (currentLine.StartsWith("$ cd .."))
                    {
                        currentDir = GetDirectory(rootDirectory, currentDir.ParentId);
                    }
                    else if (currentLine.StartsWith("$ cd") && !currentLine.EndsWith("/"))
                    {
                        string letter = currentLine.Split(" ").Last();

                        currentDir = currentDir.Folders.First(x => x.Directory.Equals(letter.Trim()));
                    }
                    else if (currentLine.StartsWith("$ ls"))
                    {
                        isList = true;
                    }
                    else if (isList)
                    {
                        if (file.Match(currentLine).Success)
                        {
                            string[] parts = currentLine.Split(' ');
                            var filename = parts.Last().Trim();
                            var filesize = int.Parse(parts.First().Trim());

                            currentDir.Files.Add(new FileModel()
                            {
                                Name = filename,
                                Size = filesize,
                            });
                        }
                        else if (dir.Match(currentLine).Success)
                        {
                            string[] parts = currentLine.Split(' ');
                            var directory = parts.Last().Trim();
                            currentDir.Folders.Add(new DirModel() { Id = Guid.NewGuid().ToString(), Directory = directory, ParentId = currentDir.Id });
                        }
                    }
                    if (currentDir == null)
                    {
                        var test = 0;
                    }
                    UpdateDirectory(currentDir);
                }
            }

            GetAllData(rootDirectory);
            int freeSpace = 70000000 - AllData.Max();
            CalculateForClearingSpace(rootDirectory, freeSpace);

            Console.WriteLine($"Result is {AllThatWouldClearEnough.Min()}");
        }
        List<int> AllData = new List<int>();
        List<int> AllThatWouldClearEnough = new List<int>();

        int GetAllData(DirModel currentDir)
        {
            int currentDirectorySize = currentDir.Files.Select(x => x.Size).Sum();
            foreach (DirModel child in currentDir.Folders)
            {
                currentDirectorySize += GetAllData(child);
            }

            AllData.Add(currentDirectorySize);
            return currentDirectorySize;
        }

        int CalculateForClearingSpace(DirModel currentDir, int freeSpace)
        {
            int currentDirectorySize = currentDir.Files.Select(x => x.Size).Sum();
            foreach (DirModel child in currentDir.Folders)
            {
                currentDirectorySize += CalculateForClearingSpace(child, freeSpace);
            }

            if (freeSpace + currentDirectorySize >= 30000000)
            {
                AllThatWouldClearEnough.Add(currentDirectorySize);
            }

            return currentDirectorySize;
        }

        void UpdateDirectory(DirModel currentDir)
        {
            DirModel updateDir = GetDirectory(rootDirectory, currentDir.Id);
            if (updateDir == null)
            {
                updateDir = GetDirectory(rootDirectory, currentDir.ParentId);
                updateDir.Folders.Add(currentDir);
            }
            else
            {
                foreach (var file in currentDir.Files)
                {
                    if (!updateDir.Files.Any(x => x.Name.Equals(file.Name)))
                    {
                        updateDir.Files.Add(file);
                    }
                }
                foreach (var folder in currentDir.Folders)
                {
                    if (!updateDir.Folders.Any(x => x.Directory.Equals(folder.Directory)))
                    {
                        updateDir.Folders.Add(folder);
                    }
                }
            }
        }

        DirModel GetDirectory(DirModel dir, string id)
        {
            if (dir == null) return dir;

            if (dir.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase))
            {
                return dir;
            }
            foreach (DirModel childDir in dir.Folders)
            {
                DirModel foundDir = GetDirectory(childDir, id);
                if (foundDir != null)
                {
                    return foundDir;
                }
            }
            return null;
        }
    }

    public class DirModel
    {
        public string Id { get; set; }
        public string Directory { get; set; }
        public string ParentId { get; set; }
        public List<FileModel> Files { get; set; } = new List<FileModel>();
        public List<DirModel> Folders { get; set; } = new List<DirModel>();
    }

    public class FileModel
    {
        public string Name { get; set; }
        public int Size { get; set; }
    }
}

