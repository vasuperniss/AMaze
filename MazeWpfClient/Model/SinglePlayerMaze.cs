using MazeWpfClient.Model.Answer;
using System.Collections.Generic;

namespace MazeWpfClient.Model
{
    public class SinglePlayerMaze
    {
        /// <summary>
        /// The generate answer that was given to start the maze
        /// </summary>
        private GenerateAnswer answer;
        /// <summary>
        /// The player's position
        /// </summary>
        private MazePosition playerPosition;
        /// <summary>
        /// The rows in the maze
        /// </summary>
        private int rows;
        /// <summary>
        /// The cols in the maze
        /// </summary>
        private int cols;
        /// <summary>
        /// The solution string
        /// </summary>
        private string solution = "";
        /// <summary>
        /// The solution List of Positions path
        /// </summary>
        private List<MazePosition> solutionPath;
        /// <summary>
        /// The hint
        /// </summary>
        private MazePosition hint;
        /// <summary>
        /// The hint index in the Solution List
        /// </summary>
        private int hintIndex = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerMaze"/> class.
        /// </summary>
        /// <param name="answer">The answer.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        public SinglePlayerMaze(GenerateAnswer answer, int rows, int cols)
        {
            this.answer = answer;
            this.cols = cols;
            this.rows = rows;
            // place the player at the start of the maze
            this.playerPosition = new MazePosition(this.answer.Start);
            this.hint = new MazePosition(this.answer.Start);
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return this.answer.Name; }
        }

        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public string Maze
        {
            get { return this.answer.Maze; }
        }

        /// <summary>
        /// Gets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public MazePosition Start
        {
            get { return this.answer.Start; }
        }

        /// <summary>
        /// Gets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public MazePosition End
        {
            get { return this.answer.End; }
        }

        /// <summary>
        /// Gets or sets the player position.
        /// </summary>
        /// <value>
        /// The player position.
        /// </value>
        public MazePosition PlayerPosition
        {
            get { return this.playerPosition; }
            set
            {
                this.playerPosition = value;
            }
        }

        /// <summary>
        /// Gets or sets the solution.
        /// </summary>
        /// <value>
        /// The solution.
        /// </value>
        public string Solution
        {
            get
            {
                return this.solution;
            }
            set
            {
                if (this.solution == value) return;
                this.solution = value;
                this.solutionPath = new List<MazePosition>();
                // start the solution path with the starting position
                this.solutionPath.Add(new MazePosition(this.Start));
                this.solutionPath.Add(new MazePosition(this.Start));
                // run through the solution string until the end of the maze is reached
                while (this.solutionPath[this.solutionPath.Count - 1].Row != this.End.Row
                    || this.solutionPath[this.solutionPath.Count - 1].Col != this.End.Col)
                {
                    MazePosition currPos = this.solutionPath[this.solutionPath.Count - 1];
                    MazePosition prevPos = this.solutionPath[this.solutionPath.Count - 2];
                    // look for the direction of the next maze
                    if (currPos.Row > 0
                        && (this.solution[(currPos.Row - 1) * (this.cols * 2 - 1) + (currPos.Col)] == '2'
                        || this.solution[(currPos.Row - 1) * (this.cols * 2 - 1) + (currPos.Col)] == '#') &&
                        (prevPos.Row != currPos.Row - 1 || prevPos.Col != currPos.Col))
                        // next solution cell is up
                        this.solutionPath.Add(new MazePosition(currPos.Row - 1, currPos.Col));
                    else if (currPos.Row < this.rows * 2 - 2
                        && (this.solution[(currPos.Row + 1) * (this.cols * 2 - 1) + (currPos.Col)] == '2'
                        || this.solution[(currPos.Row + 1) * (this.cols * 2 - 1) + (currPos.Col)] == '#') &&
                        (prevPos.Row != currPos.Row + 1 || prevPos.Col != currPos.Col))
                        // next solution cell is down
                        this.solutionPath.Add(new MazePosition(currPos.Row + 1, currPos.Col));
                    else if (currPos.Col > 0
                        && (this.solution[(currPos.Row) * (this.cols * 2 - 1) + (currPos.Col - 1)] == '2'
                        || this.solution[(currPos.Row) * (this.cols * 2 - 1) + (currPos.Col - 1)] == '#') &&
                        (prevPos.Row != currPos.Row || prevPos.Col != currPos.Col - 1))
                        // next solution cell is left
                        this.solutionPath.Add(new MazePosition(currPos.Row, currPos.Col - 1));
                    else
                        // next solution cell by illimination is on the right
                        this.solutionPath.Add(new MazePosition(currPos.Row, currPos.Col + 1));
                }
                this.solutionPath.RemoveAt(0);
                this.solutionPath.Add(this.End);
                this.solutionPath.Add(this.End);
                this.hint = this.solutionPath[2];
            }
        }

        /// <summary>
        /// Gets or sets the hint.
        /// </summary>
        /// <value>
        /// The hint.
        /// </value>
        public MazePosition Hint
        {
            get
            {
                return this.hint;
            }
            set
            {
                this.hint = value;
            }
        }

        /// <summary>
        /// Moves the specified move.
        /// </summary>
        /// <param name="move">The move.</param>
        /// <returns></returns>
        public bool Move(Move move)
        {
            bool moved = false;
            switch (move)
            {
                case Model.Move.Up:
                    if (this.playerPosition.Row > 0)
                    {
                        if (this.Maze[(this.playerPosition.Row - 1)
                            * (this.cols * 2 - 1) + this.playerPosition.Col] == '0')
                        {
                            // move the player one cell up
                            this.PlayerPosition.Row -= 2;
                            moved = true;
                        }
                    }
                    break;
                case Model.Move.Down:
                    if (this.playerPosition.Row < this.rows * 2 - 2)
                    {
                        if (this.Maze[(this.playerPosition.Row + 1)
                            * (this.cols * 2 - 1) + this.playerPosition.Col] == '0')
                        {
                            // move the player one cell down
                            this.PlayerPosition.Row += 2;
                            moved = true;
                        }
                    }
                    break;
                case Model.Move.Right:
                    if (this.playerPosition.Col < this.cols * 2 - 2)
                    {
                        if (this.Maze[this.playerPosition.Row
                            * (this.cols * 2 - 1) + this.playerPosition.Col + 1] == '0')
                        {
                            // move the player one cell right
                            this.PlayerPosition.Col += 2;
                            moved = true;
                        }
                    }
                    break;
                case Model.Move.Left:
                    if (this.playerPosition.Col > 0)
                    {
                        if (this.Maze[this.playerPosition.Row
                            * (this.cols * 2 - 1) + this.playerPosition.Col - 1] == '0')
                        {
                            // move the player one cell left
                            this.PlayerPosition.Col -= 2;
                            moved = true;
                        }
                    }
                    break;
            }
            bool onSolution = false;
            // change the new hint according to the new player position
            for (int i = 0; i < this.solutionPath.Count; i++)
                if (this.playerPosition.Row == this.solutionPath[i].Row &&
                    this.playerPosition.Col == this.solutionPath[i].Col)
                {
                    this.Hint = this.solutionPath[i + 2];
                    onSolution = true;
                    this.hintIndex = i;
                    break;
                }
            if (!onSolution)
                this.Hint = this.solutionPath[this.hintIndex];
            return moved;
        }

        /// <summary>
        /// Restarts this instance.
        /// </summary>
        internal void Restart()
        {
            this.playerPosition = new MazePosition(this.Start);
            this.hint = this.solutionPath[2];
            this.hintIndex = 2;
            this.hintIndex = 0;
        }
    }
}