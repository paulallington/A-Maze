using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace A_Maze
{
    public partial class MainWindow : Window
    {
        public Maze Maze;
        public Cell[,] Grid => Maze.Grid;
        private SolidColorBrush blackBrush = new SolidColorBrush() { Color = Colors.Black };
        private SolidColorBrush whiteBrush = new SolidColorBrush() { Color = Colors.White };

        public MainWindow()
        {
            InitializeComponent();
            GenerateMazeObjects();

            // Run in background
            Task.Run(() => Maze.BuildMaze());
        }

        public void GenerateMazeObjects()
        {
            int height = (int)((Height - 60) / 10);
            int width = (int)((Width-30) / 10);

            Maze = new Maze(width, height);
            Maze.CellVisited += Maze_Visited;

            foreach (Cell cell in Maze.Grid)
            {
                AddRectangle(cell);
            }

            // Add begining and end
            Surface.Children.Add(AddPath(0, 0, 0, 10));
            Surface.Children.Add(AddPath(width * 10, height * 10 - 10, width * 10, height * 10));
        }

        private void Maze_Visited(object sender, Tuple<Cell, Cell> cells)
        {
            AddPath(cells.Item1, cells.Item2);
            Thread.Sleep(1); // Cos it's fun to watch!
        }

        private void AddRectangle(Cell cell)
        {
            Rectangle rect = new Rectangle { Width = 11, Height = 11, Stroke = blackBrush, StrokeThickness = 1 };
            Surface.Children.Add(rect);
            Canvas.SetLeft(rect, cell.X * 10 + 10);
            Canvas.SetTop(rect, cell.Y * 10 + 10);
        }

        private void AddPath(Cell cell, Cell neighbour)
        {
            double x = cell.X * 10;
            double y = cell.Y * 10;

            Dispatcher.Invoke(() =>
            {
                if (cell.Neighbours[0] == neighbour)
                {
                    Surface.Children.Add(AddPath(x + 1, y, x + 10, y));
                }
                else if (cell.Neighbours[1] == neighbour)
                {
                    Surface.Children.Add(AddPath(x, y + 1, x, y + 10)); 
                }
                else if (cell.Neighbours[2] == neighbour)
                {
                    Surface.Children.Add(AddPath(x + 1, y + 10, x + 10, y + 10));
                }
                else if (cell.Neighbours[3] == neighbour)
                {
                    Surface.Children.Add(AddPath(x + 10, y + 1, x + 10, y + 10));
                }
            });
        }

        private Line AddPath(double x1, double y1, double x2, double y2)
        {
            Line line = new Line();
            line.SnapsToDevicePixels = true;
            line.X1 = x1 + 10;
            line.Y1 = y1 + 10;
            line.X2 = x2 + 10;
            line.Y2 = y2 + 10;
            line.Stroke = whiteBrush;
            line.StrokeThickness = 1;

            return line;
        }
    }
}
