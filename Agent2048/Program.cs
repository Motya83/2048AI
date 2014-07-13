/*
 * Created by SharpDevelop.
 * User: Dan
 * Date: 2014-03-24
 * Time: 23:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;

namespace Agent2048
{
	class Program
	{
			
		public static void Main(string[] args)
		{

            Point loc = Game2048.estimateLocationOfGameOnScreen(Color.FromArgb(255, 187, 173, 160));
			
			Console.WriteLine(loc);
			
			Size size = new Size(500,500);

            State2048 initS = Game2048.estimateBoardStateFromScreen(loc, size);
			initS.display();
			
			for(int timeout = 10; timeout > 0; timeout--)
			{
				Console.WriteLine("Waiting... {0}", timeout);
				System.Threading.Thread.Sleep(1000);
			}

            int depth = int.Parse(ConfigurationManager.AppSettings["depth"]);
			while( true )
			{
                //loc = Game2048.estimateLocationOfGameOnScreen(Color.FromArgb(255, 187, 173, 160));
				State2048 s = Game2048.estimateBoardStateFromScreen(loc,size);
				s.display();
				
				Console.WriteLine();
				
				double bestScore = double.MinValue;
				List<StateTrans> movesBest = new List<StateTrans>();
				
				List<StateTrans> moves = s.getAllMoveStates();
				foreach(StateTrans move in moves) 
				{
                    double moveRating = State2048.alphabetarate(move.state, depth, double.MinValue, double.MaxValue, true);
                    Console.WriteLine("{0}\t{1}", move.dir, moveRating);
                    //move.state.display();
                    //Console.WriteLine("____________________________");

                    if (moveRating > bestScore)
					{
                        bestScore = moveRating;
						movesBest.Clear();
					}

                    if (moveRating == bestScore)
					{
						movesBest.Add(move);
					}
				}
				
				if( movesBest.Count == 0 )
					break;
		
				
				Console.CursorLeft = 0;
				Console.CursorTop = 0;
				
				//Send Keys - AI
                if (movesBest[0].dir == MoveDir.Left)
                    System.Windows.Forms.SendKeys.SendWait("{LEFT}");

                if (movesBest[0].dir == MoveDir.Right)
                    System.Windows.Forms.SendKeys.SendWait("{RIGHT}");

                if (movesBest[0].dir == MoveDir.Up)
                    System.Windows.Forms.SendKeys.SendWait("{UP}");

                if (movesBest[0].dir == MoveDir.Down)
                    System.Windows.Forms.SendKeys.SendWait("{DOWN}");


                int sleepPause = int.Parse(ConfigurationManager.AppSettings["pause"]);
                System.Threading.Thread.Sleep(sleepPause);
				
				
                #region AI
                //if (movesBest[0].dir == MoveDir.Left)
                //    s.pushLeft();

                //if (movesBest[0].dir == MoveDir.Right)
                //    s.pushRight();

                //if (movesBest[0].dir == MoveDir.Up)
                //    s.pushUp();

                //if (movesBest[0].dir == MoveDir.Down)
                //    s.pushDown();
                #endregion

                #region Manual
                //ConsoleKeyInfo key = Console.ReadKey();
                //if (key.Key == ConsoleKey.LeftArrow)
                //    s.pushLeft();

                //if (key.Key == ConsoleKey.RightArrow)
                //    s.pushRight();

                //if (key.Key == ConsoleKey.UpArrow)
                //    s.pushUp();

                //if (key.Key == ConsoleKey.DownArrow)
                //    s.pushDown();

                //s.spawnRandom();
                #endregion

                Console.Clear();
				s.display();
			}
			
			Console.Write("Game over");
			Console.ReadKey(true);
		}
		
        
	}
	
}