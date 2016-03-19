namespace Maze_Library
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
    }
}