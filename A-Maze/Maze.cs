using System;
using System.Collections.Generic;
using System.Linq;

namespace A_Maze
{
    public class Maze
    {
        public readonly Cell[,] Grid;

        public Maze(int xSize, int ySize) 
        {
            Grid = new Cell[xSize, ySize];
            int count = 0;

            // Initialise grid
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    Grid[x, y] = new Cell(count++);
                }
            }

            // Set neighbours
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    AddNeighbour(Grid[x, y], 0, (y - 1 >= 0), x, y - 1);
                    AddNeighbour(Grid[x, y], 1, (x - 1 >= 0), x - 1, y);
                    AddNeighbour(Grid[x, y], 2, (y + 1 < ySize), x, y + 1);
                    AddNeighbour(Grid[x, y], 3, (x + 1 < xSize), x + 1, y);
                }
            }
        }

        private void AddNeighbour(Cell cell, int neighourIndex, bool inBounds, int x, int y)
        {
            if (inBounds) // go down
            {
                cell.Neighbours[neighourIndex] = Grid[x, y];
            }
        }

        private Cell GetNeighbour(Cell[,] grid, int x, int y)
        {
            if (grid[x, y] == null)
            {
                grid[x, y] = new Cell(0);
            }

            return grid[x, y];
        }
        
        public void BuildMaze()
        {
            Cell current = Grid[0, 0];
            Stack<Cell> cellStack = new Stack<Cell>();
            Random random = new Random();

            do
            {
                current.Visited = true;
                var next = current.PickUnvisitedNeighbour(random);

                if (next == null)
                {
                    next = cellStack.Pop();
                }
                else
                {
                    current.AddPath(next);
                    cellStack.Push(current);
                }

                current = next;
            }
            while (cellStack.Any());
        }
    }
}