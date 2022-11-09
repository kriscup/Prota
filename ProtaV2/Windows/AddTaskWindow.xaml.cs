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
using System.Windows.Shapes;

namespace ProtaV2.Windows
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        private string _name = "";
        private string _description = "";
        private string _location = "";
        private string _date = "";
        private EditPage _editPage;
        TaskListItem _current;
        private bool _isWindowed = true;
        private bool minimizeToTray = false;

        public AddTaskWindow(EditPage editPage, TaskListItem current = null)
        {
            InitializeComponent();
            _editPage = editPage;
            _current = current;
        }

        private void NameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            _name = NameText.Text;
            CheckButton();
        }

        private void DescText_TextChanged(object sender, TextChangedEventArgs e)
        {
            _description = DescText.Text;
            CheckButton();
        }

        private void LocationText_TextChanged(object sender, TextChangedEventArgs e)
        {
            _location = LocationText.Text;
            CheckButton();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(_current == null)
            {
                _editPage.AddTask(_name, _description, _date, _location);
            } 
            else
            {
                _editPage.EditTask(_current, _name, _description, _date, _location);
            }
            Close();
        }

        private void CheckButton()
        {
            SaveButton.IsEnabled = ((_name.Length > 0 && _description.Length > 0 && _date.Length > 0) ? true : false);
        }

        private void CompletedByDate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (CompletedByDate.Value != DateTime.MinValue)
            {
                _date = CompletedByDate.Value.ToString();
                CheckButton();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
