using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for error.xaml
    /// </summary>
    public partial class errorDark : Window {
        HomePageDark _homepage;
        EditPageDark _editpage;
        CalendarPageDark _calendarPage;
        public static string emailDataPath = Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("bin")) + "\\Data\\email.txt";

        public errorDark(HomePageDark homepage, EditPageDark editpage, CalendarPageDark calendarPage) {
            _homepage = homepage;
            _editpage = editpage;
            _calendarPage = calendarPage;
            InitializeComponent();
        }

        private void Okay_Click(object sender, RoutedEventArgs e) {
            Close(); 
        }
    }
}
