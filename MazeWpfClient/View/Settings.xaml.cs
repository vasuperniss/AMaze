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
        public Settings()
        {
            InitializeComponent();
            this.IP_text.Text = AppSettings.Settings["ip"];
            this.Port_text.Text = AppSettings.Settings["port"];
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

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
            //AppSettings.ModifySetting("ip", ip.ToString());
            //AppSettings.ModifySetting("port", port.ToString());

            AppSettings.Settings["ip"] = this.IP_text.Text;
            AppSettings.Settings["port"] = this.Port_text.Text;

            this.Close();
        }

        private void Port_text_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Port_text.Background = Brushes.White;
        }

        private void IP_text_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.IP_text.Background = Brushes.White;
        }
    }
}
