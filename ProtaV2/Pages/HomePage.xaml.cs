using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ProtaV2
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public List<TaskListItem> TaskCollection { get; set; }
        public HomePage()
        {
            InitializeComponent();

            /*
            TaskListItem task1 = new TaskListItem();
            TaskListItem task2 = new TaskListItem();
            TaskListItem task3 = new TaskListItem();


            task1.TaskName = "Task 1";
            task1.TaskText = "Do your homework";
            task1.TaskColor = Colors.PapayaWhip;
            task1.TaskBrush = new SolidColorBrush(task1.TaskColor);

            task2.TaskName = "Task 2";
            task2.TaskText = "Take out the trash";
            task2.TaskColor = Colors.SeaShell;
            task2.TaskBrush = new SolidColorBrush(task2.TaskColor);

            task3.TaskName = "Task 3";
            task3.TaskText = "Go grocery shopping for the week";
            task3.TaskColor = Colors.PaleTurquoise;
            task3.TaskBrush = new SolidColorBrush(task3.TaskColor);

            TaskCollection = new List<TaskListItem>();

            TaskCollection.Add(task1);
            TaskCollection.Add(task2);
            TaskCollection.Add(task3);

            this.TaskListbox.ItemsSource = TaskCollection; //is this how it ignores sample data but still provides the tasks from above when the app is run? -GR (not sure)
            */

            List<CategoryListItem> loadedItems = EditPage.LoadJSON();
            List<List<TaskListItem>> foundTaskLists = new List<List<TaskListItem>>();
            foreach (CategoryListItem item in loadedItems)
            {
                foundTaskLists.Add(item.tasks);
            }

            foreach(List<TaskListItem> list in foundTaskLists)
            {
                foreach(TaskListItem task in list)
                {
                    UpcomingTasksListbox.Items.Add(task);
                }
            }

            UpcomingTasksListbox.Items.Refresh();
        }

        /// <summary>
        /// This is my summary
        /// </summary>
        /// <param name="list"></param>
        public void UpdateTasks(List<TaskListItem> list)
        {
            UpcomingTasksListbox.Items.Clear();

            foreach (TaskListItem task in list)
            {
                UpcomingTasksListbox.Items.Add(task);
            }

            UpcomingTasksListbox.Items.Refresh();

        }

        private void UpcomingTasksListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskListItem item = (TaskListItem) UpcomingTasksListbox.SelectedItem;
            DescriptionText.Text = item.TaskText;
            TaskDescriptionName.Content = "Task Description - " + item.TaskName;
        }

        private void CompletedTasksListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    
}
