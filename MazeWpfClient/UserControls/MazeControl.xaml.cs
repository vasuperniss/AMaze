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
        private string maze;

        public MazeControl()
        {
            InitializeComponent();
        }

        private void canvasContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.maze = e.NewValue as string;
            //this.maze = "01000101010" + "01010101010" + "00010101010" + "11110101010" + "00000101010" + "01111101010" + "00000001010";
            //this.maze = "01000101010" + "01000101010" + "01000101010" + "01000101010" + "01000101010" + "01000101010" + "01000101010"
            //   + "01000101010" + "01000101010" + "01000101010" + "01000101010";
            int rows = 2 * 8 - 1;
            int cols = 2 * 24 - 1;
            char[,] matrix = this.stringMazeToIntMatrix(this.maze, rows, cols);
            int width = (int)this.canvas.ActualWidth / 24;
            int height = (int)this.canvas.ActualHeight / 8;
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    if (matrix[i, j] == '1' && (i % 2 == 1 ^ j % 2 == 1))
                    {
                        Line line = this.GetLine();
                        // p1 & p2
                        if (i%2 == 1)
                        {
                            line.Y1 = (line.Y2 = (height) * ((i + 1) / 2));
                            line.X1 = (width) * (j / 2);
                            line.X2 = (width) * (j / 2 + 1) + 2;
                        }
                        else
                        {
                            line.X1 = (line.X2 = (width) * ((j + 1) / 2));
                            line.Y1 = (height) * (i / 2);
                            line.Y2 = (height) * (i / 2 + 1) + 2;
                        }
                        this.canvas.Children.Add(line);
                    }
                    else if (matrix[i, j] == '*')
                    {
                        Rectangle rec = this.GetRectangle(Brushes.Aquamarine, (j / 2) * width, (i / 2) * height, width, height);
                        this.canvas.Children.Add(rec);
                        Rectangle rec2 = this.GetRectangle(Brushes.Azure, (j / 2) * width + 6, (i / 2) * height + 6, width - 12, height - 12);
                        this.canvas.Children.Add(rec2);
                    }
                    else if (matrix[i, j] == '#')
                    {
                        Rectangle rec = this.GetRectangle(Brushes.Tomato, (j / 2) * width, (i / 2) * height, width, height);
                        this.canvas.Children.Add(rec);
                        Rectangle rec2 = this.GetRectangle(Brushes.Tan, (j / 2) * width + 6, (i / 2) * height + 6, width - 12, height - 12);
                        this.canvas.Children.Add(rec2);
                    }
                }
            }
            this.AddBorders();
        }

        private void AddBorders()
        {
            // upper
            Line upper = this.GetLine();
            upper.X1 = 0;
            upper.X2 = this.canvas.ActualWidth;
            upper.Y1 = upper.Y2 = 0;
            this.canvas.Children.Add(upper);

            // lower
            Line lower = this.GetLine();
            lower.X1 = 0;
            lower.X2 = this.canvas.ActualWidth;
            lower.Y1 = lower.Y2 = this.canvas.ActualHeight;
            this.canvas.Children.Add(lower);

            // right
            Line right = this.GetLine();
            right.X1 = right.X2 = 0;
            right.Y1 = 0;
            right.Y2 = this.canvas.ActualHeight;
            this.canvas.Children.Add(right);

            // left
            Line left = this.GetLine();
            left.X1 = left.X2 = this.canvas.ActualWidth;
            left.Y1 = 0;
            left.Y2 = this.canvas.ActualHeight;
            this.canvas.Children.Add(left);
        }

        private Line GetLine()
        {
            Line line = new Line();
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 5;
            return line;
        }

        private Rectangle GetRectangle(Brush b, int x, int y, int width, int height)
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

        private char[,] stringMazeToIntMatrix(string m, int rows, int cols)
        {
            char[,] matrix = new char[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix[i, j] = m[i * cols + j];
            return matrix;
        }
    }
}
