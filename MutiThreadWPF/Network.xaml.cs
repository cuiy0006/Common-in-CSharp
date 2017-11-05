using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MutiThreadWPF
{
    /// <summary>
    /// Interaction logic for Network.xaml
    /// </summary>
    public partial class Network : Window
    {
        public Network()
        {
            InitializeComponent();
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bt01_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btGameBoard_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            if (bt != null)
            { 
                
            
            }
        }
    }
}
