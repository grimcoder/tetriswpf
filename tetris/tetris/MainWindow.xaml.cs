using System.Collections.Generic;
using System.Diagnostics;
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
        public MainWindow()
        {
            InitializeComponent();
            InitBoard();
            DrawBoard();

            DrawTiles();

        }

        private void DrawTiles()
        {
            
        }

        public void InitBoard()
        {
            Board.Width = 10;
            Board.Height = 10;

            Board.Tiles = new List<Tile>();
             
            Board.Tiles.AddRange(new Tile[]
            {
                new Tile(0,0),
                new Tile(1,0),
                new Tile(2,0),
                new Tile(3,0),
                new Tile(4,0),
                new Tile(4,1),
                new Tile(5,2),
            }
        );

        }

        private void DrawBoard()
        {
            //Grid.ShowGridLines = true;
            
            Grid.RowDefinitions.Clear();
            for (int x = 0; x < Board.Width; x++)
            {
                Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(Size) });
                TilesGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(Size) });
            }
            
            for (int x = 0; x < Board.Height; x++)
            {
                Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(Size) });
                TilesGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(Size) });
            }


            for (int x = 0; x < Board.Width; x++)
            {
                for (int y = 0; y < Board.Height; y++)
                {
                    Border border = new Border();

                    Grid.Children.Add(border);

                    border.Width = Size - Padding * 2;
                    border.Height = Size - Padding * 2;
                    Grid.SetColumn(border, y);
                    Grid.SetRow(border, x);
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
                    Debug.WriteLine("key up");
                    break;   
             
                case Key.Down:
                    Debug.WriteLine("key down");
                    break;   
             
                case Key.Right:
                    Debug.WriteLine("key right");
                    break;     
           
                case Key.Left:
                    Debug.WriteLine("key left");
                    break;


            }
        }


    }
}
