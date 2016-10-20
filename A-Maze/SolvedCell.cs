
namespace A_Maze
{
    public class SolvedCell
    {
        public Cell Cell { get; set; }
        public SolveType Type { get; set; }

        public SolvedCell(Cell cell, SolveType type)
        {
            Cell = cell;
            Type = type;
        }
    }

    public enum SolveType
    {
        Start,
        Block,
        Path
    }
}
