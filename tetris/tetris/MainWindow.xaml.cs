using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public const int Size = 50;
        public const int Padding = 1;
        public const int Width = 10;
        public const int Height = 10;

        public MainWindow()
        {
            InitializeComponent();
            InitBoard();
            DrawBoard();

            DrawTiles();


            ///BackgroundWorker worker = new BackgroundWorker();



        }

        private void DrawTiles()
        {
            TilesGrid.Children.Clear();
            ActiveFigureGrid.Children.Clear();

            foreach (var tile in Board.Tiles)
            {
                Border border = new Border();

                TilesGrid.Children.Add(border);

                border.Width = Size - Padding*2;
                border.Height = Size - Padding*2;

                Grid.SetRow(border, tile.Y);
                Grid.SetColumn(border, tile.X);
                border.Background = new SolidColorBrush(Colors.Orange);
                border.Padding = new Thickness(0);
            }

            foreach (var tile in Board.ActiveFigure.Tiles)
            {
                Border border = new Border();

                ActiveFigureGrid.Children.Add(border);

                border.Width = Size - Padding * 2;
                border.Height = Size - Padding * 2;

                Grid.SetRow(border, tile.Y + Board.ActiveFigure.Y);
                Grid.SetColumn(border, tile.X + Board.ActiveFigure.X);

                border.Background = new SolidColorBrush(Colors.OrangeRed);
                border.Padding = new Thickness(0);

            }
        }


        public void InitBoard()
        {
            Board.Width = Width;
            Board.Height = Height;

            Board.Tiles = new List<Tile>();

            Board.Tiles.AddRange(new []
            {
                new Tile(0, 0),
                new Tile(1, 0),
                new Tile(2, 0),
                new Tile(3, 0),
                new Tile(4, 0),
                new Tile(5, 0),
                new Tile(7, 0),
                new Tile(8, 0),
                new Tile(8, 1),
                new Tile(8, 2),
                new Tile(9, 0),
            });


            Board.ActiveFigure =
                new Figure
                {
                    Tiles = new List<Tile>(new Tile[]
                    {
                        new Tile(-1, 0),
                        new Tile(0, 0),
                        new Tile(1, 0),
                        new Tile(1, 1)
                    })
                };

            Board.ActiveFigure.X = 5;
            Board.ActiveFigure.Y = 5;

        }

        private void DrawBoard()
        {
            //Grid.ShowGridLines = true;

            Grid.RowDefinitions.Clear();
            for (int x = 0; x < Board.Width; x++)
            {
                Grid.ColumnDefinitions.Add(new ColumnDefinition() {Width = new GridLength(Size)});
                TilesGrid.ColumnDefinitions.Add(new ColumnDefinition() {Width = new GridLength(Size)});
                ActiveFigureGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(Size) });
            }

            for (int x = 0; x < Board.Height; x++)
            {
                Grid.RowDefinitions.Add(new RowDefinition() {Height = new GridLength(Size)});
                TilesGrid.RowDefinitions.Add(new RowDefinition() {Height = new GridLength(Size)});
                ActiveFigureGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(Size) });
            }


            for (int x = 0; x < Board.Width; x++)
            {
                for (int y = 0; y < Board.Height; y++)
                {
                    Border border = new Border();

                    Grid.Children.Add(border);

                    border.Width = Size - Padding*2;
                    border.Height = Size - Padding*2;
                    Grid.SetRow(border, y);
                    Grid.SetColumn(border, x);
                    border.Background = new SolidColorBrush(Colors.Beige);
                    border.Padding = new Thickness(0);
                }
            }
        }

        public Board Board = new Board();

        private void Grid_OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    MoveActiveFigure(Direction.Up);
                    break;

                case Key.Down:
                    MoveActiveFigure(Direction.Down);
                    break;

                case Key.Right:
                    MoveActiveFigure(Direction.Right);
                    break;

                case Key.Left:
                    MoveActiveFigure(Direction.Left);
                    break;

                case Key.Space:
                    RotateActiveFigure(Direction.Right);
                    break;
            }
        }

        private void RotateActiveFigure(Direction direction)
        {

            Board.ActiveFigure.Tiles.ForEach(tile =>
            {
                //  What quadrant are you in ? 

                int x = tile.X;
                int y = tile.Y;


                if (x <= 0 && y < 0 || x >= 0 && y > 0)
                {
                    tile.X = y;
                    tile.Y = -1*x;
                }
                else 
                {
                    tile.Y = x * -1;
                    tile.X = y;
                }

            });
            
            Board.ActiveFigure.Orientation = Board.ActiveFigure.Orientation == Orientation.Horizontal
                ? Orientation.Vertical
                : Orientation.Horizontal;

            DrawTiles();

        }

        private bool MoveActiveFigure(Direction direction)
        {
            if (!CheckBounds(direction)) 
                return false;

            switch (direction)
            {
                
                case Direction.Down:
                    
                    Board.ActiveFigure.Y -= 1;
                    break;

                case Direction.Up:
                    Board.ActiveFigure.Y += 1;

                    break;
                
                case Direction.Left:
                    Board.ActiveFigure.X -= 1;

                    break;
                
                case Direction.Right:
                    Board.ActiveFigure.X += 1;

                    break;
            }

            DrawTiles();
            return true;
        }

        private bool CheckBounds(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    if (Board.ActiveFigure.Tiles.Any(tile => Board.ActiveFigure.Y + tile.Y - 1 < 0)) return false;
                    break;

                case Direction.Up:
                    if (Board.ActiveFigure.Tiles.Any(tile => Board.ActiveFigure.Y + tile.Y + 1 >= Board.Height)) return false;
                    break;

                case Direction.Left:
                    if (Board.ActiveFigure.Tiles.Any(tile => Board.ActiveFigure.X + tile.X - 1 < 0)) return false;
                    break;

                case Direction.Right:
                    if (Board.ActiveFigure.Tiles.Any(tile => Board.ActiveFigure.X + tile.X + 1 >= Board.Width)) return false;
                    break;
            }

            return true;
        }
    }
}