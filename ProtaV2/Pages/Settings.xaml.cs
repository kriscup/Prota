using ProtaV2.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProtaV2 {
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page {

        private MainWindow _mainWindow;
        private HomePage _homepage;
        private EditPage _editPage;
        private CalendarPage _calendarPage;

        public Settings(MainWindow mainWindow, HomePage  homepage, EditPage editPage, CalendarPage calendarPage) {
            InitializeComponent();
            _mainWindow = mainWindow;
            _homepage = homepage;
            _editPage = editPage;
            _calendarPage = calendarPage;
        }

        private void FontSizes_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void ResetButton_Click(object sender, RoutedEventArgs e) {

            Window resetWindow = new Attention(_homepage, _editPage, _calendarPage);
            
            resetWindow.ShowDialog();
        }

        private void ResolutionSizes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] resolution = ResolutionSizes.SelectedItem.ToString().Split();
            string width = resolution[1];
            string height = resolution[3];

            if (_mainWindow != null)
            {
                _mainWindow.Width = int.Parse(width);
                _mainWindow.Height = int.Parse(height);
                double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
                double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
                double windowWidth = this.Width;
                double windowHeight = this.Height;
                _mainWindow.Left = (screenWidth / 2) - (windowWidth / 2);
                _mainWindow.Top = (screenHeight / 2) - (windowHeight / 2);
            }

        }

        private void DarkmodeToggle_Click(object sender, RoutedEventArgs e)
        {
            if (DarkmodeToggle != null)
            {
                if ((bool)DarkmodeToggle.IsChecked)
                {
                    // Checked is now true
                    // Do something
                }
                else
                {
                    // Checked is now false
                    // Do something
                }
            }
        }

        private void MinimizetoTray_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.minimizeToTray = (bool)MinimizetoTray.IsChecked;
            int x = 10;
        }

        private void StartMinimized_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddNotificationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveNotificationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Contactwords_Click(object sender, RoutedEventArgs e)
        {
            var destinationurl = "https://www.google.com/";
            var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
            {
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(sInfo);
        }
    }
}
