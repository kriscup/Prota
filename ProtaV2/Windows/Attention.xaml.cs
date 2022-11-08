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

namespace ProtaV2.Windows {
    /// <summary>
    /// Interaction logic for Attention.xaml
    /// </summary>
    public partial class Attention : Window {
        public Attention() {
            InitializeComponent();
        }

        private void REconfirmYes_Click(object sender, RoutedEventArgs e) {

        }

        private void REconfirmNo_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
