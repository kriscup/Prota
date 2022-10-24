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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private int selectedCategory = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void AddButton1_Click(object sender, RoutedEventArgs e)
        {
            CategoryListbox.UnselectAll();
            TaskListbox.UnselectAll();
            TaskText.Text = "";
            TaskName.Content = "Click a task to view";
            UpdateTasks();

            AddButton1.IsEnabled = false;
            await Task.Delay(100);
            CategoryListItem newItem = new CategoryListItem();
            newItem.CategoryName = "New Category X";
            newItem.tasks = new List<TaskListItem>();

            Random r = new Random();
            newItem.CategoryColor = (Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 233))).ToString();

            CategoryListbox.Items.Add(newItem);
            await Task.Delay(100);
            AddButton1.IsEnabled = true;
        }

        private async void AddButton2_Click(object sender, RoutedEventArgs e)
        {
            AddButton2.IsEnabled = false;
            await Task.Delay(100);
            TaskListItem newItem = new TaskListItem();
            newItem.TaskName = "Some Task";
            newItem.TaskText = "Lorem Ipsum";
            newItem.TaskColor = ((CategoryListItem)CategoryListbox.Items.GetItemAt(selectedCategory)).CategoryColor;
            ((CategoryListItem)CategoryListbox.Items.GetItemAt(selectedCategory)).tasks.Add(newItem);
            await Task.Delay(100);
            AddButton2.IsEnabled = true;
            UpdateTasks();

        }

        private void TaskListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TaskListbox.SelectedItem != null)
            {
                TaskText.Text = ((TaskListItem)TaskListbox.SelectedItem).TaskText;
                TaskName.Content = ((TaskListItem)TaskListbox.SelectedItem).TaskName;
            }
        }

        private void CategoryListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryListbox.SelectedItem != null)
            {
                UpdateTasks();
            }
            AddButton2.IsEnabled = (CategoryListbox.SelectedItem != null);
        }

        private void UpdateTasks()
        {
            selectedCategory = CategoryListbox.SelectedIndex;
            TaskListbox.Items.Clear();
            CategoryListItem current = (CategoryListItem)CategoryListbox.SelectedItem;
            if (current != null)
            {
                foreach (TaskListItem item in current.tasks)
                {
                    TaskListbox.Items.Add(item);
                }
            }

            TaskListbox.Items.Refresh();
        }
    }

   
}
