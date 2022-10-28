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
    /// Interaction logic for AddCategoryWindow.xaml
    /// </summary>
    public partial class AddCategoryWindow : Window
    {
        private EditPage editPage;
        private string categoryName;
        private Color categoryColor;

        public AddCategoryWindow(EditPage editPage)
        {
            InitializeComponent();
            ColorPicker.Models.NotifyableColor color = CategoryColorPicker.Color;
            Color outColor = Color.FromRgb((byte)color.RGB_R, (byte)color.RGB_G, (byte)color.RGB_B);

            this.editPage = editPage;
            categoryColor = outColor;
        }

        private void NameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            CategoryPreview.Text = NameText.Text;
            categoryName = NameText.Text;
            AddButton.IsEnabled = (NameText.Text.Length > 0);
        }

        private void CategoryColorPicker_ColorChanged(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush();
            ColorPicker.Models.NotifyableColor color = CategoryColorPicker.Color;
            Color outColor = Color.FromRgb((byte)color.RGB_R, (byte)color.RGB_G, (byte)color.RGB_B);
            brush.Color = outColor;
            Border.Background = brush;
            categoryColor = outColor;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            editPage.AddCategory(categoryName, categoryColor);
            Close();
        }
    }
}
