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
        public List<CategoryListItem> CategoryCollection = new List<CategoryListItem>();
        public HomePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is my summary
        /// </summary>
        /// <param name="list"></param>
        public void UpdateTasks(List<TaskListItem> list, List<CategoryListItem> categories)
        {
            CategoryCollection = categories;
            UpcomingTasksListbox.Items.Clear();

            List<TaskListItem> items = new List<TaskListItem>();
            List<TaskListItem> itemsDone = new List<TaskListItem>();

            foreach (TaskListItem t in list)
            {
                if(!t.isDone)
                {
                    items.Add(t);
                }
                else
                {
                    itemsDone.Add(t);
                }
            }

            items.Sort((x, y) => DateTime.Parse(x.DueDate).CompareTo(DateTime.Parse(y.DueDate)));
            itemsDone.Sort((x, y) => DateTime.Parse(x.DueDate).CompareTo(DateTime.Parse(y.DueDate)));

            UpcomingTasksListbox.Items.Clear();
            CompletedTasksListbox.Items.Clear();

            foreach (TaskListItem t in items)
            {
                UpcomingTasksListbox.Items.Add(t);
            }

            foreach (TaskListItem t in itemsDone)
            {
                CompletedTasksListbox.Items.Add(t);
            }

            UpcomingTasksListbox.Items.Refresh();
            CompletedTasksListbox.Items.Refresh();

        }

        private void UpcomingTasksListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskListItem item = (TaskListItem) UpcomingTasksListbox.SelectedItem;
            if(item != null)
            {
                CompletedTasksListbox.UnselectAll();
                CheckButton.Visibility = Visibility.Visible;
                DescriptionText.Text = item.TaskText;
                LocationText.Text = "Where: " + item.Location;
                DueText.Text = "When: " + item.DueDate;
                TaskDescriptionName.Content = item.TaskName;
                CheckButton.Content = "Set Done";
            }
            
        }

        private void CompletedTasksListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskListItem item = (TaskListItem)CompletedTasksListbox.SelectedItem;
            if (item != null)
            {
                UpcomingTasksListbox.UnselectAll();
                CheckButton.Visibility = Visibility.Visible;
                DescriptionText.Text = item.TaskText;
                LocationText.Text = "Where: " + item.Location;
                DueText.Text = "When: " + item.DueDate;
                TaskDescriptionName.Content = item.TaskName;
                CheckButton.Content = "Set Not Done";
            }
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            if(UpcomingTasksListbox.SelectedItem != null || CompletedTasksListbox.SelectedItem != null)
            {
                TaskListItem task = UpcomingTasksListbox.SelectedItem != null ? (TaskListItem) UpcomingTasksListbox.SelectedItem : (TaskListItem)CompletedTasksListbox.SelectedItem;

                if(task != null)
                {
                    if (!task.isDone)
                    {
                        task.isDone = true;
                        UpcomingTasksListbox.Items.Remove(task);
                        CompletedTasksListbox.Items.Add(task);
                        CompletedTasksListbox.SelectedItem = task;
                        
                        List<TaskListItem> items = new List<TaskListItem>();

                        foreach(TaskListItem t in UpcomingTasksListbox.Items)
                        {
                            items.Add(t);
                        }

                        items.Sort((x, y) => DateTime.Parse(x.DueDate).CompareTo(DateTime.Parse(y.DueDate)));

                        UpcomingTasksListbox.Items.Clear();

                        foreach(TaskListItem t in items)
                        {
                            UpcomingTasksListbox.Items.Add(t);
                        }

                        if(CategoryCollection.Count > 0)
                        {
                            EditPage.UpdateJSON(CategoryCollection);
                        }

                        CompletedTasksListbox.SelectedItem = task;
                        CheckButton.Content = "Set Not Done";
                    }
                    else
                    {
                        task.isDone = false;
                        CompletedTasksListbox.Items.Remove(task);
                        UpcomingTasksListbox.Items.Insert(task.HomePageIndex, task);

                        List<TaskListItem> items = new List<TaskListItem>();

                        foreach (TaskListItem t in UpcomingTasksListbox.Items)
                        {
                            items.Add(t);
                        }

                        items.Sort((x, y) => DateTime.Parse(x.DueDate).CompareTo(DateTime.Parse(y.DueDate)));

                        UpcomingTasksListbox.Items.Clear();

                        foreach (TaskListItem t in items)
                        {
                            UpcomingTasksListbox.Items.Add(t);
                        }

                        if (CategoryCollection.Count > 0)
                        {
                            EditPage.UpdateJSON(CategoryCollection);
                        }
                        UpcomingTasksListbox.SelectedItem = task;
                        CheckButton.Content = "Set Done";
                    }
                }
                else
                {
                    CheckButton.Visibility = Visibility.Hidden;
                }

                
            }
        }
    }

    
}
