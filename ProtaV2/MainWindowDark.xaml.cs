using ProtaV2.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    public partial class MainWindowDark : Window {
        public static SplashDark splashScreen;
        public static HomePageDark homePage;
        public static EditPageDark editPage;
        public static CalendarPageDark calendarPage;
        public static SettingsDark settingsPage;

        public static MainWindow mainWindowLight;
        public static Splash splashScreenLight;
        public static HomePage homePageLight;
        public static EditPage editPageLight;
        public static CalendarPage calendarPageLight;
        public static Settings settingsPageLight;

        private bool _isWindowed = true;
        public bool minimizeToTray = false;
        public static string email = "";

        public MainWindowDark (bool doCreate, MainWindow main) {
            InitializeComponent();

            splashScreen = new SplashDark();
            homePage = new HomePageDark();
            calendarPage = new CalendarPageDark();
            editPage = new EditPageDark(homePage, calendarPage);
            settingsPage = new SettingsDark(this, homePage, editPage, calendarPage);
            settingsPage.ResolutionSizes.SelectedIndex = 3;

            if (doCreate)
            {
                mainWindowLight = main;

                ButtonStackPanel.Opacity = 1;

                HomeButton.IsEnabled = true;

                EditButton.IsEnabled = true;
                CalButton.IsEnabled = true;
                SettingsButton.IsEnabled = true;

                EditButton.IsHitTestVisible = false;
                CalButton.IsHitTestVisible = false;
                SettingsButton.IsHitTestVisible = false;

                HomeButton.IsHitTestVisible = true;
                CalButton.IsHitTestVisible = true;
                SettingsButton.IsHitTestVisible = true;
                EditButton.IsHitTestVisible = true;
            }
            else
            {
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
                            this.Dispatcher.BeginInvoke(() => UpdateButton());
                        });
                    });
                });
            }
            UpdateButton();
        }

        private void UpdateButton()
        {
            email = CheckEmail();

            SendEmailButton.IsEnabled = email != "" ? true : false;
        }

        private string CheckEmail()
        {
            if (File.Exists(Settings.emailDataPath))
            {
                string[] lines = File.ReadAllLines(Settings.emailDataPath);
                return lines[0];
            }
            else
            {
                return "";
            }
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

        private void SendEmailButton_Click(object sender, RoutedEventArgs e)
        {
            SendMail();
        }

        private void SendMail()
        {
            SendEmailButton.IsEnabled = false;
            Task.Delay(250).ContinueWith(t =>
            {
                var fromAddress = new MailAddress("protahelper@gmail.com", "Prota");
                var toAddress = new MailAddress(email, "Prota User");
                const string fromPassword = "hbauvkickjicojnj";
                const string subject = "Prota Data";
                const string body = "Hello Prota User,\nHere is your data backup.\n\nThanks!\nProta Helper";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                Attachment data = new Attachment(EditPage.dataPath);

                var message = new MailMessage(fromAddress, toAddress);
                message.Subject = subject;
                message.Body = body;
                message.Attachments.Add(data);

                this.Dispatcher.BeginInvoke(() => smtp.Send(message));

                Task.Delay(250).ContinueWith(t =>
                {
                    this.Dispatcher.BeginInvoke(() => SendEmailButton.Content = "✔️");
                    Task.Delay(2000).ContinueWith(t =>
                    {
                        this.Dispatcher.BeginInvoke(() => SendEmailButton.Content = "📧");
                        this.Dispatcher.BeginInvoke(() => SendEmailButton.IsEnabled = true);
                    });
                });
            });
        }
    }
}
