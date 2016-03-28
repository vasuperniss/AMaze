using System;

namespace MazeClient.Model.Answer
{
    class MultiplayerAnswer : IServerAnswer
    {
        private string gameName;
        private string mazeName;
        private GenerateAnswer myMaze;
        private GenerateAnswer othersMaze;

        public string Name
        {
            get { return this.gameName; }
            set { this.gameName = value; }
        }

        public string MazeName
        {
            get { return this.mazeName; }
            set { this.mazeName = value; }
        }

        public GenerateAnswer You
        {
            get { return this.myMaze; }
            set { this.myMaze = value; }
        }

        public GenerateAnswer Other
        {
            get { return this.othersMaze; }
            set { this.othersMaze = value; }
        }

        public override string ToString()
        {
            return string.Format("Multiplayer Game, game-name: {0}, maze name {1}\n"
                                +"Your maze: {2}\nOther player's maze: {3}",this.gameName,
                                this.mazeName, this.myMaze, this.othersMaze);
        }
    }
}
