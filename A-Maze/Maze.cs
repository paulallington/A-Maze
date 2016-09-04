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
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    Grid[x, y] = Grid[x, y] ?? new Cell(count++);

                    // Populate neighbours
                    if (y - 1 >= 0) // go down
                    {
                        Grid[x, y].Neighbours[0] = GetNeighbour(Grid, x, y - 1);
                    }
                    if (x - 1 >= 0) // go left
                    {
                        Grid[x, y].Neighbours[1] = GetNeighbour(Grid, x - 1, y);
                    }
                    if (y + 1 < ySize) // go up
                    {
                        Grid[x, y].Neighbours[2] = GetNeighbour(Grid, x, y + 1); 
                    }
                    if (x + 1 < xSize) // go right
                    {
                        Grid[x, y].Neighbours[3] = GetNeighbour(Grid, x + 1, y);
                    }
                }
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