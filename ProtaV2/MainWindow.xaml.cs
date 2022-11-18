using ProtaV2.Windows;
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
        public static Splash splashScreen;
        public static HomePage homePage;
        public static EditPage editPage;
        public static CalendarPage calendarPage;
        public static Settings settingsPage;
        private bool _isWindowed = true;
        public bool minimizeToTray = false;

        public MainWindow(){
            InitializeComponent();



            DataContext = new HomePage();
            splashScreen = new Splash();
            homePage = new HomePage();
            calendarPage = new CalendarPage();
            editPage = new EditPage(homePage, calendarPage);
            settingsPage = new Settings(this, homePage, editPage, calendarPage);

            settingsPage.ResolutionSizes.SelectedIndex = 3;

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

        public static void UpdateCategories(List<CategoryListItem> newCategories)
        {
            List<TaskListItem> tasks = new List<TaskListItem>();

            foreach(CategoryListItem item in newCategories)
            {
                foreach(TaskListItem task in item.tasks)
                {
                    tasks.Add(task);
                }
            }

            homePage.UpdateTasks(tasks, newCategories);
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

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            _isWindowed = !_isWindowed;
            if (_isWindowed)
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
                MaximizeButton.Content = "☐";
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
                MaximizeButton.Content = "❏";
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if(!minimizeToTray)
            {
                App.Current.Shutdown();
            }
            else
            {
                this.Hide();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();

                if(Application.Current.MainWindow.WindowState == WindowState.Maximized)
                {
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                    MaximizeButton.Content = "☐";
                    _isWindowed = true;
                }
            }
        }

        private void myNotifyIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            this.Show();
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
        public bool isDone { get; set; } = false;
        public int HomePageIndex { get; set; }
    }

    public class CategoryListItem
    {
        public string CategoryName { get; set; }
        public List<TaskListItem> tasks { get; set; }
        public Color CategoryColor { get; set; }
        public SolidColorBrush CategoryBrush { get; set; }
        public int Amount { get; set; } = 0;
    }


}
