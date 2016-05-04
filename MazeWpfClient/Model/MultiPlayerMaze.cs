using MazeWpfClient.Model.Answer;
using MazeWpfClient.Model.Server;
using System.Collections.Generic;
using System;

namespace MazeWpfClient.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class MultiPlayerMaze
    {
        /// <summary>
        /// The answer to all questions.
        /// </summary>
        private MultiplayerAnswer answer;
        /// <summary>
        /// The server.
        /// </summary>
        private IServer server;
        /// <summary>
        /// The player position
        /// </summary>
        private MazePosition playerPosition;
        /// <summary>
        /// The opponenet position
        /// </summary>
        private MazePosition opponenetPosition;
        /// <summary>
        /// The rows
        /// </summary>
        private int rows;
        /// <summary>
        /// The columns
        /// </summary>
        private int cols;
        /// <summary>
        /// The solution
        /// </summary>
        private string solution = "";
        /// <summary>
        /// The solution path
        /// </summary>
        private List<MazePosition> solutionPath;
        /// <summary>
        /// The hint index
        /// </summary>
        private int hintIndex = 0;
        /// <summary>
        /// The hint
        /// </summary>
        private MazePosition hint;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerMaze"/> class.
        /// </summary>
        /// <param name="answer">The answer.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The columns.</param>
        /// <param name="server">The server.</param>
        public MultiPlayerMaze(MultiplayerAnswer answer, int rows, int cols, IServer server)
        {
            this.server = server;
            this.answer = answer;
            this.cols = cols;
            this.rows = rows;
            this.playerPosition = new MazePosition(this.answer.You.Start);
            this.opponenetPosition = new MazePosition(this.answer.Other.Start);
            this.hint = new MazePosition(this.answer.You.Start);
        }

        /// <summary>
        /// Gets the name of the game.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return this.answer.Name; }
        }

        /// <summary>
        /// Gets the player's maze.
        /// </summary>
        /// <value>
        /// The player maze.
        /// </value>
        public string PlayerMaze
        {
            get { return this.answer.You.Maze; }
        }

        /// <summary>
        /// Gets the opponent's maze.
        /// </summary>
        /// <value>
        /// The opponent maze.
        /// </value>
        public string OpponentMaze
        {
            get { return this.answer.Other.Maze; }
        }

        /// <summary>
        /// Gets the name of the player's maze.
        /// </summary>
        /// <value>
        /// The name of the player maze.
        /// </value>
        public string PlayerMazeName
        {
            get { return this.answer.You.Name; }
        }

        /// <summary>
        /// Gets the name of the opponent's maze.
        /// </summary>
        /// <value>
        /// The name of the opponent maze.
        /// </value>
        public string OpponentMazeName
        {
            get { return this.answer.Other.Name; }
        }

        /// <summary>
        /// Gets the player start point.
        /// </summary>
        /// <value>
        /// The player start.
        /// </value>
        public MazePosition PlayerStart
        {
            get { return this.answer.You.Start; }
        }

        /// <summary>
        /// Gets the player end point.
        /// </summary>
        /// <value>
        /// The player end.
        /// </value>
        public MazePosition PlayerEnd
        {
            get { return this.answer.You.End; }
        }

        /// <summary>
        /// Gets the opponent start point.
        /// </summary>
        /// <value>
        /// The opponent start.
        /// </value>
        public MazePosition OpponentStart
        {
            get { return this.answer.Other.Start; }
        }

        /// <summary>
        /// Gets the opponent end point.
        /// </summary>
        /// <value>
        /// The opponent end.
        /// </value>
        public MazePosition OpponentEnd
        {
            get { return this.answer.Other.End; }
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
            set { this.playerPosition = value; }
        }

        /// <summary>
        /// Gets or sets the opponent position.
        /// </summary>
        /// <value>
        /// The opponent position.
        /// </value>
        public MazePosition OpponentPosition
        {
            get { return this.opponenetPosition; }
            set { this.opponenetPosition = value; }
        }

        /// <summary>
        /// Gets the start point of the player's maze.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public MazePosition Start
        {
            get { return this.answer.You.Start; }
        }

        /// <summary>
        /// Gets the end point of the player's maze.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public MazePosition End
        {
            get { return this.answer.You.End; }
        }

        /// <summary>
        /// Gets or sets the player solution.
        /// </summary>
        /// <value>
        /// The player solution.
        /// </value>
        public string PlayerSolution
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
        /// Moves the player and updates the server.
        /// </summary>
        /// <param name="move">The move.</param>
        /// <returns></returns>
        public bool Move(Move move)
        {
            bool moved = false;
            string direction = "play "+Enum.GetName(typeof(Move),move).ToLower();

            switch (move)
            {
                case Model.Move.Up:
                    if (this.playerPosition.Row > 0)
                    {
                        if (this.PlayerMaze[(this.playerPosition.Row - 1) * (this.cols * 2 - 1) + this.playerPosition.Col] == '0')
                        {
                            // move the player upwards and update the server.
                            this.PlayerPosition.Row -= 2;
                            moved = true;
                            this.server.SendRequest(direction);
                        }
                    }
                    break;
                case Model.Move.Down:
                    if (this.playerPosition.Row < this.rows * 2 - 2)
                    {
                        if (this.PlayerMaze[(this.playerPosition.Row + 1) * (this.cols * 2 - 1) + this.playerPosition.Col] == '0')
                        {
                            // move the player downwards and update the server.
                            this.PlayerPosition.Row += 2;
                            moved = true;
                            this.server.SendRequest(direction);
                        }
                    }
                    break;
                case Model.Move.Right:
                    if (this.playerPosition.Col < this.cols * 2 - 2)
                    {
                        if (this.PlayerMaze[this.playerPosition.Row * (this.cols * 2 - 1) + this.playerPosition.Col + 1] == '0')
                        {
                            // move the player to the right and update the server.
                            this.PlayerPosition.Col += 2;
                            moved = true;
                            this.server.SendRequest(direction);
                        }
                    }
                    break;
                case Model.Move.Left:
                    if (this.playerPosition.Col > 0)
                    {
                        if (this.PlayerMaze[this.playerPosition.Row * (this.cols * 2 - 1) + this.playerPosition.Col - 1] == '0')
                        {
                            // move the player to the left and update the server.
                            this.PlayerPosition.Col -= 2;
                            moved = true;
                            this.server.SendRequest(direction);
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
    }
}