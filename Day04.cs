using MoreLinq;

public static class Day04
{
    record Assignment(int Start, int End)
    {
        public bool Contains(Assignment other)
        {
            return other.Start >= this.Start && other.End <= this.End;
        }

        public bool Overlaps(Assignment other)
        {
            return other.Start <= this.End && other.End >= this.Start;
        }
    }

    static Assignment ParseAssignment(string s)
    {
        var numbers = s.Split('-').Select(int.Parse).ToArray();
        return new Assignment(numbers[0], numbers[1]);
    }

    static Assignment[] ParseAssignmentPair(string s) =>
        s.Split(',').Select(ParseAssignment).ToArray();

    public static void Solve()
    {
        var input = File.ReadAllLines("input/04.txt");
        var pairs = input.Select(ParseAssignmentPair);

        Console.WriteLine(pairs.Count(p => p[0].Contains(p[1]) || p[1].Contains(p[0])));
        Console.WriteLine(pairs.Count(p => p[0].Overlaps(p[1])));
    }
}