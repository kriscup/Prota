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
    /// Interaction logic for CalendarPageDark.xaml
    /// </summary>
    public partial class CalendarPageDark : Page
    {
        List<CategoryListItem> loadedItems;
        List<List<TaskListItem>> foundTaskLists;

        public CalendarPageDark()
        {
            InitializeComponent();
            loadedItems = EditPageDark.LoadJSON(EditPageDark.dataPath);
            foundTaskLists = new List<List<TaskListItem>>();
        }

        private void Calendar_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private async void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DailyTasks.Items.Clear();
            DailyTasks.Items.Refresh();

            foreach (CategoryListItem item in loadedItems)
            {
                foundTaskLists.Add(item.tasks);
            }

            foreach (List<TaskListItem> list in foundTaskLists)
            {
                foreach (TaskListItem task in list)
                {
                    if(task.DueDate == TaskCalendar.SelectedDate.ToString() && !DailyTasks.Items.Contains(task))
                    {
                        DailyTasks.Items.Add(task);
                    }
                }
            }
            
            DailyTasks.Items.Refresh();
        }

        public void UpdateTasks(List<TaskListItem> list)
        {
            DailyTasks.Items.Clear();

            foreach (TaskListItem task in list)
            {
                if (task.DueDate == TaskCalendar.SelectedDate.ToString() && !DailyTasks.Items.Contains(task))
                {
                    DailyTasks.Items.Add(task);
                }
            }

            DailyTasks.Items.Refresh();

        }
    }
}
