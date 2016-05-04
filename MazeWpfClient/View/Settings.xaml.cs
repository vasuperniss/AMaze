using MazeWpfClient.Model.Server;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MazeWpfClient.View
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        /// <summary>
        /// The server
        /// </summary>
        private IServer server;
        /// <summary>
        /// The text box background color
        /// </summary>
        private Brush textBoxBackgroundColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        public Settings(IServer server)
        {
            InitializeComponent();
            this.IP_text.Text = AppSettings.Settings["ip"];
            this.Port_text.Text = AppSettings.Settings["port"];
            this.server = server;
            this.textBoxBackgroundColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#636363"));
        }

        /// <summary>
        /// Handles the Click event of the cancel button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the confirm button. Saves settings if they are valid.
        /// Notifies the user if they are invalid.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            IPAddress ip;
            int port;
            var bc = new BrushConverter();
            string errorColor = "#d66262";
            bool ipRet = IPAddress.TryParse(this.IP_text.Text, out ip);
            bool portRet = int.TryParse(this.Port_text.Text, out port);

            if (!ipRet)
                this.IP_text.Background = (Brush)bc.ConvertFrom(errorColor);
            if (!portRet)
                this.Port_text.Background = (Brush)bc.ConvertFrom(errorColor);
            if (!portRet || !ipRet) return;

            // update appSettings.
            AppSettings.Settings["ip"] = this.IP_text.Text;
            AppSettings.Settings["port"] = this.Port_text.Text;

            this.Close();
        }

        /// <summary>
        /// Changes textBox background color back to default.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Port_text_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Port_text.Background = this.textBoxBackgroundColor;
        }

        /// <summary>
        /// Changes textBox background color back to default.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void IP_text_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.IP_text.Background = this.textBoxBackgroundColor;
        }
    }
}
