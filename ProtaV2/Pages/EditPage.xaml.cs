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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProtaV2
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        private int selectedCategory = 0;

        public EditPage()
        {
            InitializeComponent();
        }

        private async void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(100);
            AddCategoryWindow addCategoryWindow = new AddCategoryWindow(this);

            addCategoryWindow.ShowDialog();  // Showdialog for non-modal
        }

        public async void AddTask(string name, string desc, string dueDate, string location)
        {
            CategoryListItem categoryListItem = ((CategoryListItem)CategoryListbox.Items.GetItemAt(selectedCategory));
            AddCategoryButton.IsEnabled = false;
            AddTaskButton.IsEnabled = false;

            await Task.Delay(100);

            TaskListItem newItem = new TaskListItem();
            newItem.TaskName = name;
            newItem.TaskText = desc;
            newItem.TaskColor = categoryListItem.CategoryColor;
            newItem.DueDate = dueDate;
            newItem.Location = location;
            categoryListItem.tasks.Add(newItem);

            await Task.Delay(100);
            AddCategoryButton.IsEnabled = true;
            AddTaskButton.IsEnabled = true;
            UpdateTasks();
        }

        public async void AddCategory(string name, Color color)
        {
            TaskName.Content = "Click a task to view";
            UpdateTasks();
            AddCategoryButton.IsEnabled = false;

            await Task.Delay(100);

            CategoryListItem newItem = new CategoryListItem();
            newItem.CategoryName = name;
            newItem.tasks = new List<TaskListItem>();

            newItem.CategoryColor = color.ToString();

            CategoryListbox.Items.Add(newItem);
            await Task.Delay(100);
            AddCategoryButton.IsEnabled = true;
            AddTaskButton.IsEnabled = true;
            UpdateTasks();
        }

        private async void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(100);
            AddTaskWindow addTaskWindow = new AddTaskWindow(this);

            addTaskWindow.ShowDialog();

        }

        private void TaskListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TaskListbox.SelectedItem != null)
            {
                TaskListItem selected = ((TaskListItem)TaskListbox.SelectedItem);
                TaskText.Text = selected.TaskText;
                DueText.Text = "Due: " + selected.DueDate;
                LocationText.Text = "Where: " + selected.Location;
                TaskName.Content = ((CategoryListItem)CategoryListbox.SelectedItem).CategoryName + " - " + ((TaskListItem)TaskListbox.SelectedItem).TaskName;
            }
        }

        private void CategoryListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryListbox.SelectedItem != null)
            {
                UpdateTasks();
                selectedCategory = CategoryListbox.SelectedIndex;
            }
            AddTaskButton.IsEnabled = (CategoryListbox.SelectedItem != null);
        }

        private void UpdateTasks()
        {
            
            TaskListbox.Items.Clear();
            CategoryListItem current = (CategoryListItem)CategoryListbox.SelectedItem;
            if (current != null)
            {
                foreach (TaskListItem item in current.tasks)
                {
                    TaskListbox.Items.Add(item);
                }
            }
            selectedCategory = CategoryListbox.SelectedIndex;
            TaskListbox.Items.Refresh();
        }
    }

   
}
