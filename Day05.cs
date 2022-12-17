using System.Text.RegularExpressions;
using MoreLinq;

public static partial class Day05
{
    [GeneratedRegex("""move (?<count>\d*) from (?<source>\d*) to (?<target>\d*)""")]
    private static partial Regex MoveRegex();

    record Move(int Count, int Source, int Target);

    public static void Solve()
    {
        var input = File.ReadAllLines("input/05.txt");
        var stackLines = input.TakeWhile(s => s.Length > 0).Reverse().ToArray();
        var moveLines = input.Skip(stackLines.Length + 1);

        var moves = moveLines.Select(l =>
        {
            var match = MoveRegex().Match(l);
            return new Move(
                int.Parse(match.Groups["count"].Value),
                int.Parse(match.Groups["source"].Value),
                int.Parse(match.Groups["target"].Value));
        }).ToArray();

        IReadOnlyDictionary<int, Stack<char>> stacks;

        InitStacks();
        ApplyMoves();
        PrintSolution();

        InitStacks();
        ApplyMultipleMoves();
        PrintSolution();

        void InitStacks()
        {
            stacks = stackLines.First()
                               .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                               .ToDictionary(int.Parse, _ => new Stack<char>());

            foreach (var line in stackLines.Skip(1))
            {
                int id = 1;
                for (int i = 1; i < line.Length; i += 4)
                {
                    if (line[i] != ' ')
                    {
                        stacks[id].Push(line[i]);
                    }

                    id++;
                }
            }
        }

        void ApplyMoves()
        {
            foreach (var move in moves)
            {
                for (int i = 0; i < move.Count; i++)
                {
                    stacks[move.Target].Push(stacks[move.Source].Pop());
                }
            }
        }

        void ApplyMultipleMoves()
        {
            foreach (var move in moves)
            {
                var temp = new Stack<char>();
                for (int i = 0; i < move.Count; i++)
                {
                    temp.Push(stacks[move.Source].Pop());
                }

                while (temp.Count > 0)
                {
                    stacks[move.Target].Push(temp.Pop());
                }
            }
        }

        void PrintSolution()
        {
            var solution = new string(stacks.OrderBy(s => s.Key).Select(s => s.Value.Peek()).ToArray());
            Console.WriteLine(solution);
        }
    }
}