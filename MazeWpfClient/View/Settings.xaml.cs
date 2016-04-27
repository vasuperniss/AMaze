using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            {
                this.IP_text.Background = (Brush)bc.ConvertFrom(errorColor);
                this.IP_text.Text = "Invalid IP Address!";
            }
            if (!portRet)
            {
                this.Port_text.Background = (Brush)bc.ConvertFrom(errorColor);
                this.Port_text.Text = "Invalid Port!";
            }
            if (!portRet || !ipRet) return;

            // update appSettings.
            AppSettings.ModifySetting("ip", ip.ToString());
            AppSettings.ModifySetting("port", port.ToString());
            this.Close();
        }

        private void Port_text_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Port_text.Background = Brushes.White;
            this.Port_text.Text = "";
        }

        private void IP_text_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.IP_text.Background = Brushes.White;
            this.IP_text.Text = "";
        }
    }
}
