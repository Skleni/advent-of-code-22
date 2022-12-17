using MoreLinq;

public static class Day03
{
    static (string First, string Second) GetRucksack(string line)
    {
        int middle = line.Length / 2;
        return (line.Substring(0, middle), line.Substring(middle));
    }

    static int GetPriority(char c) =>
        char.IsLower(c)
        ? c - 'a' + 1
        : c - 'A' + 27;

    public static void Solve()
    {
        var input = File.ReadAllLines("input/03.txt");
        var rucksacks = input.Select(GetRucksack);
        var doubleItems = rucksacks.Select(r => r.First.Distinct().Single(c => r.Second.Contains(c))).ToList();
        var doubleItemPriorities = doubleItems.Select(GetPriority);

        Console.WriteLine(doubleItemPriorities.Sum());

        var groups = input.Batch(3);
        var badgeItems = groups.Select(g => g.First().Distinct().Single(c => g.Skip(1).All(r => r.Contains(c))));
        var badgeItemPriorities = badgeItems.Select(GetPriority);

        Console.WriteLine(badgeItemPriorities.Sum());
    }
}