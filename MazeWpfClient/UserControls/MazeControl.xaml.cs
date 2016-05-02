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
        private Ellipse hint;

        private int canvasWidth = 480, canvasHeight = 250;

        private Border[,] mazeCells;

        private int mCols;
        private int mRows;

        public MazeControl()
        {
            InitializeComponent();

            this.mRows = AppSettings.Settings["rows"] != null ? int.Parse(AppSettings.Settings["rows"]) : 8;
            this.mCols = AppSettings.Settings["cols"] != null ? int.Parse(AppSettings.Settings["cols"]) : 24;
            mazeCells = new Border[mRows * 2 - 1, mCols * 2 - 1];
            double x = canvasWidth / ((double)(3 * mCols - 1));
            double y = canvasHeight / ((double)(3 * mRows - 1));
            double posX = 0, posY = 0;
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
                    mazeCells[i, j] = this.GetBorder(Brushes.Bisque, posX, posY, width, height);
                    this.canvas.Children.Add(mazeCells[i, j]);
                    posX += width;
                }
                posY += height;
            }
            this.player = new Ellipse();
            this.player.Fill = Brushes.Black;
            this.player.Width = 20;
            this.player.Height = 20;

            this.hint = new Ellipse();
            this.hint.Fill = Brushes.OrangeRed;
            this.hint.Width = 30;
            this.hint.Height = 30;

            double www = this.canvas.Height;
            www = this.canvas.ActualHeight;
        }

        private void DrawPlayer(int row, int col)
        {
            this.canvas.Children.Remove(this.player);
            Canvas.SetLeft(this.player, ((col / 2) * (canvasWidth / 24)));
            Canvas.SetTop(this.player, ((row / 2) * (canvasHeight / 8)));
            this.canvas.Children.Add(this.player);
        }

        private void DrawHint(int row, int col)
        {
            this.canvas.Children.Remove(this.hint);
            Canvas.SetLeft(this.hint, ((col / 2) * (canvasWidth / 24)));
            Canvas.SetTop(this.hint, ((row / 2) * (canvasHeight / 8)));
            this.canvas.Children.Add(this.hint);
            if (this.canvas.Children.Contains(this.player))
            {
                this.canvas.Children.Remove(this.player);
                this.canvas.Children.Add(this.player);
            }
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

                        this.mazeCells[i, j].Background = Brushes.Cornsilk;
                        this.mazeCells[i, j].BorderBrush = Brushes.Navy;
                        this.mazeCells[i, j].BorderThickness = new Thickness(left, top, right, bottom);
                    }
                }
        }

        private void AddBorders()
        {
            // upper
            Line upper = this.GetLine();
            upper.X1 = 0;
            upper.X2 = canvasWidth;
            upper.Y1 = upper.Y2 = 0;
            this.canvas.Children.Add(upper);

            // lower
            Line lower = this.GetLine();
            lower.X1 = 0;
            lower.X2 = canvasWidth;
            lower.Y1 = lower.Y2 = canvasHeight;
            this.canvas.Children.Add(lower);

            // right
            Line right = this.GetLine();
            right.X1 = right.X2 = 0;
            right.Y1 = 0;
            right.Y2 = canvasHeight;
            this.canvas.Children.Add(right);

            // left
            Line left = this.GetLine();
            left.X1 = left.X2 = canvasWidth;
            left.Y1 = 0;
            left.Y2 = canvasHeight;
            this.canvas.Children.Add(left);
        }

        private Line GetLine()
        {
            Line line = new Line();
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 5.5;
            return line;
        }

        private Rectangle GetRectangle(Brush b, double x, double y, double width, double height)
        {
            Rectangle rec = new Rectangle();
            rec.Width = width;
            rec.Height = height;
            Canvas.SetLeft(rec, x);
            Canvas.SetTop(rec, y);
            rec.Fill = b;
            rec.StrokeThickness = 5;
            return rec;
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
            }
        }

        private void HintTextChanged(object sender, TextChangedEventArgs e)
        {
            this.canvas.Children.Remove(this.hint);
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
            int width = canvasWidth / 23;
            int height = canvasHeight / 8;
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    if (matrix[i, j] == '2')
                    {
                        Rectangle rec2 = this.GetRectangle(Brushes.PaleVioletRed, (j / 2) * width + 6, (i / 2) * height + 6, width - 12, height - 12);
                        this.canvas.Children.Add(rec2);
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
