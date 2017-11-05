using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Demo;

namespace MutiThreadWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StatusLabel.Content = "Ping......";
            //Task task = Task.Run(() => MultiThreads.ShowMultiThread()).ContinueWith(FormerTask =>
            //    {
            //        Dispatcher.BeginInvoke((Action)(() =>
            //            {
            //                StatusLabel.Content = "Finished";
            //            }));

            //    });
            Ping ping = new Ping();
            PingReply pingReplay = await ping.SendPingAsync("www.IntelliTect.com");
            StatusLabel.Content = pingReplay.Status.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GameTreeSearch GTS = new GameTreeSearch();
            GTS.ShowDialog();
        }

        private void btNetwork_Click(object sender, RoutedEventArgs e)
        {
            Network NT = new Network();
            NT.ShowDialog();
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
