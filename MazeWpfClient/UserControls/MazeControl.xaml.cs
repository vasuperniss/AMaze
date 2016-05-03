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
        private Ellipse player;

        private int canvasWidth = 480, canvasHeight = 250;

        private Border[,] mazeCells;

        private int mCols;
        private int mRows;

        private int endCol, endRow;
        private int startCol, startRow;

        private int lastHintRow = -1, lastHintCol = -1;
        private Brush backgroundColor;

        public MazeControl()
        {
            InitializeComponent();

            this.mRows = AppSettings.Settings["rows"] != null ? int.Parse(AppSettings.Settings["rows"]) : 8;
            this.mCols = AppSettings.Settings["cols"] != null ? int.Parse(AppSettings.Settings["cols"]) : 24;
            mazeCells = new Border[mRows * 2 - 1, mCols * 2 - 1];
            double x = canvasWidth / ((double)(3 * mCols - 1));
            double y = canvasHeight / ((double)(3 * mRows - 1));
            double posX = 0, posY = 0;
            this.backgroundColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ededed"));

            for (int i = 0; i < mRows * 2 - 1; ++i)
            {
                double height = 0;
                posX = 0;
                for (int j = 0; j < mCols * 2 - 1; ++j)
                {
                    double width = 2 * x;
                    height = 2 * y;
                    if (i % 2 == 1) height = y;
                    if (j % 2 == 1) width = x;
                    mazeCells[i, j] = this.GetBorder(this.backgroundColor, posX, posY, width, height);
                    this.canvas.Children.Add(mazeCells[i, j]);
                    posX += width;
                }
                posY += height;
            }
            this.player = new Ellipse();
            this.player.Fill = Brushes.Black;
            this.player.Width = x < y ? 2 * x : 2 * y;
            this.player.Height = x < y ? 2 * x : 2 * y;
        }

        private void DrawPlayer(int row, int col)
        {
            this.canvas.Children.Remove(this.player);
            Point relativePoint = this.mazeCells[row, col].TransformToAncestor(this.canvas)
                          .Transform(new Point(0, 0));
            Canvas.SetLeft(this.player, relativePoint.X + (this.mazeCells[row, col].Width - this.player.Width) / 2);
            Canvas.SetTop(this.player, relativePoint.Y + (this.mazeCells[row, col].Height - this.player.Height) / 2);
            this.canvas.Children.Add(this.player);
            if (this.lastHintCol != -1)
                this.mazeCells[lastHintRow, lastHintCol].Background = this.backgroundColor;
        }

        private void DrawHint(int row, int col)
        {
            this.mazeCells[row, col].Background = Brushes.Tomato;
            this.lastHintRow = row;
            this.lastHintCol = col;
        }

        private void DrawMaze(string maze)
        {
            char[,] matrix = this.stringMazeToIntMatrix(maze, this.mRows * 2 - 1, this.mCols * 2 - 1);
            for (int i = 0; i < mRows * 2 - 1; ++i)
                for (int j = 0; j < mCols * 2 - 1; ++j)
                {
                    if (matrix[i, j] == '1')
                    {
                        double bottom = 2, top = 2, left = 2, right = 2;

                        if (j > 0 && matrix[i, j - 1] == '1') left = 0;
                        if (j + 1 < mCols * 2 - 1 && matrix[i, j + 1] == '1') right = 0;

                        if (i > 0 && matrix[i - 1, j] == '1') top = 0;
                        if (i + 1 < mRows * 2 - 1 && matrix[i + 1, j] == '1') bottom = 0;

                        this.mazeCells[i, j].Background = Brushes.Gray;
                        this.mazeCells[i, j].BorderBrush = Brushes.Navy;
                        this.mazeCells[i, j].BorderThickness = new Thickness(left, top, right, bottom);
                    }
                    else
                    {
                        double bottom = 1, top = 1, left = 1, right = 1;

                        if (j > 0 && matrix[i, j - 1] != '1') left = 0;
                        if (j + 1 < mCols * 2 - 1 && matrix[i, j + 1] != '1') right = 0;

                        if (i > 0 && matrix[i - 1, j] != '1') top = 0;
                        if (i + 1 < mRows * 2 - 1 && matrix[i + 1, j] != '1') bottom = 0;

                        this.mazeCells[i, j].Background = this.backgroundColor;
                        this.mazeCells[i, j].BorderBrush = Brushes.Navy;
                        this.mazeCells[i, j].BorderThickness = new Thickness(left, top, right, bottom);
                    }
                    if (matrix[i, j] == '*')
                    {
                        this.mazeCells[i, j].Background = Brushes.LawnGreen;
                        this.mazeCells[i, j].BorderThickness = new Thickness(2);
                        this.mazeCells[i, j].BorderBrush = Brushes.Green;
                        startCol = j;
                        startRow = i;
                    }
                    else if (matrix[i, j] == '#')
                    {
                        this.mazeCells[i, j].Background = Brushes.CadetBlue;
                        this.mazeCells[i, j].BorderThickness = new Thickness(2);
                        this.mazeCells[i, j].BorderBrush = Brushes.Blue;
                        endCol = j;
                        endRow = i;
                    }
                }
        }

        private Border GetBorder(Brush b, double x, double y, double width, double height)
        {
            Border bor = new Border();
            bor.Width = width;
            bor.Height = height;
            Canvas.SetLeft(bor, x);
            Canvas.SetTop(bor, y);
            bor.Background = b;

            return bor;
        }

        private char[,] stringMazeToIntMatrix(string m, int rows, int cols)
        {
            char[,] matrix = new char[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix[i, j] = m[i * cols + j];
            return matrix;
        }

        private void MazeTextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text.Length > 0)
                this.DrawMaze((sender as TextBox).Text);
        }

        private void PositionTextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text.Length > 0)
            {
                string[] position = (sender as TextBox).Text.Split(new char[] { ',' });
                int row = int.Parse(position[0]);
                int col = int.Parse(position[1]);
                this.DrawPlayer(row, col);

                this.mazeCells[startRow, startCol].Background = Brushes.GreenYellow;
                this.mazeCells[endRow, endCol].Background = Brushes.BlueViolet;
            }
        }

        private void HintTextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text.Length > 0)
            {
                string[] position = (sender as TextBox).Text.Split(new char[] { ',' });
                int row = int.Parse(position[0]);
                int col = int.Parse(position[1]);
                this.DrawHint(row, col);
            }
        }

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

        private void SolutionTextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text.Length > 0)
            {
                this.DrawSolution((sender as TextBox).Text);
            }
        }

        private void DrawSolution(string solution)
        {
            int rows = 2 * 8 - 1;
            int cols = 2 * 24 - 1;
            char[,] matrix = this.stringMazeToIntMatrix(solution, rows, cols);
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    if (matrix[i, j] == '2')
                    {
                        this.mazeCells[i, j].Background = Brushes.Violet;
                    }
                }
            }
            if (this.canvas.Children.Contains(this.player))
            {
                this.canvas.Children.Remove(this.player);
                this.canvas.Children.Add(this.player);
            }
        }
    }
}
