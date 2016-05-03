using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MazeWpfClient.View
{
    public class MusicPlayer
    {
        private MediaPlayer player;
        private bool playing;
        private Uri path;

        public MusicPlayer(string path)
        {
            this.path = new Uri(path, UriKind.RelativeOrAbsolute);
            this.player = new MediaPlayer();
            this.player.Open(this.path);
            this.player.MediaEnded += this.Media_Ended;
        }

        public void Play()
        {
            this.player.Play();
            this.playing = true;
        }

        public void Pause()
        {
            this.player.Pause();
            this.playing = false;
        }

        public void Stop()
        {
            this.player.Stop();
            this.playing = false;
        }

        public bool Playing
        {
            get { return this.playing; }
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            this.player.Position = TimeSpan.Zero;
            this.player.Play();
        }
    }
}
