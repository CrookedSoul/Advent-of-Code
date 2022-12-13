using System.Text;

namespace AoC.Days
{
    public class Day2
    {
        const string filename = "/Users/ratkoristic/Desktop/AoC/AoC/AoC/Files/AoC_D2.txt";

        public Day2()
        {
        }

        public void Task1()
        {
            List<RockPaperScissorsModel> RpsList = RockPaperScissorsModel.GetRockPaperScissorsTask1();
            List<int> result = new List<int>();
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
                        int score = 0;
                        var line = currentLine.Split(' ');
                        RockPaperScissorsModel rpsModel = RpsList.FirstOrDefault(x => x.Alias.Equals(line[1], StringComparison.InvariantCultureIgnoreCase));
                        if (rpsModel != null)
                        {
                            score += rpsModel.Score;
                            if (line[0].Equals(rpsModel.WinsAgainst, StringComparison.InvariantCultureIgnoreCase))
                            {
                                score += RockPaperScissorsModel.ScoreIfWon;
                                Console.WriteLine($"Won");
                            }
                            else if (line[0].Equals(rpsModel.Draw, StringComparison.InvariantCultureIgnoreCase))
                            {
                                score += RockPaperScissorsModel.ScoreIfDraw;
                                Console.WriteLine($"Draw");
                            }
                            else
                            {
                                Console.WriteLine($"Loss");
                            }
                            result.Add(score);
                        }
                    }
                }
            }

            Console.WriteLine($"Task1 Total Score is {result.Sum()}");
        }

        public void Task2()
        {
            List<RockPaperScissorsModel> RpsList = RockPaperScissorsModel.GetRockPaperScissorsTask2();
            List<TournamentSetupModel> SetupsList = TournamentSetupModel.GetSetups();
            List<int> result = new List<int>();
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
                        int score = 0;
                        var line = currentLine.Split(' ');
                        TournamentSetupModel setupModel = SetupsList.First(x => x.Alias.Equals(line[1], StringComparison.InvariantCultureIgnoreCase));
                        if (setupModel != null)
                        {
                            if (setupModel.NeedToWin)
                            {
                                RockPaperScissorsModel heIsTrowing = RpsList.First(x => x.Alias.Equals(line[0], StringComparison.InvariantCultureIgnoreCase));
                                RockPaperScissorsModel iNeedToTrow = RpsList.First(x => x.Alias.Equals(heIsTrowing.LosesAgainst, StringComparison.InvariantCultureIgnoreCase));
                                score += RockPaperScissorsModel.ScoreIfWon + iNeedToTrow.Score;
                                Console.WriteLine($"NeedToWin");
                            }
                            if (setupModel.NeedToDraw)
                            {
                                RockPaperScissorsModel heIsTrowing = RpsList.First(x => x.Alias.Equals(line[0], StringComparison.InvariantCultureIgnoreCase));
                                RockPaperScissorsModel iNeedToTrow = RpsList.First(x => x.Alias.Equals(heIsTrowing.Draw, StringComparison.InvariantCultureIgnoreCase));
                                score += RockPaperScissorsModel.ScoreIfDraw + iNeedToTrow.Score;
                                Console.WriteLine($"NeedToDraw");
                            }
                            if (setupModel.NeedToLose)
                            {
                                RockPaperScissorsModel heIsTrowing = RpsList.First(x => x.Alias.Equals(line[0], StringComparison.InvariantCultureIgnoreCase));
                                RockPaperScissorsModel iNeedToTrow = RpsList.First(x => x.Alias.Equals(heIsTrowing.WinsAgainst, StringComparison.InvariantCultureIgnoreCase));
                                score += iNeedToTrow.Score;
                                Console.WriteLine($"NeedToLose");
                            }

                            result.Add(score);
                        }
                    }
                }
            }

            Console.WriteLine($"Task2 Total Score is {result.Sum()}");
        }
	}

    public class RockPaperScissorsModel
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public string Alias { get; set; }
        public string WinsAgainst { get; set; }
        public string Draw { get; set; }
        public string LosesAgainst { get; set; }

        public static List<RockPaperScissorsModel> GetRockPaperScissorsTask1()
        {
            List<RockPaperScissorsModel> result = new List<RockPaperScissorsModel>();
            result.Add(new RockPaperScissorsModel() { Name = "Rock", Score = 1, Alias = "x", WinsAgainst = Scissor, Draw = Rock });
            result.Add(new RockPaperScissorsModel() { Name = "Paper", Score = 2, Alias = "y", WinsAgainst = Rock, Draw = Paper });
            result.Add(new RockPaperScissorsModel() { Name = "Scissor", Score = 3, Alias = "z", WinsAgainst = Paper, Draw = Scissor });
            return result;
        }

        public static List<RockPaperScissorsModel> GetRockPaperScissorsTask2()
        {
            List<RockPaperScissorsModel> result = new List<RockPaperScissorsModel>();
            result.Add(new RockPaperScissorsModel() { Name = "Rock", Score = 1, Alias = Rock, WinsAgainst = Scissor, Draw = Rock, LosesAgainst = Paper });
            result.Add(new RockPaperScissorsModel() { Name = "Paper", Score = 2, Alias = Paper, WinsAgainst = Rock, Draw = Paper, LosesAgainst = Scissor });
            result.Add(new RockPaperScissorsModel() { Name = "Scissor", Score = 3, Alias = Scissor, WinsAgainst = Paper, Draw = Scissor, LosesAgainst = Rock });
            return result;
        }

        public const int ScoreIfWon = 6;
        public const int ScoreIfDraw = 3;
        private const string Rock = "a";
        private const string Paper = "b";
        private const string Scissor = "c";
    }

    public class TournamentSetupModel
    {
        public bool NeedToLose => Alias.Equals("x", StringComparison.InvariantCultureIgnoreCase);
        public bool NeedToDraw => Alias.Equals("y", StringComparison.InvariantCultureIgnoreCase);
        public bool NeedToWin => Alias.Equals("z", StringComparison.InvariantCultureIgnoreCase);
        public string Alias { get; set; }

        public static List<TournamentSetupModel> GetSetups()
        {
            List<TournamentSetupModel> result = new List<TournamentSetupModel>();
            result.Add(new TournamentSetupModel() { Alias = "x" });
            result.Add(new TournamentSetupModel() { Alias = "y" });
            result.Add(new TournamentSetupModel() { Alias = "z" });
            return result;
        }
    }
}

