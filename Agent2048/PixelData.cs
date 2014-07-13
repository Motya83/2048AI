/*
 * Created by SharpDevelop.
 * User: Dan
 * Date: 2014-03-26
 * Time: 0:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Agent2048
{
	
	
	public struct PixelData
	{
	    public byte B;
	    public byte G;
	    public byte R;
	    public byte A;
	    
	    public PixelData(byte r, byte g, byte b, byte a)
	    {
	        this.R = r;
	        this.G = g;
	        this.B = b;
	        this.A = a;
	    }
	}
	
}
