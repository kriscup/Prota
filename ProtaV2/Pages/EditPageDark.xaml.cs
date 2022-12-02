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
using System.Reflection;
using System.Diagnostics;
using Microsoft.Win32;

namespace ProtaV2
{
    /// <summary>
    /// Interaction logic for MainPageDark.xaml
    /// </summary>
    public partial class EditPageDark : Page
    {
        private int _selectedCategory = 0;
        private HomePageDark _page;
        private CalendarPageDark _calPage;
        public static string dataPath = Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("bin")) + "\\Data\\data.txt";


        public EditPageDark(HomePageDark page, CalendarPageDark calendarPage)
        {
            InitializeComponent();
            List<CategoryListItem> loadedItems = LoadJSON(dataPath);

            _page = page;
            _calPage = calendarPage;

            foreach (CategoryListItem item in loadedItems)
            {
                CategoryListbox.Items.Add(item);
            }
            UpdateTasks();
        }

        public void AppendData(List<CategoryListItem> loadedItems)
        {
            List<string> knownCategoryNames = new List<string>();

            foreach(CategoryListItem knownItem in CategoryListbox.Items)
            {
                knownCategoryNames.Add(knownItem.CategoryName);
            }

            foreach (CategoryListItem item in loadedItems)
            {
                if(!knownCategoryNames.Contains(item.CategoryName))
                {
                    CategoryListbox.Items.Add(item);
                }
            }
            UpdateTasks();
            UpdateJSON();
        }

        public void SetData(List<CategoryListItem> loadedItems)
        {
            CategoryListbox.Items.Clear();

            foreach (CategoryListItem item in loadedItems)
            {
                CategoryListbox.Items.Add(item);
            }

            CategoryListbox.Items.Refresh();
            UpdateTasks();
            UpdateJSON();
        }

        private async void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(100);
            AddCategoryWindowDark addCategoryWindow = new AddCategoryWindowDark(this);

            addCategoryWindow.ShowDialog();  // Showdialog for non-modal
        }

        private void ResetCenterText()
        {
            TaskText.Text = "No task selected";
            DueText.Text = "";
            LocationText.Text = "";
            TaskName.Content = "Click a task to view";
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
            UpdateJSON();
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
            AddTaskWindowDark addTaskWindow = new AddTaskWindowDark(this);

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
            ResetCenterText();
            AddTaskButton.IsEnabled = (CategoryListbox.SelectedItem != null);
            EditCategoryButton.IsEnabled = (CategoryListbox.SelectedItem != null);
        }

        private void UpdateTasks()
        {
            TaskListbox.Items.Clear();
            CategoryListItem current = (CategoryListItem)CategoryListbox.SelectedItem;
            List<TaskListItem> tasks = new List<TaskListItem>();
            List<CategoryListItem> categories = new List<CategoryListItem>();
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

            foreach(CategoryListItem category in CategoryListbox.Items)
            {
                category.Amount = category.tasks.Count;
                categories.Add(category);
                foreach (TaskListItem task in category.tasks)
                {
                    tasks.Add(task);
                }
            }
            CategoryListbox.Items.Refresh();
            _page.UpdateTasks(tasks, categories);
            _calPage.UpdateTasks(tasks);
            

        }

        public void DeleteData()
        {
            CategoryListbox.Items.Clear();
            TaskListbox.Items.Clear();
        }

        private void UpdateJSON()
        {
            try
            {
                
                using (StreamWriter sw = new StreamWriter(dataPath, false))
                {
                    // Wipe the file
                }

                foreach (CategoryListItem item in CategoryListbox.Items)
                {
                    string jsonString = JsonConvert.SerializeObject(item);

                    using (StreamWriter sw = new StreamWriter(dataPath, true))
                    {
                        sw.WriteLine(jsonString);
                    }
                }
            }
            catch (Exception ex)
            { 
            }
        }

        public static void UpdateJSON(List<CategoryListItem> items)
        {
            try
            {

                using (StreamWriter sw = new StreamWriter(dataPath, false))
                {
                    // Wipe the file
                }

                foreach (CategoryListItem item in items)
                {
                    string jsonString = JsonConvert.SerializeObject(item);

                    using (StreamWriter sw = new StreamWriter(dataPath, true))
                    {
                        sw.WriteLine(jsonString);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static List<CategoryListItem> LoadJSON(string path)
        {
            if (File.Exists(path))
            {
                List<CategoryListItem> found = new List<CategoryListItem>();
                // Read the file and display it line by line.  
                foreach (string line in File.ReadLines(path))
                {
                    found.Add(JsonConvert.DeserializeObject<CategoryListItem>(line));
                }
                return found;
            }
            return new List<CategoryListItem>();
        }

        private async void EditCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(100);
            CategoryListItem current = (CategoryListItem)CategoryListbox.SelectedItem;
            AddCategoryWindowDark addCategoryWindow = new AddCategoryWindowDark(this, current);
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
            UpdateJSON();
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
            UpdateJSON();
        }

        private async void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(100);
            TaskListItem current = (TaskListItem)TaskListbox.SelectedItem;
            AddTaskWindowDark addTaskWindow = new AddTaskWindowDark(this, current);
            addTaskWindow.NameText.Text = current.TaskName;
            addTaskWindow.DescText.Text = current.TaskText;
            addTaskWindow.LocationText.Text = current.Location;
            addTaskWindow.CompletedByDate.Value = DateTime.Parse(current.DueDate);

            addTaskWindow.ShowDialog();
        }

        private async void ImportDataButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(100);
            ImportDataWindowDark importDataWindow = new ImportDataWindowDark(this);

            importDataWindow.ShowDialog();
        }

        private void ExportDataButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if ((bool)openFileDialog.ShowDialog())
            {

                try
                {
                    try
                    {
                        foreach (CategoryListItem item in CategoryListbox.Items)
                        {
                            string jsonString = JsonConvert.SerializeObject(item);

                            using (StreamWriter sw = new StreamWriter(openFileDialog.FileName, true))
                            {
                                sw.WriteLine(jsonString);
                            }
                        }
                        MessageBox.Show("Export completed!");
                    }
                    catch (Exception ex)
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured.", "Prota");
                }
            }
        }
    }

   
}
