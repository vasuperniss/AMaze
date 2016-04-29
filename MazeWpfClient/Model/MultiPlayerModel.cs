using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeWpfClient.Model.Answer;
using MazeWpfClient.Model.Server;

namespace MazeWpfClient.Model
{
    class MultiPlayerModel : IMultiPlayerModel
    {
        private IServer server;
        private JsonAnswerFactory answersFactory;
        private MultiPlayerMaze multiPlayerMaze;

        private int cols;
        private int rows;
        private bool showSolution;
        private bool showHint;

        public event PropertyChangedEventHandler PropertyChanged;

        public MultiPlayerModel(IServer server)
        {
            this.server = server;
            this.server.OnResponseReceived += this.ServerResponseHandler;
            this.cols = 24;
            this.rows = 8;
            this.answersFactory = new JsonAnswerFactory();
        }

        public string GameName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public MazePosition Hint
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string MazeName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string MazeString
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public MazePosition OpponentPosition
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public MazePosition PlayerPosition
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string SolutionString
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool WonGame
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void GetHint()
        {
            throw new NotImplementedException();
        }

        public void LoadNewGame(string mazeName)
        {
            throw new NotImplementedException();
        }

        public void MakeMove(Move move)
        {
            throw new NotImplementedException();
        }

        public void SolveMaze()
        {
            throw new NotImplementedException();
        }

        private void ServerResponseHandler(object sender, ResponseEventArgs args)
        {
        }
    }
}
