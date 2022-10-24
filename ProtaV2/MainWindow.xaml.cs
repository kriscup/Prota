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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProtaV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Content = new MainPage();
            HomeButton.IsEnabled = true;
            EditButton.IsEnabled = false;
            CalButton.IsEnabled = true;
        }

        private void CalButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Content = null;
            HomeButton.IsEnabled = true;
            EditButton.IsEnabled = true;
            CalButton.IsEnabled = false;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Content = null;
            HomeButton.IsEnabled = false;
            EditButton.IsEnabled = true;
            CalButton.IsEnabled = true;
        }
    }

    public class TaskListItem
    {
        public string TaskName { get; set; }
        public string TaskText { get; set; }
        public string TaskColor { get; set; }
    }

    public class CategoryListItem
    {
        public string CategoryName { get; set; }
        public List<TaskListItem> tasks { get; set; }
        public string CategoryColor { get; set; }
    }
}
