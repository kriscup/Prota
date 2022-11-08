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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProtaV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{
        private Splash splashScreen;
        private HomePage homePage;
        private EditPage editPage;
        private CalendarPage calendarPage;
        private Settings settingsPage;

        public MainWindow(){
            InitializeComponent();
            DataContext = new HomePage();
            splashScreen = new Splash();
            homePage = new HomePage();
            editPage = new EditPage(homePage);
            calendarPage = new CalendarPage();
            settingsPage = new Settings();

            MainContentFrame.Content = splashScreen;
            ButtonStackPanel.Opacity = 0;

            HomeButton.IsEnabled = false;
           
            EditButton.IsEnabled = true;
            CalButton.IsEnabled = true;
            SettingsButton.IsEnabled = true;

            EditButton.IsHitTestVisible = false;
            CalButton.IsHitTestVisible = false;
            SettingsButton.IsHitTestVisible = false;


            Task.Delay(3000).ContinueWith(t =>
            {
                this.Dispatcher.BeginInvoke(() => AnimationHelper.AnimatePageOpactiy(1, 0, 1, splashScreen));

                Task.Delay(1250).ContinueWith(t =>
                {
                    this.Dispatcher.BeginInvoke(() => AnimationHelper.AnimateStackPanelOpactiy(0, 1, 1, ButtonStackPanel));

                    Task.Delay(1000).ContinueWith(t =>
                    {
                        
                        this.Dispatcher.BeginInvoke(() => AnimationHelper.AnimatePageOpactiy(0, 1, 1, homePage));
                        this.Dispatcher.BeginInvoke(() => MainContentFrame.Content = homePage);
                        this.Dispatcher.BeginInvoke(() => HomeButton.IsHitTestVisible = true);
                        this.Dispatcher.BeginInvoke(() => CalButton.IsHitTestVisible = true);
                        this.Dispatcher.BeginInvoke(() => SettingsButton.IsHitTestVisible = true);
                        this.Dispatcher.BeginInvoke(() => EditButton.IsHitTestVisible = true);
                    });
                });
            });
        }

        private void EditButton_Click(object sender, RoutedEventArgs e){
            EditButton.IsEnabled = false;

            HomeButton.IsEnabled = true;
            CalButton.IsEnabled = true;
            SettingsButton.IsEnabled = true;

            HomeButton.IsHitTestVisible = false;
            CalButton.IsHitTestVisible = false;
            SettingsButton.IsHitTestVisible = false;

            Task.Delay(125).ContinueWith(t =>
            {
                this.Dispatcher.BeginInvoke(() => AnimationHelper.AnimatePageOpactiy(1, 0, 0.25f, (Page)MainContentFrame.Content));

                Task.Delay(250).ContinueWith(t =>
                {
                    this.Dispatcher.BeginInvoke(() => AnimationHelper.AnimatePageOpactiy(0, 1, 0.25f, editPage));
                    this.Dispatcher.BeginInvoke(() => MainContentFrame.Content = editPage);
                    this.Dispatcher.BeginInvoke(() => HomeButton.IsHitTestVisible = true);
                    this.Dispatcher.BeginInvoke(() => CalButton.IsHitTestVisible = true);
                    this.Dispatcher.BeginInvoke(() => SettingsButton.IsHitTestVisible = true);
                    this.Dispatcher.BeginInvoke(() => EditButton.IsHitTestVisible = true);
                });
            });
        }

        private void CalButton_Click(object sender, RoutedEventArgs e){
            CalButton.IsEnabled = false;

            HomeButton.IsEnabled = true;
            EditButton.IsEnabled = true;
            SettingsButton.IsEnabled = true;

            HomeButton.IsHitTestVisible = false;
            EditButton.IsHitTestVisible = false;
            SettingsButton.IsHitTestVisible = false;


            Task.Delay(125).ContinueWith(t =>
            {
                this.Dispatcher.BeginInvoke(() => AnimationHelper.AnimatePageOpactiy(1, 0, 0.25f, (Page)MainContentFrame.Content));

                Task.Delay(250).ContinueWith(t =>
                {
                    this.Dispatcher.BeginInvoke(() => AnimationHelper.AnimatePageOpactiy(0, 1, 0.25f, calendarPage));
                    this.Dispatcher.BeginInvoke(() => MainContentFrame.Content = calendarPage);
                    this.Dispatcher.BeginInvoke(() => HomeButton.IsHitTestVisible = true);
                    this.Dispatcher.BeginInvoke(() => CalButton.IsHitTestVisible = true);
                    this.Dispatcher.BeginInvoke(() => SettingsButton.IsHitTestVisible = true);
                    this.Dispatcher.BeginInvoke(() => EditButton.IsHitTestVisible = true);
                });
            });
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e){
            HomeButton.IsEnabled = false;

            CalButton.IsEnabled = true;
            EditButton.IsEnabled = true;
            SettingsButton.IsEnabled = true;

            CalButton.IsHitTestVisible = false;
            EditButton.IsHitTestVisible = false;
            SettingsButton.IsHitTestVisible = false;

            Task.Delay(125).ContinueWith(t =>
            {
                this.Dispatcher.BeginInvoke(() => AnimationHelper.AnimatePageOpactiy(1, 0, 0.25f, (Page)MainContentFrame.Content));

                Task.Delay(250).ContinueWith(t =>
                {
                    this.Dispatcher.BeginInvoke(() => AnimationHelper.AnimatePageOpactiy(0, 1, 0.25f, homePage));
                    this.Dispatcher.BeginInvoke(() => MainContentFrame.Content = homePage);
                    this.Dispatcher.BeginInvoke(() => HomeButton.IsHitTestVisible = true);
                    this.Dispatcher.BeginInvoke(() => CalButton.IsHitTestVisible = true);
                    this.Dispatcher.BeginInvoke(() => SettingsButton.IsHitTestVisible = true);
                    this.Dispatcher.BeginInvoke(() => EditButton.IsHitTestVisible = true);
                });
            });
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e) {
            SettingsButton.IsEnabled = false;

            CalButton.IsEnabled = true;
            EditButton.IsEnabled = true;
            HomeButton.IsEnabled = true;

            CalButton.IsHitTestVisible = false;
            EditButton.IsHitTestVisible = false;
            HomeButton.IsHitTestVisible = false;

            Task.Delay(125).ContinueWith(t =>
            {
                this.Dispatcher.BeginInvoke(() => AnimationHelper.AnimatePageOpactiy(1, 0, 0.25f, (Page)MainContentFrame.Content));

                Task.Delay(250).ContinueWith(t =>
                {
                    this.Dispatcher.BeginInvoke(() => AnimationHelper.AnimatePageOpactiy(0, 1, 0.25f, settingsPage));
                    this.Dispatcher.BeginInvoke(() => MainContentFrame.Content = settingsPage);
                });
                this.Dispatcher.BeginInvoke(() => HomeButton.IsHitTestVisible = true);
                this.Dispatcher.BeginInvoke(() => CalButton.IsHitTestVisible = true);
                this.Dispatcher.BeginInvoke(() => SettingsButton.IsHitTestVisible = true);
                this.Dispatcher.BeginInvoke(() => EditButton.IsHitTestVisible = true);
            });
        }

        private void DarkmodeButton_Click(object sender, RoutedEventArgs e) {
            //DarkmodeToggle
            //Click = "DarkmodeToggle_Click"
        }
    }

    public class TaskListItem
    {
        public string TaskName { get; set; }
        public string TaskText { get; set; }
        public Color TaskColor { get; set; }
        public SolidColorBrush TaskBrush { get; set; }
        public string DueDate { get; set; }
        public string Location { get; set; }
    }

    public class CategoryListItem
    {
        public string CategoryName { get; set; }
        public List<TaskListItem> tasks { get; set; }
        public Color CategoryColor { get; set; }
        public SolidColorBrush CategoryBrush { get; set; }
    }


}
