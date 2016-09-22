using System.Collections.Generic;
using System.Windows;

namespace A_Maze
{
    public partial class MainWindow : Window
    {
        public Maze Maze;
        public Cell[,] Grid => Maze.Grid;
        List<List<Cell>> lsts = new List<List<Cell>>();


        public MainWindow()
        {
            InitializeComponent();
            GenerateMaze();
            MazeGrid.ItemsSource = lsts;
        }

        public void GenerateMaze()
        {
            int height = 100;
            int width = 50;

            Maze = new Maze(width, height);

            for (int x = 0; x < width; x++)
            {
                lsts.Add(new List<Cell>());

                for (int y = 0; y < height; y++)
                {
                    lsts[x].Add(Maze.Grid[x, y]);
                }
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Maze.BuildMaze();
        }



    }
}
