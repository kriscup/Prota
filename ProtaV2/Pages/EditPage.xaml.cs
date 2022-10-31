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
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace ProtaV2
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        private int _selectedCategory = 0;

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
            CategoryListItem categoryListItem = ((CategoryListItem)CategoryListbox.Items.GetItemAt(_selectedCategory));
            AddCategoryButton.IsEnabled = false;
            AddTaskButton.IsEnabled = false;

            await Task.Delay(100);

            TaskListItem newItem = new TaskListItem();
            newItem.TaskName = name;
            newItem.TaskText = desc;
            newItem.TaskColor = categoryListItem.CategoryColor;
            newItem.TaskBrush = new SolidColorBrush(categoryListItem.CategoryColor);
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
            newItem.CategoryColor = color;
            newItem.CategoryBrush = new SolidColorBrush(color);
            CategoryListbox.Items.Add(newItem);
            await Task.Delay(100);
            AddCategoryButton.IsEnabled = true;
            UpdateTasks();
            UpdateJSON();
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
            EditTaskButton.IsEnabled = (TaskListbox.SelectedItem != null);

        }

        private void CategoryListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryListbox.SelectedItem != null)
            {
                _selectedCategory = CategoryListbox.SelectedIndex;
                UpdateTasks();
            }
            AddTaskButton.IsEnabled = (CategoryListbox.SelectedItem != null);
            EditCategoryButton.IsEnabled = (CategoryListbox.SelectedItem != null);
        }

        private void UpdateTasks()
        {
            TaskListbox.Items.Clear();
            CategoryListItem current = (CategoryListItem)CategoryListbox.SelectedItem;
            if (current != null)
            {
                foreach (TaskListItem item in current.tasks)
                {
                    item.TaskColor = current.CategoryColor;
                    item.TaskBrush = new SolidColorBrush(current.CategoryColor);
                    TaskListbox.Items.Add(item);
                }
            }
            TaskListbox.Items.Refresh();
        }

        private void UpdateJSON()
        {
            try
            {
                Uri path1 = new Uri("pack://application:,,,/Data/data.json");
                string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Data/data.json");
                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    sw.WriteLine("");
                }

                foreach (CategoryListItem item in CategoryListbox.Items)
                {

                    string jsonString = JsonConvert.SerializeObject(item);

                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine(jsonString);
                    }
                }
            }
            catch (Exception ex)
            { 
            }
            
        }

        private async void EditCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(100);
            CategoryListItem current = (CategoryListItem)CategoryListbox.SelectedItem;
            AddCategoryWindow addCategoryWindow = new AddCategoryWindow(this, current);
            addCategoryWindow.NameText.Text = current.CategoryName;
            addCategoryWindow.CategoryColorPicker.Color.RGB_R = current.CategoryColor.R;
            addCategoryWindow.CategoryColorPicker.Color.RGB_G = current.CategoryColor.G;
            addCategoryWindow.CategoryColorPicker.Color.RGB_B = current.CategoryColor.B;

            addCategoryWindow.ShowDialog();
        }

        public void EditCategory(CategoryListItem current, string name, Color color)
        {
            int index = CategoryListbox.Items.IndexOf(current);
            CategoryListItem categoryListItem = (CategoryListItem) CategoryListbox.Items[index];

            if(categoryListItem != null)
            {
                categoryListItem.CategoryColor = color;
                categoryListItem.CategoryBrush = new SolidColorBrush(color);
                categoryListItem.CategoryName = name;
            }

            UpdateTasks();
            CategoryListbox.Items.Refresh();
        }

        public async void EditTask(TaskListItem current, string name, string desc, string dueDate, string location)
        {
            int index = TaskListbox.Items.IndexOf(current);
            TaskListItem taskListItem = (TaskListItem)TaskListbox.Items[index];

            if (taskListItem != null)
            {
                taskListItem.TaskName = name;
                taskListItem.TaskText = desc;
                taskListItem.DueDate = dueDate;
                taskListItem.Location = location;
            }
            TaskListbox.Items.Refresh();
            TaskListItem selected = ((TaskListItem)TaskListbox.SelectedItem);
            TaskText.Text = selected.TaskText;
            DueText.Text = "Due: " + selected.DueDate;
            LocationText.Text = "Where: " + selected.Location;
            TaskName.Content = ((CategoryListItem)CategoryListbox.SelectedItem).CategoryName + " - " + ((TaskListItem)TaskListbox.SelectedItem).TaskName;
        }

        private async void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(100);
            TaskListItem current = (TaskListItem)TaskListbox.SelectedItem;
            AddTaskWindow addTaskWindow = new AddTaskWindow(this, current);
            addTaskWindow.NameText.Text = current.TaskName;
            addTaskWindow.DescText.Text = current.TaskText;
            addTaskWindow.LocationText.Text = current.Location;
            addTaskWindow.CompletedByDate.SelectedDate = DateTime.Parse(current.DueDate);

            addTaskWindow.ShowDialog();
        }
    }

   
}
