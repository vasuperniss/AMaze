using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MazeWpfClient.View
{
    /// <summary>
    /// Wrapper for MediaPlayer
    /// </summary>
    public class MusicPlayer
    {
        /// <summary>
        /// The music player
        /// </summary>
        private MediaPlayer player;
        /// <summary>
        /// State of player
        /// </summary>
        private bool playing;
        /// <summary>
        /// The path of the music file
        /// </summary>
        private Uri path;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicPlayer"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public MusicPlayer(string path)
        {
            this.path = new Uri(path, UriKind.RelativeOrAbsolute);
            this.player = new MediaPlayer();
            this.player.Open(this.path);
            this.player.MediaEnded += this.Media_Ended;
        }

        /// <summary>
        /// Plays music.
        /// </summary>
        public void Play()
        {
            this.player.Play();
            this.playing = true;
        }

        /// <summary>
        /// Pauses music.
        /// </summary>
        public void Pause()
        {
            this.player.Pause();
            this.playing = false;
        }

        /// <summary>
        /// Stops music.
        /// </summary>
        public void Stop()
        {
            this.player.Stop();
            this.playing = false;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="MusicPlayer"/> is playing.
        /// </summary>
        /// <value>
        ///   <c>true</c> if playing; otherwise, <c>false</c>.
        /// </value>
        public bool Playing
        {
            get { return this.playing; }
        }

        /// <summary>
        /// Handles the Ended event of the Media control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Media_Ended(object sender, EventArgs e)
        {
            this.player.Position = TimeSpan.Zero;
            this.player.Play();
        }
    }
}
