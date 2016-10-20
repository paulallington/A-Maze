using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace A_Maze
{
    public class Maze
    {
        private readonly int _xSize;
        private readonly int _ySize;
        public readonly Cell[,] Grid;
        public event EventHandler<Tuple<Cell, Cell>> CellVisited;
        public event EventHandler<SolvedCell> CellSolved;

        public Maze(int xSize, int ySize) 
        {
            _xSize = xSize;
            _ySize = ySize;
            Grid = new Cell[xSize, ySize];

            // Initialise grid
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    Grid[x, y] = new Cell(x, y);
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
                    CellVisited?.Invoke(this, new Tuple<Cell, Cell>(current, next));
                    cellStack.Push(current);
                }

                current = next;
            }
            while (cellStack.Any());
        }

        /// <summary>
        /// Just...because
        /// </summary>
        public void Solve()
        {
            Cell startCell = Grid[0, 0];
            Cell currentCell = Grid[0, 0];
            Cell endCell = Grid[_xSize - 1, _ySize -1];

            CellSolved?.Invoke(this, new SolvedCell(startCell, SolveType.Start));
            CellSolved?.Invoke(this, new SolvedCell(endCell, SolveType.Start));

            List<Cell> open = new List<Cell>();
            List<Cell> closed = new List<Cell>();
            
            while (currentCell != endCell)
            {
                foreach (var cell in currentCell.Paths)
                {
                    cell.CalculateCosts(currentCell, startCell, endCell);
                }

                open.AddRange(currentCell.Paths);

                CellSolved?.Invoke(this, new SolvedCell(currentCell, SolveType.Path));
                closed.Add(currentCell);
                var nextCell = currentCell.Paths.Except(closed).OrderBy(x => x.F).FirstOrDefault();
                while (nextCell == null)
                {
                    CellSolved?.Invoke(this, new SolvedCell(currentCell, SolveType.Block));
                    currentCell = currentCell.PreviousCell;
                    nextCell = currentCell.Paths.Except(closed).OrderBy(x => x.F).FirstOrDefault();
                }

                nextCell.PreviousCell = currentCell;
                currentCell = nextCell;
            }
        }
    }
}