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

    public enum Orientation
    {
        Horizontal,
        Vertical

    }

    public class Figure
    {
        public Orientation Orientation;

        public Figure()
        {
            this.Orientation = Orientation.Horizontal;
        }
        public void RotateLeft()
        {}

        public void RotateRight()
        {}

        public List<Tile> Tiles;


        public int X { get; set; }
        public int Y { get; set; }
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

    public enum Direction
    {
        Right,
        Up,
        Down,
        Left
    }
}
