using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace ProtaV2.Windows {
    /// <summary>
    /// Interaction logic for Attention.xaml
    /// </summary>
    public partial class AttentionDark : Window {
        HomePageDark _homepage;
        EditPageDark _editpage;
        CalendarPageDark _calendarPage;

        public AttentionDark(HomePageDark homepage, EditPageDark editpage, CalendarPageDark calendarPage) {
            _homepage = homepage;
            _editpage = editpage;
            _calendarPage = calendarPage;
            InitializeComponent();
        }
        private void REconfirmYes_Click(object sender, RoutedEventArgs e) {

            Brush color = AttentionText.Foreground;
            Task.Delay(250).ContinueWith(_ => 
            {
                using (StreamWriter sw = new StreamWriter(EditPage.dataPath, false))
                {
                    // Wipe the file
                }

                this.Dispatcher.BeginInvoke(() => _homepage.UpdateTasks(new List<TaskListItem>(), new List<CategoryListItem>()));
                this.Dispatcher.BeginInvoke(() => _calendarPage.UpdateTasks(new List<TaskListItem>()));
                this.Dispatcher.BeginInvoke(() => _editpage.DeleteData());

                this.Dispatcher.BeginInvoke(() => AttentionText.Text = "Data deleted!");
                this.Dispatcher.BeginInvoke(() => AttentionText.Foreground = new SolidColorBrush(Colors.Red));
                this.Dispatcher.BeginInvoke(() => REconfirmYes.IsEnabled = false);
                this.Dispatcher.BeginInvoke(() => REconfirmNo.IsEnabled = false);
                Task.Delay(2000).ContinueWith(_ =>
                {
                    this.Dispatcher.BeginInvoke(() => AttentionText.Text = "Are you sure you want to do this, this will reset ALL of your data.");
                    this.Dispatcher.BeginInvoke(() => AttentionText.Foreground = color);
                    this.Dispatcher.BeginInvoke(() => REconfirmYes.IsEnabled = true);
                    this.Dispatcher.BeginInvoke(() => REconfirmNo.IsEnabled = true);
                    this.Dispatcher.BeginInvoke(() => Close());

                });



                
            });
            
        }

        private void REconfirmNo_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
