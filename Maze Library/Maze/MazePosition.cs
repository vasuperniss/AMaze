namespace Maze_Library.Maze
{
    public class MazePosition
    {
        private int row, colomn;

        public MazePosition(int row, int colomn)
        {
            this.row = row;
            this.colomn = colomn;
        }

        public override int GetHashCode()
        {
            string str = row + "|" + colomn;
            return str.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.row == (obj as MazePosition).row
                && this.colomn == (obj as MazePosition).colomn;
        }

        public int Row
        {
            get { return this.row; }
        }

        public int Colomn
        {
            get { return this.colomn; }
        }

        internal static MazePosition PositionBetween(MazePosition pos1
                                                        , MazePosition pos2)
        {
            int row = pos1.row;
            int col = pos1.colomn;

            if (pos2.row > pos1.row) { row++; }
            else if (pos2.row < pos1.row) { row--; }

            if (pos2.colomn > pos1.colomn) { col++; }
            else if (pos2.colomn < pos1.colomn) { col--; }

            return new MazePosition(row, col);
        }
    }
}