/*
 * Created by SharpDevelop.
 * User: Dan
 * Date: 2014-03-24
 * Time: 23:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace Agent2048
{
	
	
	/// <summary>
	/// Description of State2048.
	/// </summary>
	public class State2048
	{
		static Random rng = new Random();
		
		public int [,] grid;
		public int rows;
		public int cols;
        public double freeCellScore = 0;
        public double averageCellTile = 0;


		//De novo constructor
		public State2048(int rows, int cols)
		{
			this.grid = new int[rows,cols];
			this.rows = rows;
			this.cols = cols;
		}

        public State2048(int rows, int cols, bool seed) : this(rows, cols)
        {
            if (seed)
            {
                spawnRandom();
                spawnRandom();
            }
        }
		
		//Copy constructor
		public State2048(State2048 s)
		{
			this.rows = s.rows;
			this.cols = s.cols;
			this.grid = new int[rows, cols];
			
			for(int r = 0; r < this.rows; r++)
				for(int c = 0; c < this.cols; c++)
					this.grid[r,c] = s.grid[r,c];
		}

        #region push functions
        public void pushLeft()
		{
			for(int r = 0; r < this.rows; r++)
				pushRowLeft(r);
		}
		
		public void pushRight()
		{
			for(int r = 0; r < this.rows; r++)
				pushRowRight(r);
		}
		
		private void pushRowLeft(int r)
		{
			//Collect the items
			List<int> items = new List<int>(this.cols);
			for(int c = 0; c < cols; c++)
				if( this.grid[r,c] != 0 )
					items.Add(this.grid[r,c]);
			
			//Consolidate duplicates
			for(int i = 0; i < items.Count - 1; i++)
			{
				if( items[i] == items[i+1] )
				{
					items[i]++;
					items.RemoveAt(i+1);
				}
			}
			
			//Write the data back to the row
			for(int c = 0; c < cols; c++)
				this.grid[r,c] = (c < items.Count ? items[c] : 0);
		}
		
		private void pushRowRight(int r)
		{
			//Collect the items
			List<int> items = new List<int>(this.cols);
			for(int c = 0; c < cols; c++)
				if( this.grid[r,c] != 0 )
					items.Add(this.grid[r,c]);
			
			//Consolidate duplicates
			for(int i = items.Count - 1; i > 0; i--)
			{
				if( items[i] == items[i-1] )
				{
					items[i]++;
					items.RemoveAt(i-1);
					i--;
				}
			}
			
			//Write the data back to the row
			for(int i = 0; i < this.cols; i++)
				this.grid[r,this.cols - 1 - i] = (items.Count - 1 - i >= 0 ? items[items.Count - 1 - i] : 0);
		}

		public void pushUp()
		{
			for(int c = 0; c < this.cols; c++)
				pushColUp(c);
		}
		
		private void pushColUp(int c)
		{
			//Collect the items
			List<int> items = new List<int>(this.cols);
			for(int r = 0; r < rows; r++)
				if( this.grid[r,c] != 0 )
					items.Add(this.grid[r,c]);
			
			//Consolidate duplicates
			for(int i = 0; i < items.Count - 1; i++)
			{
				if( items[i] == items[i+1] )
				{
					items[i]++;
					items.RemoveAt(i+1);
				}
			}
			
			//Write the data back to the row
			for(int r = 0; r < rows; r++)
				this.grid[r,c] = (r < items.Count ? items[r] : 0);
		}
		
		public void pushDown()
		{
			for(int c = 0; c < this.cols; c++)
				pushColDown(c);
		}
		
		private void pushColDown(int c)
		{
			//Collect the items
			List<int> items = new List<int>(this.cols);
			for(int r = 0; r < rows; r++)
				if( this.grid[r,c] != 0 )
					items.Add(this.grid[r,c]);
			
			//Consolidate duplicates
			for(int i = items.Count - 1; i > 0; i--)
			{
				if( items[i] == items[i-1] )
				{
					items[i]++;
					items.RemoveAt(i-1);
					i--;
				}
			}
			
			//Write the data back to the row
			for(int i = 0; i < this.rows; i++)
				this.grid[this.cols - 1 - i,c] = (items.Count - 1 - i >= 0 ? items[items.Count - 1 - i] : 0);
		}
        #endregion

        private List<Tuple<int, int>> getFree()
		{
			List<Tuple<int,int>> free = new List<Tuple<int, int>>();
			for(int r = 0; r < rows; r++)
				for(int c = 0; c < cols; c++)
					if( this.grid[r,c] == 0 )
						free.Add(new Tuple<int,int>(r,c));
			
			return free;
		}

		public void spawnRandom()
		{
			List<Tuple<int,int>> free = getFree();
			if( free.Count == 0 )
				return;
			
			Tuple<int,int> target = free[rng.Next(0, free.Count)];
			this.grid[target.Item1, target.Item2] = (rng.NextDouble() < .9 ? 1 : 2);
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
                            snakeList.Add(new Tile(r,c,board[r, c],4,0));
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

        public double SnakeRating()
        {
            List<Tile>[] snakes = new List<Tile>[2];
            snakes[0] = GetSnake(true, this.grid);
            snakes[1] = GetSnake(false, this.grid);

            //int[,] grid90 = RotateGrid(this.grid);

            //snakes[2] = GetSnake(true, grid90);
            //snakes[3] = GetSnake(false, grid90);

            //int[,] grid180 = RotateGrid(grid90);

            //snakes[4] = GetSnake(true, grid180);
            //snakes[5] = GetSnake(false, grid180);

            //int[,] grid270 = RotateGrid(grid180);

            //snakes[6] = GetSnake(true, grid270);
            //snakes[7] = GetSnake(false, grid270);

            //List<int> firstSnake = new List<int>();
            double score = 0;
            double bestScore = 0;
            double weight = 1;
            for (int x = 0; x < snakes.Length; x++)
            {
                score = 0;
                weight = 1;
                for (int i = 0; i < snakes[x].Count; i++)
                {
                    score += Math.Pow(2, snakes[x][i].value) * weight;
                    weight *= 0.25;
                }
                if (score > bestScore)
                    bestScore = score;
            }
            return bestScore;
        }


        
        public double rate()
		{
            // commented out to boost performance
            //List<StateTrans> moves = this.getAllMoveStates();
            //If we can't move, the game is over; worst penalty
            //if( moves.Count == 0 )
                //return double.MinValue;
            return SnakeRating();
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

        public double EmptyCells()
        {
            double freeCount = getFree().Count;
            return freeCount;
        }

        
		
		public static double alphabetarate(State2048 root, int depth, double alpha, double beta, bool player)
		{	
            string spcall = string.Format("alphabetarate(depth:{0},a{1},b{2},player{3}",depth, alpha, beta, player);
            //Trace.WriteLine(spcall);
			if( depth == 0 )
			{
				return root.rate();
			}
			
			if( player )
			{
				List<StateTrans> moves = root.getAllMoveStates();
			
				//If we can't move, the game is over; worst penalty
				if( moves.Count == 0 )
					return double.MinValue;
				
				foreach(StateTrans st in moves)
				{
					alpha = Math.Max(alpha, alphabetarate(st.state, depth - 1, alpha, beta, false));
					if ( beta <= alpha )
						break;
				}
				return alpha;
			}
			else
			{
				List<State2048> moves = root.getAllRandom();
				
				foreach(State2048 st in moves)
				{
					beta = Math.Min(beta, alphabetarate(st, depth - 1, alpha, beta, true));
					if ( beta <= alpha )
						break;
				}
				return beta;
			}
		}

        public static double minimax(State2048 root, int depth, bool player)
        {
            double bestValue = 0;
            double val = 0;
            if (depth == 0)
                return root.rate();
            if (player)
            {
                bestValue = double.MinValue;
                List<StateTrans> moves = root.getAllMoveStates();
			
				//If we can't move, the game is over; worst penalty
				if( moves.Count == 0 )
					return double.MinValue;

                foreach (StateTrans st in moves)
                {
                    val = minimax(st.state, depth - 1, false);
                    bestValue = Math.Max(val, bestValue);
                }
                return bestValue;
            }
            else
            {
                bestValue = double.MaxValue;
                List<State2048> moves = root.getAllRandom();

                foreach (State2048 st in moves)
                {
                    val = minimax(st, depth - 1, true);
                    bestValue = Math.Min(val, bestValue);
                }
                
                return bestValue;
            }
        }

        public static double expectiminimax(State2048 root, int depth, bool player)
        {
            double bestValue = 0;
            double val = 0;
            if (depth == 0)
                return root.rate();
            if (player)
            {
                bestValue = double.MinValue;
                List<StateTrans> moves = root.getAllMoveStates();

                //If we can't move, the game is over; worst penalty
                if (moves.Count == 0)
                    return double.MinValue;

                foreach (StateTrans st in moves)
                {
                    val = expectiminimax(st.state, depth - 1, false);
                    bestValue = Math.Max(val, bestValue);
                }
                return bestValue;
            }
            else
            {
                bestValue = 0;
                List<State2048> moves = root.getAllRandom();
                int i = 0;
                foreach (State2048 st in moves)
                {
                    bestValue += (ProbabilityOfChild(moves.Count, i) * expectiminimax(st, depth - 1, true));
                    i++;
                }

                return bestValue;
            }
        }

        private static double ProbabilityOfChild(int possibleChildren, int childIndex)
        {
            double probability = 0;
            double chance = 0;
            double twoChild = possibleChildren / 2;
            chance = (childIndex % 2 == 0) ? 0.9 : 0.1;
            probability = (double)(1 / twoChild) * chance;
            return probability;
        }

		public List<StateTrans> getAllMoveStates()
		{
			List<StateTrans> allMoves = new List<StateTrans>();
			
			State2048 next;

			next = new State2048(this);
			next.pushLeft();
			if( !this.equalTo(next) )
				allMoves.Add(new StateTrans(next, MoveDir.Left));
			
			next = new State2048(this);
			next.pushRight();
			if( !this.equalTo(next) )
				allMoves.Add(new StateTrans(next, MoveDir.Right));
			
			next = new State2048(this);
			next.pushUp();
			if( !this.equalTo(next) )
				allMoves.Add(new StateTrans(next, MoveDir.Up));
			
			next = new State2048(this);
			next.pushDown();
			if( !this.equalTo(next) )
				allMoves.Add(new StateTrans(next, MoveDir.Down));
			
			return allMoves;
		}
		
		public List<State2048> getAllRandom()
		{
			List<State2048> res = new List<State2048>();
			List<Tuple<int,int>> free = this.getFree();
			
			foreach(Tuple<int, int> x in free)
			{
				State2048 next;
				
				next = new State2048(this);
				next.grid[x.Item1, x.Item2] = 1;
				res.Add(next);
				
				next = new State2048(this);
				next.grid[x.Item1, x.Item2] = 2;
				res.Add(next);
			}
			
			return res;
		}
				
		public bool equalTo(State2048 alt)
		{
			if( this.rows != alt.rows )
				return false;
			if( this.cols != alt.cols )
				return false;
			
			for(int r = 0; r < rows; r++)
				for(int c = 0; c < cols; c++)
					if( this.grid[r,c] != alt.grid[r,c] )
						return false;
			
			return true;
		}
		
		public void display()
		{
			for(int r = 0; r < this.rows; r++)
			{
				for(int c = 0; c < this.cols; c++)
				{
					string number = "";
					
					if ( this.grid[r,c] != 0 )
					{
						number = ((int)Math.Pow(2,this.grid[r,c])).ToString();
					}
					
					Console.Write(number.PadLeft(5,' '));
				}
				Console.WriteLine();
			}
			Console.WriteLine(" ---- ---- ---- ----");
			Console.WriteLine("Rating: {0}", this.rate());
            //Console.WriteLine("Freecell: {0}", this.freeCellScore);
            //Console.WriteLine("AverageCellScore: {0}", this.averageCellTile);
		}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int r = 0; r < this.rows; r++)
            {
                for (int c = 0; c < this.cols; c++)
                {
                    string number = "";

                    if (this.grid[r, c] != 0)
                    {
                        number = ((int)Math.Pow(2, this.grid[r, c])).ToString();
                    }
                    sb.Append(number.PadLeft(5, ' '));
                    
                }
                sb.AppendLine();

            }
            sb.AppendLine(" ---- ---- ---- ----");
            
            return sb.ToString();
        }
		
	}
}
