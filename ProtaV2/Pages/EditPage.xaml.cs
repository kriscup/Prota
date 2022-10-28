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
        }

        private async void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTaskButton.IsEnabled = false;

            await Task.Delay(100);

            TaskListItem newItem = new TaskListItem();

            newItem.TaskName = "Some Task";
            newItem.TaskText = "Lorem Ipsum";
            newItem.TaskColor = ((CategoryListItem)CategoryListbox.Items.GetItemAt(selectedCategory)).CategoryColor;
            ((CategoryListItem)CategoryListbox.Items.GetItemAt(selectedCategory)).tasks.Add(newItem);

            await Task.Delay(100);
            AddTaskButton.IsEnabled = true;
            UpdateTasks();

        }

        private void TaskListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TaskListbox.SelectedItem != null)
            {
                TaskText.Text = ((TaskListItem)TaskListbox.SelectedItem).TaskText;
                TaskName.Content = ((CategoryListItem)CategoryListbox.SelectedItem).CategoryName + " - " + ((TaskListItem)TaskListbox.SelectedItem).TaskName;
            }
        }

        private void CategoryListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryListbox.SelectedItem != null)
            {
                UpdateTasks();
            }
            AddTaskButton.IsEnabled = (CategoryListbox.SelectedItem != null);
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
