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

namespace ProtaV2
{
    /// <summary>
    /// Interaction logic for AddCategoryWindowDark.xaml
    /// </summary>
    public partial class AddCategoryWindowDark : Window
    {
        private EditPageDark _editPage;
        private string _categoryName;
        private Color _categoryColor;
        private CategoryListItem _current;
        private bool _isWindowed = true;
        private bool minimizeToTray = false;

        public AddCategoryWindowDark(EditPageDark editPage, CategoryListItem current = null)
        {
            InitializeComponent();
            ColorPicker.Models.NotifyableColor color = CategoryColorPicker.Color;
            Color outColor = Color.FromRgb((byte)color.RGB_R, (byte)color.RGB_G, (byte)color.RGB_B);
            _current = current;
            _editPage = editPage;
            _categoryColor = outColor;
        }

        private void NameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            CategoryPreview.Text = NameText.Text;
            _categoryName = NameText.Text;
            AddButton.IsEnabled = (NameText.Text.Length > 0);
        }

        private void CategoryColorPicker_ColorChanged(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush();
            ColorPicker.Models.NotifyableColor color = CategoryColorPicker.Color;
            Color outColor = Color.FromRgb((byte)color.RGB_R, (byte)color.RGB_G, (byte)color.RGB_B);
            brush.Color = outColor;
            Border.Background = brush;
            _categoryColor = outColor;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            if (_current == null) {
                _editPage.AddCategory(_categoryName, _categoryColor);
            }
            else {
                _editPage.EditCategory(_current, _categoryName, _categoryColor);
            }
            Close();
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
    }
}
