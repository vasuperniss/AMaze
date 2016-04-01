namespace MazeServer.Model.JsonOptions
{
    class MazePosition
    {
        private int row;
        private int col;

        public int Row
        {
            get { return this.row; }
            set { this.row = value; }
        }

        public int Col
        {
            get { return this.col; }
            set { this.col = value; }
        }

        public override string ToString()
        {
            return "(row: " + this.row + ", col: " + this.col + ")";
        }
    }
}
