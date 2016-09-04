using System.Collections.Generic;
using System.Windows;

namespace A_Maze
{
    public partial class MainWindow : Window
    {
        public
            Maze Maze;
        public Cell[,] Grid => Maze.Grid;


        public MainWindow()
        {
            InitializeComponent();
            GenerateMaze();
        }

        public void GenerateMaze()
        {
            int height = 100;
            int width = 50;

            Maze = new Maze(width, height);

            List<List<Cell>> lsts = new List<List<Cell>>();
            for (int x = 0; x < width; x++)
            {
                lsts.Add(new List<Cell>());

                for (int y = 0; y < height; y++)
                {
                    lsts[x].Add(Maze.Grid[x, y]);
                }
            }

            InitializeComponent();

            MazeGrid.ItemsSource = lsts;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Maze.BuildMaze();
        }



    }
}
