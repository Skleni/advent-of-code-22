public static class Day07
{
    interface IFileSystemEntry
    {
        string Name { get; }
        long Size { get; }
    }

    record Folder(string Name, Folder? Parent) : IFileSystemEntry
    {
        private List<IFileSystemEntry> entries = new();
        public long Size => this.entries.Sum(e => e.Size);

        public Folder GetOrAddFolder(string name)
        {
            var folder = this.entries
                             .OfType<Folder>()
                             .FirstOrDefault(e => e.Name == name);
            
            if (folder == null) 
            {
                folder = new Folder(name, this);
                this.entries.Add(folder);
            }

            return folder;
        }

        public void AddFile(string name, long size)
        {
            this.entries.Add(new File(name, size, this));
        }

        public IEnumerable<IFileSystemEntry> Entries => this.entries;
    }

    record File(string Name, long Size, Folder Parent) : IFileSystemEntry;

    public static void Solve()
    {
        var rootFolder = new Folder("/", null);
        var currentFolder = rootFolder;

        var input = System.IO.File.ReadAllLines("input/07.txt");
        foreach (var line in input)
        {
            switch (line)
            {
                case "$ cd /":
                    currentFolder = rootFolder;
                    break;

                case "$ cd ..":
                    currentFolder = currentFolder.Parent!;
                    break;

                case string cd when cd.StartsWith("$ cd "):
                    var folder = cd.Substring(5);
                    currentFolder = currentFolder.GetOrAddFolder(folder);
                    break;

                case "$ ls":
                    break;

                case string dir when dir.StartsWith("dir "):
                    break;

                default:
                    var parts = line.Split(' ');
                    currentFolder.AddFile(parts[1], int.Parse(parts[0]));
                    break;
            }
        }

        var solution = EnumerateFolders(rootFolder).Where(f => f.Size <= 100000)
                                                   .Sum(f => f.Size);
        Console.WriteLine(solution);

        var totalSize = 70000000;
        var requiredSize = 30000000;
        var usedSize = rootFolder.Size;
        var freeSize = totalSize - usedSize;
        var sizeToDelete = requiredSize - freeSize;

        var folderToDelete = EnumerateFolders(rootFolder)
            .Where(f => f.Size >= sizeToDelete)
            .MinBy(f => f.Size)!;

        Console.WriteLine(folderToDelete.Size);

        IEnumerable<Folder> EnumerateFolders(Folder startFolder)
        {
            yield return startFolder;
            foreach (var childFolder in startFolder.Entries.OfType<Folder>())
            {
                foreach (var folder in EnumerateFolders(childFolder))
                {
                    yield return folder;
                }
            }
        }
    }
}