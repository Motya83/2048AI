using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Agent2048
{
    public class State2048ShelvedCode
    {
        static Random rng = new Random();

        public int[,] grid;
        public int rows;
        public int cols;
        public double freeCellScore = 0;
        public double averageCellTile = 0;

        public Point[] vectors = new Point[4];

        //    {
        //  0: { x: 0,  y: -1 }, // up
        //  1: { x: 1,  y: 0 },  // right
        //  2: { x: 0,  y: 1 },  // down
        //  3: { x: -1, y: 0 }   // left
        //}
		
        public int BoardTotalValue
        {
            get
            {
                int totalVal = 0;
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        if (grid[r, c] != 0)
                        {
                            int squareVal = (int)Math.Pow(((double)2), ((double)grid[r, c]));
                            totalVal += squareVal;
                        }
                    }
                }
                return totalVal;
            }
        }

        public int ZeroSquares
        {
            get
            {
                int numZero = 0;
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {


                        if (grid[r, c] == 0)
                        {
                            numZero++;
                            continue;
                        }
                    }
                }
                return numZero;
            }
        }

        public int NonZeroSquares
        {
            get
            {
                int numNonZero = rows * cols - ZeroSquares;
                return numNonZero;
            }
        }

        		//De novo constructor
        public State2048ShelvedCode(int rows, int cols)
		{
			this.grid = new int[rows,cols];
			this.rows = rows;
			this.cols = cols;
            this.vectors[0] = new Point(0, -1);// up
            this.vectors[1] = new Point(1, -0);// right
            this.vectors[2] = new Point(0, 1); // down
            this.vectors[3] = new Point(-1, 0);// left
		}

        private List<Tuple<int, int>> getNonFree()
        {
            List<Tuple<int, int>> nonFree = new List<Tuple<int, int>>();
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    if (this.grid[r, c] != 0)
                        nonFree.Add(new Tuple<int, int>(r, c));

            return nonFree;
        }

        public double AverageTileValue()
        {
            List<Tuple<int, int>> nonFree = getNonFree();
            double nonFreeCount = (double)nonFree.Count;
            double freeCount = rows * cols - nonFreeCount;
            double totalVal = (double)BoardTotalValue;
            //return (nonFreeCount / BoardTotalValue) * (nonFreeCount / freeCount);
            return (nonFreeCount / BoardTotalValue);
        }

        public double getMaxValue()
        {
            int max = 0;
            for (var x = 0; x < rows; x++)
            {
                for (var y = 0; y < cols; y++)
                {
                    if (this.grid[x, y] != 0)
                    {
                        int value = this.grid[x, y];
                        if (value > max)
                        {
                            max = value;
                        }
                    }

                }
            }
            double result = max; // Math.Log(max) / Math.Log(2);
            return result;
        }
        public Point getMaxValueLocation()
        {
            int maxX = 0, maxY = 0, max = 0;

            for (var x = 0; x < rows; x++)
            {
                for (var y = 0; y < cols; y++)
                {
                    if (this.grid[x, y] != 0)
                    {
                        int value = this.grid[x, y];
                        if (value > max)
                        {
                            max = value;
                            maxX = x; maxY = y;
                        }
                    }

                }
            }
            return new Point(maxX, maxY);
        }
        public bool WithinBounds(Point cell)
        {
            return cell.X >= 0 && cell.X < this.cols && cell.Y >= 0 && cell.Y < this.rows;
        }

        public bool CellOccupied(Point cell)
        {
            if (!WithinBounds(cell))
                return false;
            else
                return (this.grid[cell.X, cell.Y] != 0);
        }

        public FarthestPosition FindFartherstPosition(Tuple<int, int> cell, Point vector)
        {
            Tuple<int, int> previous;
            do
            {
                previous = cell;
                cell = new Tuple<int, int>(previous.Item1 + vector.X, previous.Item2 + vector.Y);
            }
            while (this.WithinBounds(new Point(cell.Item1, cell.Item2)) && !CellOccupied(new Point(cell.Item1, cell.Item2)));
            FarthestPosition fp = new FarthestPosition();
            fp.Farthest = previous;
            fp.Next = cell;
            return fp;
        }

        public double GetSmoothness()
        {
            double smoothness = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (this.grid[r, c] != 0)
                    {
                        int value = this.grid[r, c]; // Math.Log(this.cellContent(this.indexes[x][y]).value) / Math.log(2);
                        for (int direction = 1; direction <= 2; direction++)
                        {
                            Point vector = this.vectors[direction];
                            Tuple<int, int> targetCell = this.FindFartherstPosition(new Tuple<int, int>(r, c), vector).Next;

                            if (this.CellOccupied(new Point(targetCell.Item1, targetCell.Item2)))
                            {
                                int targetPower = this.grid[targetCell.Item1, targetCell.Item2];// this.cellContent(targetCell);
                                var targetValue = targetPower; //Math.Log(target.value) / Math.Log(2);
                                smoothness -= Math.Abs(value - targetValue);
                            }
                        }
                    }
                }
            }
            return smoothness;
        }

        public double MonotonicityOld2()
        {
            int[] totals = new int[] { 0, 0, 0, 0 };
            // vertical
            for (int x = 0; x < cols; x++)
            {
                int current = 0;
                int next = current + 1;
                while (next < cols)
                {
                    while (next < cols && !this.CellOccupied(new Point(x, next)))
                        next++;
                    if (next >= 4)
                        next--;
                    int currentValue = this.CellOccupied(new Point(x, current)) ? this.grid[x, current] : 0;
                    int nextValue = this.CellOccupied(new Point(x, next)) ? this.grid[x, next] : 0;
                    if (currentValue > nextValue)
                        totals[0] += nextValue - currentValue;
                    else if (nextValue > currentValue)
                        totals[1] += currentValue - nextValue;
                    current = next;
                    next++;
                }
            }
            // horizontal
            for (int y = 0; y < rows; y++)
            {
                int current = 0;
                int next = current + 1;
                while (next < rows)
                {
                    while (next < cols && !this.CellOccupied(new Point(next, y)))
                        next++;
                    if (next >= 4)
                        next--;
                    int currentValue = this.CellOccupied(new Point(current, y)) ? this.grid[current, y] : 0;
                    int nextValue = this.CellOccupied(new Point(next, y)) ? this.grid[next, y] : 0;
                    if (currentValue > nextValue)
                        totals[2] += nextValue - currentValue;
                    else if (nextValue > currentValue)
                        totals[3] += currentValue - nextValue;
                    current = next;
                    next++;
                }
            }
            return Math.Max(totals[0], totals[1]) + Math.Max(totals[2], totals[3]);
        }

        public double Monotonicity2()
        {
            int[] totals = new int[] { 0, 0, 0, 0 };
            // vertical
            for (int r = 0; r < rows; r++)
            {
                int current = 0;
                int next = current + 1;
                while (next < rows)
                {
                    while (next < rows && !this.CellOccupied(new Point(r, next)))
                        next++;
                    if (next >= 4)
                        next--;
                    int currentValue = this.CellOccupied(new Point(r, current)) ? this.grid[r, current] : 0;
                    int nextValue = this.CellOccupied(new Point(r, next)) ? this.grid[r, next] : 0;
                    if (currentValue > nextValue)
                        totals[0] += nextValue - currentValue;
                    else if (nextValue > currentValue)
                        totals[1] += currentValue - nextValue;
                    current = next;
                    next++;
                }
            }
            // horizontal
            for (int c = 0; c < cols; c++)
            {
                int current = 0;
                int next = current + 1;
                while (next < cols)
                {
                    while (next < cols && !this.CellOccupied(new Point(next, c)))
                        next++;
                    if (next >= 4)
                        next--;
                    int currentValue = this.CellOccupied(new Point(current, c)) ? this.grid[current, c] : 0;
                    int nextValue = this.CellOccupied(new Point(next, c)) ? this.grid[next, c] : 0;
                    if (currentValue > nextValue)
                        totals[2] += nextValue - currentValue;
                    else if (nextValue > currentValue)
                        totals[3] += currentValue - nextValue;
                    current = next;
                    next++;
                }
            }
            return Math.Max(totals[0], totals[1]) + Math.Max(totals[2], totals[3]);
        }

        public int[,] RotateGrid(int[,] inGrid)
        {
            int[,] rotatedGrid = new int[rows, cols];
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    rotatedGrid[i, j] = inGrid[4 - j - 1, i];
                }
            }
            return rotatedGrid;
        }

        public List<List<Tile>> Paths = new List<List<Tile>>();

        public List<Tile> BestPath = new List<Tile>();

        public double RatePaths()
        {
            FindAllPaths();

            double score = 0;
            double bestScore = 0;
            double weight = 1;

            foreach (List<Tile> currPath in this.Paths)
            {
                score = 0;
                weight = 1;
                for (int i = 0; i < currPath.Count; i++)
                {
                    score += Math.Pow(2, currPath[i].value) * weight;
                    weight *= 0.25;
                }
                if (score > bestScore)
                {
                    bestScore = score;
                    this.BestPath = currPath;
                }
            }
            return bestScore;
        }

        public List<Tile> GetSnake(bool horizontal, int[,] board)
        {
            List<Tile> snakeList = new List<Tile>();
            if (horizontal)
            {
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        // if even row (0,2)
                        if (r % 2 == 0)
                        {
                            snakeList.Add(new Tile(r, c, board[r, c], 4, 0));
                        }
                        else // if odd row(1,3)
                        {
                            snakeList.Add(new Tile(r, c, board[r, (cols - 1) - c], 4, 0));
                            //snakeList.Add(board[r, (cols - 1) - c]);
                        }

                    }
                }
            }
            else
            {
                for (int c = 0; c < cols; c++)
                {
                    for (int r = 0; r < rows; r++)
                    {
                        // if even column (0,2)
                        if (c % 2 == 0)
                        {
                            snakeList.Add(new Tile(r, c, board[r, c], 4, 0));
                            //snakeList.Add(board[r, c]);
                        }
                        else // if odd column (1,3)
                        {
                            snakeList.Add(new Tile((rows - 1) - r, c, board[r, c], 4, 0));
                            //snakeList.Add(board[(rows - 1) - r, c]);
                        }

                    }
                }
            }
            return snakeList;
        }

        public void FindAllPaths()
        {
            this.Paths.Clear();
            List<Tile> list = new List<Tile>();
            List<Tile> partialSnake = new List<Tile>();
            list = GetSnake(true, this.grid);
            int i = 0;
            foreach (Tile tile in list)
            {

                bool firstTtile = (i == 0);
                if (firstTtile || tile.value <= list[i - 1].value) //tile.value >= 4 && 
                    partialSnake.Add(tile);
                else
                    break;
                i++;
            }
            if (partialSnake.Count > 0 && partialSnake.Count < list.Count)
            {
                Tile nextTile = list[partialSnake.Count];
                Traverse(nextTile.row, nextTile.col, partialSnake, 0);
            }
            else
                Traverse(0, 0, partialSnake, 0);
            //Traverse(0, 0, list, 0);
            //Console.WriteLine("Total Paths " + this.Paths.Count.ToString());
        }

        public bool Traverse(int row, int col, List<Tile> currPath, int depth)
        {

            bool currMovePossible = false;
            //bool nextMovePossible = false;
            Tile currTile = new Tile(row, col, 0, 4, depth);
            //int depthDiff = currPath.Count == 0 ? 1 : (depth - currPath[currPath.Count - 1].depth);

            if (currTile.WithinBorder && !currPath.Contains(currTile)) // && depthDiff == 1
            {
                currMovePossible = true;
                currTile.value = this.grid[row, col];

                Tile prevTile = null;
                if (currPath.Count > 0)
                    prevTile = currPath[currPath.Count - 1];
                bool prevTileIsSmaller = false;
                if (prevTile != null)
                    if (currTile.value > prevTile.value)
                        prevTileIsSmaller = true;
                if (!prevTileIsSmaller)
                {
                    //Console.WriteLine("Traversing " + currTile.value.ToString() + "(" + row.ToString() + "," + col.ToString() + "," + currPath.Count.ToString() + ")");
                    List<Tile> copyPath = new List<Tile>();
                    foreach (Tile pastTile in currPath)
                    {
                        copyPath.Add(new Tile(pastTile.row, pastTile.col, pastTile.value, pastTile.n, pastTile.depth));
                    }
                    copyPath.Add(currTile);
                    if (depth < 5)
                    {
                        Traverse(row - 1, col, copyPath, depth + 1);
                        Traverse(row + 1, col, copyPath, depth + 1);
                        Traverse(row, col - 1, copyPath, depth + 1);
                        Traverse(row, col + 1, copyPath, depth + 1);
                    }

                    //if (!nextMovePossible && currPath.Count >= 14)
                    if (currPath.Count >= 2)
                        this.Paths.Add(currPath);
                }
            }
            return currMovePossible;
        }

        public double GetEntropy()
        {
            int numZero = 0;
            double entropy = 0;

            Dictionary<int, int> count = new Dictionary<int, int>();

            int cur;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {


                    if (grid[r, c] == 0)
                    {
                        numZero++;
                        continue;
                    }

                    if (!count.TryGetValue(grid[r, c], out cur))
                        count[grid[r, c]] = 1;
                    else
                        count[grid[r, c]] = cur + 1;
                }
            }

            int numNonZero = rows * cols - numZero;
            foreach (int k in count.Keys)
            {
                double freq = (double)count[k] / numNonZero;
                entropy -= freq * Math.Log(freq);
            }

            entropy /= Math.Log(rows * cols);
            return entropy;
        }
    }
}
