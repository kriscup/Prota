using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for ImportDataWindow.xaml
    /// </summary>
    public partial class ImportDataWindowDark : Window
    {
        private List<CategoryListItem> _categories = new List<CategoryListItem>();
        private EditPageDark _editPage;
        public ImportDataWindowDark(EditPageDark editPage)
        {
            InitializeComponent();
            _editPage = editPage;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void AppendButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if ((bool)openFileDialog.ShowDialog())
            {
                try
                {
                    _categories = EditPage.LoadJSON(openFileDialog.FileName);

                    if(_categories != null)
                    {
                        _editPage.AppendData(_categories);
                    }
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured.", "Prota");
                }
            }
                
        }

        private void ReplaceButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if ((bool)openFileDialog.ShowDialog())
            {

                try
                {
                    _categories = EditPage.LoadJSON(openFileDialog.FileName);

                    if (_categories != null)
                    {
                        _editPage.SetData(_categories);
                    }
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured.", "Prota");
                }
            }
        }
    }
}
