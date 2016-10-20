using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace A_Maze
{
    public class Cell
    {
        public int X, Y;
        public Cell[] Neighbours = new Cell[4];
        public bool Visited;
        public ObservableCollection<Cell> Paths = new ObservableCollection<Cell>(); 

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Cell PickUnvisitedNeighbour(Random random)
        {
            var neighbours = Neighbours.Where(x => x != null && !x.Visited).ToList();

            if (neighbours.Any())
            {
                int r = random.Next(neighbours.Count);
                return neighbours[r];
            }

            return null;
        }

        public void AddPath(Cell next)
        {
            Paths.Add(next);
            next.Paths.Add(this);
        }

        public int Border(Cell west)
        {
            return Paths.Contains(west) ? 0 : 1;
        }
    }
}
