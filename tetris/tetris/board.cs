using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    public class Board
    {
        public int Height;
        public int Width;
        public List<Tile> Tiles = new List<Tile>();

        public Figure ActiveFigure;
    }

    public class Figure
    {
        public void RotateLeft()
        {}

        public void RotateRight()
        {}

        public List<Tile> Tiles;

    }

    public class Tile
    {
        public Tile(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X;

        public int Y;

    }
}
