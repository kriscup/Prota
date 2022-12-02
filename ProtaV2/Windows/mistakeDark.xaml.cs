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
    /// Interaction logic for mistakeDark.xaml
    /// </summary>
    public partial class mistakeDark : Window {
        HomePageDark _homepage;
        EditPageDark _editpage;
        CalendarPageDark _calendarPage;
        public mistakeDark(HomePageDark homepage, EditPageDark editpage, CalendarPageDark calendarPage) {
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
