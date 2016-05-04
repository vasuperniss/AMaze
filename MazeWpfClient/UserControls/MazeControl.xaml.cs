using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MazeWpfClient.UserControls
{
    /// <summary>
    /// Interaction logic for MazeControl.xaml
    /// </summary>
    public partial class MazeControl : UserControl
    {
        /// <summary>
        /// The player (graphicly)
        /// </summary>
        private Ellipse player;
        /// <summary>
        /// The canvas width and height
        /// </summary>
        private int canvasWidth = 320, canvasHeight = 320;
        /// <summary>
        /// The maze cells - for drawing mazes
        /// </summary>
        private Border[,] mazeCells;

        /// <summary>
        /// The actual number of columns in the maze
        /// </summary>
        private int mCols;
        /// <summary>
        /// The actual number of rows in the maze
        /// </summary>
        private int mRows;

        /// <summary>
        /// The end point's row and column
        /// </summary>
        private int endCol, endRow;
        /// <summary>
        /// The start point's row and column
        /// </summary>
        private int startCol, startRow;

        /// <summary>
        /// The row and column indexes for the last hint
        /// </summary>
        private int lastHintRow = -1, lastHintCol = -1;
        /// <summary>
        /// The background color of the cells
        /// </summary>
        private Brush backgroundColor;
        private Brush borderColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeControl"/> class.
        /// </summary>
        public MazeControl()
        {
            InitializeComponent();
            // read rows and cols from AppSettings
            this.mRows = AppSettings.Settings["rows"] != null ?
                                int.Parse(AppSettings.Settings["rows"]) : 10;
            this.mCols = AppSettings.Settings["cols"] != null ?
                                int.Parse(AppSettings.Settings["cols"]) : 10;
            // create the needed amount of cells to draw the maze
            mazeCells = new Border[mRows * 2 - 1, mCols * 2 - 1];
            // calculate widths and heights of the cells
            double x = canvasWidth / ((double)(3 * mCols - 1));
            double y = canvasHeight / ((double)(3 * mRows - 1));
            double posX = 0, posY = 0;
            this.backgroundColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ededed"));
            this.borderColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#218781"));

            for (int i = 0; i < mRows * 2 - 1; ++i)
            {
                double height = 0;
                posX = 0;
                for (int j = 0; j < mCols * 2 - 1; ++j)
                {
                    double width = 2 * x;
                    height = 2 * y;
                    // check the type of cell and match the height and width accordingly
                    if (i % 2 == 1) height = y;
                    if (j % 2 == 1) width = x;
                    mazeCells[i, j] = this.GetBorder(this.backgroundColor,
                                                    posX, posY, width, height);
                    this.canvas.Children.Add(mazeCells[i, j]);
                    posX += width;
                }
                // increase posY with the height of the previus row
                posY += height;
            }
            // create the player circle
            this.player = new Ellipse();
            this.player.Fill = Brushes.Black;
            this.player.Width = x < y ? 2 * x : 2 * y;
            this.player.Height = x < y ? 2 * x : 2 * y;
        }

        /// <summary>
        /// Draws the player on the maze.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        private void DrawPlayer(int row, int col)
        {
            this.canvas.Children.Remove(this.player);
            // calculate the relative position on the player on the canvas
            Point relativePoint = this.mazeCells[row, col]
                            .TransformToAncestor(this.canvas)
                          .Transform(new Point(0, 0));
            Canvas.SetLeft(this.player, relativePoint.X
                + (this.mazeCells[row, col].Width - this.player.Width) / 2);
            Canvas.SetTop(this.player, relativePoint.Y
                + (this.mazeCells[row, col].Height - this.player.Height) / 2);
            this.canvas.Children.Add(this.player);
            if (this.lastHintCol != -1)
                // hide the hint
                this.mazeCells[lastHintRow, lastHintCol].Background
                                                        = this.backgroundColor;
        }

        /// <summary>
        /// Draws the hint on the maze.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        private void DrawHint(int row, int col)
        {
            this.mazeCells[row, col].Background = Brushes.Tomato;
            this.lastHintRow = row;
            this.lastHintCol = col;
        }

        /// <summary>
        /// Draws the maze.
        /// </summary>
        /// <param name="maze">The maze.</param>
        private void DrawMaze(string maze)
        {
            // change the maze string into a matrix array of chars
            char[,] matrix = this.stringMazeToCharMatrix(maze,
                                    this.mRows * 2 - 1, this.mCols * 2 - 1);
            for (int i = 0; i < mRows * 2 - 1; ++i)
                for (int j = 0; j < mCols * 2 - 1; ++j)
                {
                    if (matrix[i, j] == '1')
                    {
                        // draw as a wall
                        double bottom = 2, top = 2, left = 2, right = 2;

                        if (j > 0 && matrix[i, j - 1] == '1') left = 0;
                        if (j + 1 < mCols * 2 - 1 && matrix[i, j + 1] == '1')
                            right = 0;
                        if (i > 0 && matrix[i - 1, j] == '1') top = 0;
                        if (i + 1 < mRows * 2 - 1 && matrix[i + 1, j] == '1')
                            bottom = 0;

                        this.mazeCells[i, j].Background = Brushes.Gray;
                        this.mazeCells[i, j].BorderBrush = this.borderColor;
                        this.mazeCells[i, j].BorderThickness = new Thickness(left, top, right, bottom);
                    }
                    else
                    {
                        // draw as a passage
                        double bottom = 1, top = 1, left = 1, right = 1;

                        if (j > 0 && matrix[i, j - 1] != '1') left = 0;
                        if (j + 1 < mCols * 2 - 1 && matrix[i, j + 1] != '1')
                            right = 0;
                        if (i > 0 && matrix[i - 1, j] != '1') top = 0;
                        if (i + 1 < mRows * 2 - 1 && matrix[i + 1, j] != '1')
                            bottom = 0;

                        this.mazeCells[i, j].Background = this.backgroundColor;
                        this.mazeCells[i, j].BorderBrush = this.borderColor;
                        this.mazeCells[i, j].BorderThickness = new Thickness(left, top, right, bottom);
                    }
                    if (matrix[i, j] == '*')
                    {
                        // draw starting position
                        this.mazeCells[i, j].Background = Brushes.LawnGreen;
                        this.mazeCells[i, j].BorderThickness = new Thickness(2);
                        this.mazeCells[i, j].BorderBrush = Brushes.Green;
                        startCol = j;
                        startRow = i;
                    }
                    else if (matrix[i, j] == '#')
                    {
                        // draw end position
                        this.mazeCells[i, j].Background = Brushes.CadetBlue;
                        this.mazeCells[i, j].BorderThickness = new Thickness(2);
                        this.mazeCells[i, j].BorderBrush = this.borderColor;
                        endCol = j;
                        endRow = i;
                    }
                }
        }

        /// <summary>
        /// Gets a Border Component.
        /// </summary>
        /// <param name="b">The brush.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        private Border GetBorder(Brush b, double x, double y,
                                                double width, double height)
        {
            Border bor = new Border();
            bor.Width = width;
            bor.Height = height;
            Canvas.SetLeft(bor, x);
            Canvas.SetTop(bor, y);
            bor.Background = b;

            return bor;
        }

        /// <summary>
        /// changes the string maze to character matrix.
        /// </summary>
        /// <param name="m">The maze string.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        private char[,] stringMazeToCharMatrix(string m, int rows, int cols)
        {
            char[,] matrix = new char[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix[i, j] = m[i * cols + j];
            return matrix;
        }

        /// <summary>
        /// Mazes Label the text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void MazeTextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text.Length > 0)
                this.DrawMaze((sender as TextBox).Text);
        }

        /// <summary>
        /// Positions Label the text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void PositionTextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text.Length > 0)
            {
                string[] position = (sender as TextBox).Text
                                    .Split(new char[] { ',' });
                int row = int.Parse(position[0]);
                int col = int.Parse(position[1]);
                this.DrawPlayer(row, col);

                this.mazeCells[startRow, startCol].Background
                                                        = Brushes.GreenYellow;
                this.mazeCells[endRow, endCol].Background
                                                        = Brushes.BlueViolet;
            }
        }

        /// <summary>
        /// Hint Label's text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void HintTextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text.Length > 0)
            {
                string[] position = (sender as TextBox).Text
                                                    .Split(new char[] { ',' });
                int row = int.Parse(position[0]);
                int col = int.Parse(position[1]);
                this.DrawHint(row, col);
            }
        }

        /// <summary>
        /// Won Label's text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void WonTextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text.ToLower() == "true")
            {
                this.cTxt1.Visibility = Visibility.Visible;
                this.cTxt2.Visibility = Visibility.Visible;
            }
            else
            {
                this.cTxt1.Visibility = Visibility.Hidden;
                this.cTxt2.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Lost Label's text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void LostTextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text.ToLower() == "true")
            {
                this.lostTextUpper.Visibility = Visibility.Visible;
            }
            else
            {
                this.lostTextUpper.Visibility = Visibility.Hidden;
            }
        }
    }
}
