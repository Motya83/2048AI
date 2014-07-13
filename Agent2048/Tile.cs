using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agent2048
{
    public class Tile : IEquatable<Tile>
    {
        public int row;
        public int col;
        public int value;
        public int n;
        public int depth;

        public bool WithinBorder
        {
            get
            {
                return (row >= 0 && row < n) && (col >= 0 && col < n);
            }
        }

        public Tile(int r, int c, int v, int gridsize, int d)
        {
            this.row = r;
            this.col = c;
            this.value = v;
            this.n = gridsize;
            this.depth = d;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Tile objAsTile = obj as Tile;
            if (objAsTile == null) return false;
            else
            {
                return Equals(objAsTile);
            }
            
        }

        public bool Equals(Tile otherTile)
        {
            return otherTile.col == this.col && otherTile.row == this.row;
        }

        public override int GetHashCode()
        {
            return row * col * value * n;
        }

        public override string ToString()
        {
            string valstr = value >= 1 ? Math.Pow(2, value).ToString() : "0";
            return valstr + "(" + row + "," + col + ") ";
        }
    }
}
