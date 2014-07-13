/*
 * Created by SharpDevelop.
 * User: Dan
 * Date: 2014-03-25
 * Time: 16:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Agent2048
{
	public enum MoveDir { Left, Right, Up, Down, Self };

	public class StateTrans
	{
		public MoveDir dir;
		public State2048 state;
		
		public StateTrans(State2048 state, MoveDir dir)
		{
			this.dir = dir;
			this.state = state;
		}
	}
}
