using Microsoft.Win32;
using Newtonsoft.Json;
using ProtaV2.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        public static string emailDataPath = Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("bin")) + "\\Data\\email.txt";

        public Settings(MainWindow mainWindow, HomePage  homepage, EditPage editPage, CalendarPage calendarPage) {
            InitializeComponent();
            _mainWindow = mainWindow;
            _homepage = homepage;
            _editPage = editPage;
            _calendarPage = calendarPage;
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
            // Checked is now true
            // Do something
            MainWindow.mainWindowDark = new MainWindowDark(true, _mainWindow);
            MainWindowDark dark = MainWindow.mainWindowDark;
            Window current = Application.Current.MainWindow;
            MainWindowDark.settingsPage.ResolutionSizes.SelectedIndex = MainWindow.settingsPage.ResolutionSizes.SelectedIndex;
            Application.Current.MainWindow = dark;
            current.Visibility = Visibility.Hidden;
            dark.MainContentFrame.Content = MainWindowDark.settingsPage;

            Application.Current.MainWindow.Show();
        }

        private void MinimizetoTray_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.minimizeToTray = (bool)MinimizetoTray.IsChecked;
            
        }


        private void AddNotificationButton_Click(object sender, RoutedEventArgs e){
            if (EmailInput.Text.Length != 0 && EmailInput.Text.Contains('@')) {
                if (File.Exists(emailDataPath)) {
                    string[]lines = File.ReadAllLines(emailDataPath);
                    if (lines.Length >= 1) {
                        error errorWindow = new error(_homepage, _editPage, _calendarPage);
                        errorWindow.userEmail.Text = lines[0];
                        errorWindow.ShowDialog();
                    }
                    else {
                        using StreamWriter writer = new StreamWriter(emailDataPath);
                        string input = EmailInput.Text;
                        MainWindow.email = EmailInput.Text;
                        writer.WriteLine(input);
                        writer.Close();
                        EmailInput.Text = null;
                    }
                }
            }
            else {
                Window mistakeWindow = new mistake(_homepage, _editPage, _calendarPage);
                mistakeWindow.ShowDialog();
            }
        }

        private void RemoveNotificationButton_Click(object sender, RoutedEventArgs e){
            // Wipe the file
            using StreamWriter delete = new StreamWriter(emailDataPath, false);
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

        private void StartMinimized_Click(object sender, RoutedEventArgs e) {

        }
    }
}
