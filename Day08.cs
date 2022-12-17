using System.Collections;

public static class Day08
{
    class VisibilityMap
    {
        private int rows;
        private int columns;

        private int[,] grid;
        private BitArray map;

        public bool AdjustCurrentHeight { get; set; }

        public VisibilityMap(int[,] grid)
        {
            this.grid = grid;

            rows = grid.GetLength(0);
            columns = grid.GetLength(1);

            map = new BitArray(rows * columns);
        }

        public void Reset() =>
            map.SetAll(false);

        public int CountVisibleTrees() =>
            map.Cast<bool>().Count(v => v);

        public void MarkTreesVisibleToTheRight(
            int row,
            int column,
            int maxHeight)
        {
            int currentHeight = -1;

            column++;
            while (column < columns)
            {
                if (grid[row, column] > currentHeight)
                {
                    if (AdjustCurrentHeight)
                    {
                        currentHeight = grid[row, column];
                    }

                    map.Set(row * columns + column, true);
                }

                if (grid[row, column] >= maxHeight)
                {
                    break;
                }

                column++;
            }
        }

        public void MarkTreesVisibleToTheLeft(
            int row,
            int column,
            int maxHeight)
        {
            int currentHeight = -1;

            column--;
            while (column >= 0)
            {
                if (grid[row, column] > currentHeight)
                {
                    if (AdjustCurrentHeight)
                    {
                        currentHeight = grid[row, column];
                    }

                    map.Set(row * columns + column, true);
                }

                if (grid[row, column] >= maxHeight)
                {
                    break;
                }

                column--;
            }
        }

        public void MarkTreesVisibleToTheBottom(
            int row,
            int column,
            int maxHeight)
        {
            int currentHeight = -1;

            row++;
            while (row < rows)
            {
                if (grid[row, column] > currentHeight)
                {
                    if (AdjustCurrentHeight)
                    {
                        currentHeight = grid[row, column];
                    }

                    map.Set(row * columns + column, true);
                }

                if (grid[row, column] >= maxHeight)
                {
                    break;
                }

                row++;
            }
        }

        public void MarkTreesVisibleToTheTop(
            int row,
            int column,
            int maxHeight)
        {
            int currentHeight = -1;

            row--;
            while (row >= 0)
            {
                if (grid[row, column] > currentHeight)
                {
                    if (AdjustCurrentHeight)
                    {
                        currentHeight = grid[row, column];
                    }

                    map.Set(row * columns + column, true);
                }

                if (grid[row, column] >= maxHeight)
                {
                    break;
                }

                row--;
            }
        }
    }

    public static void Solve()
    {
        var lines = File.ReadAllLines("input/08.txt");
        var rows = lines.Length;
        var columns = lines.First().Length;
        
        var grid = new int[rows, columns];
       
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                grid[r,c] = lines[r][c] - '0';
            }
        }

        Console.WriteLine(CountVisibleTrees());
        Console.WriteLine(CalculateHighestScore());

        int CountVisibleTrees()
        {
            var visibilityMap = new VisibilityMap(grid) { AdjustCurrentHeight = true };

            for (int r = 0; r < rows; r++)
            {
                visibilityMap.MarkTreesVisibleToTheLeft(r, columns, 9);
                visibilityMap.MarkTreesVisibleToTheRight(r, -1, 9);
            }

            for (int c = 0; c < columns; c++)
            {
                visibilityMap.MarkTreesVisibleToTheTop(rows, c, 9);
                visibilityMap.MarkTreesVisibleToTheBottom(-1, c, 9);
            }

            return visibilityMap.CountVisibleTrees();
        }

        int CalculateHighestScore()
        {
            int highestScore = 0;
            var visibilityMap = new VisibilityMap(grid) { AdjustCurrentHeight = false };

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    int score = 1;
                    var maxVisibleHeight = grid[r, c];

                    visibilityMap.Reset();
                    visibilityMap.MarkTreesVisibleToTheLeft(r, c, maxVisibleHeight);
                    score *= visibilityMap.CountVisibleTrees();

                    if (score == 0)
                    {
                        continue;
                    }

                    visibilityMap.Reset();
                    visibilityMap.MarkTreesVisibleToTheRight(r, c, maxVisibleHeight);
                    score *= visibilityMap.CountVisibleTrees();

                    if (score == 0)
                    {
                        continue;
                    }

                    visibilityMap.Reset();
                    visibilityMap.MarkTreesVisibleToTheTop(r, c, maxVisibleHeight);
                    score *= visibilityMap.CountVisibleTrees();

                    if (score == 0)
                    {
                        continue;
                    }

                    visibilityMap.Reset();
                    visibilityMap.MarkTreesVisibleToTheBottom(r, c, maxVisibleHeight);
                    score *= visibilityMap.CountVisibleTrees();

                    if (score > highestScore)
                    {
                        highestScore = score;
                    }
                }
            }

            return highestScore;
        }
    }
}