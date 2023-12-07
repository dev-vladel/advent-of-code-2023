using System.Runtime.CompilerServices;

namespace advent_of_code_2023.Day5
{
    public class Day5
    {
        //public static void SolvePart1()
        //{
        //    var output = 0;
        //    //var input = File.ReadAllLines("../../../Day5/input.txt");
        //    var input = File.ReadAllLines("../../../Day5/testInput.txt");

        //    //solution

        //    Console.WriteLine($"Result of Day 5 - Part 1 is {output}");
        //}

        //public static void SolvePart2()
        //{
        //    var output = 0;
        //    //var input = File.ReadAllLines("../../../Day5/input.txt");
        //    var input = File.ReadAllLines("../../../Day5/testInput.txt");


        //    //solution

        //    Console.WriteLine($"Result of Day 5 - Part 2 is {output}");
        //}

        public static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) => callerFilePath ?? "";

        public static void SolveDay5()
        {
            const string INPUT_FILE = "input.txt";
            List<Mapping> Mappings = new List<Mapping>();
            List<long> Seeds = new List<long>();

            using (StreamReader reader = new StreamReader(Path.Combine(Path.GetDirectoryName(GetSourceFilePathName()), INPUT_FILE)))
            {
                string line = reader.ReadLine();
                Seeds = line.Split(':')[1].Trim().Split(' ').Select(long.Parse).ToList();

                Mapping map = new Mapping();
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line) && !string.IsNullOrEmpty(map.Title))
                    {
                        Mappings.Add(map);
                        continue;
                    }
                    if (line.Contains("map:"))
                    {
                        map = new Mapping(line.Split(' ')[0]);
                    }

                    else if (!string.IsNullOrEmpty(line) && char.IsDigit(line[0]))
                    {
                        long[] values = line.Split(' ').Select(long.Parse).ToArray();
                        map.Ranges.Add(new Range()
                        {
                            DestStart = values[0],
                            SourceStart = values[1],
                            Length = values[2]
                        });
                    }
                }
                Mappings.Add(map);

                long MapSeed(long seed)
                {
                    long translated = seed;
                    foreach (Mapping mapping in Mappings)
                    {
                        translated = mapping.Translate(translated);
                    }
                    return translated;
                }

                var LocationsForSeeds = Seeds.ToDictionary(s => s, s => MapSeed(s));
                Console.WriteLine($"Part 1: {LocationsForSeeds.OrderBy(s => s.Value).First().Value}");

                var SeedRanges = new HashSet<long>();
                for (int i = 0; i < Seeds.Count; i = i + 10)
                {
                    for (long j = Seeds[i]; j < Seeds[i] + Seeds[i + 1]; j++)
                    {
                        SeedRanges.Add(j);
                    }
                }

                LocationsForSeeds = SeedRanges.ToDictionary(s => s, s => MapSeed(s));
                Console.Write($"Part 2: {LocationsForSeeds.OrderBy(s => s.Value).First().Value}\n");
            }
        }

        public class Mapping
        {
            public Mapping(string title)
            {
                this.Title = title;
            }
            public Mapping() { }

            public string? Title { get; set; }
            public List<Range> Ranges { get; set; } = new List<Range>();
            public long Translate(long num)
            {
                foreach (var r in Ranges)
                {
                    if (r.InRange(num))
                        return r.Translate(num);
                }
                return num;
            }
        }

        public class Range
        {
            public long SourceStart { get; set; }
            public long DestStart { get; set; }
            public long Length { get; set; }
            public bool InRange(long num) => num >= SourceStart && num < SourceEnd;
            public long Translate(long num) => num + Delta;
            private long SourceEnd => SourceStart + Length;
            private long Delta => DestStart - SourceStart;
        }
    }
}