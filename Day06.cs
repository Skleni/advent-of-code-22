public static class Day06
{
    public static void Solve()
    {
        FindMarker(4);
        FindMarker(14);
    }

    static void FindMarker(int distinctCharacters)
    {
        using var reader = new StreamReader("input/06.txt");
        var buffer = new char[distinctCharacters];
        int writePointer = 0;
        int count = 0;

        while (!reader.EndOfStream)
        {
            reader.Read(buffer, writePointer, 1);
            writePointer = (writePointer + 1) % buffer.Length;
            count++;

            if (count >= buffer.Length && buffer.Distinct().Count() == buffer.Length)
            {
                Console.WriteLine(count);
                return;
            }
        }
    }
}