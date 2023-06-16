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

namespace WorkAnalitycsWPF.View
{
    /// <summary>
    /// Interaction logic for ViewPage.xaml
    /// </summary>
    public partial class ViewPage : Page
    {
        private int viewElementCount = 0;
        public int rowCount = 0;

        public ViewPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            double columnCount = Math.Floor(ActualWidth / 200);

            grid.ColumnDefinitions.Clear();
            for (int i = 0; i < columnCount; i++)
            {
                var column = new ColumnDefinition();
                column.MinWidth = 200;
                grid.ColumnDefinitions.Add(column);
            }

            rowCount = (int)Math.Ceiling(viewElementCount / columnCount);

            grid.RowDefinitions.Clear();
            for (int i = 0; i < columnCount; i++)
            {
                var column = new RowDefinition();
                column.MinHeight = 30;
                grid.RowDefinitions.Add(column);
            }
        }

        private void FileLocation_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
