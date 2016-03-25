namespace Maze_Library.Maze.Matrix
{
    class MatrixPosition : IMazePosition
    {
        private int row, colomn;

        public MatrixPosition(int row, int colomn)
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
            return this.row == (obj as MatrixPosition).row
                && this.colomn == (obj as MatrixPosition).colomn;
        }
    }
}
