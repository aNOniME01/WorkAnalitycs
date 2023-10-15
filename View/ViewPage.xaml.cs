using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private List<UIElement> viewElements= new List<UIElement>();
        public int rowCount = 0;
        private string FileLocation = "";

        public ViewPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateView();
        }

        public void UpdateView()
        {
            double columnCount = ActualWidth > 200 ? Math.Floor(ActualWidth / 200) : 1;

            grid.ColumnDefinitions.Clear();
            for (int i = 0; i < columnCount; i++)
            {
                var column = new ColumnDefinition();
                column.MinWidth = 200;
                grid.ColumnDefinitions.Add(column);
            }

            rowCount = (int)Math.Ceiling(viewElements.Count / columnCount);

            grid.RowDefinitions.Clear();
            for (int i = 0; i < rowCount; i++)
            {
                var row = new RowDefinition();
                row.MinHeight = 30;
                grid.RowDefinitions.Add(row);
            }

            int InfoNum = 0;
            grid.Children.Clear();

            for (int i = 0; i < grid.RowDefinitions.Count && InfoNum < viewElements.Count(); i++)
            {
                for (int j = 0; j < grid.ColumnDefinitions.Count && InfoNum < viewElements.Count(); j++)
                {
                    Grid.SetRow(viewElements[InfoNum], i);
                    Grid.SetColumn(viewElements[InfoNum], j);

                    grid.Children.Add(viewElements[InfoNum]);
                    InfoNum++;
                }
            }
        }

        public void ClearViewElements() => viewElements.Clear();

        public void AddLabel(string name, string value)
        {
            Label lbl = new Label();
            lbl.Content = $"{name}: {value}";
            viewElements.Add(lbl);
        }

        public void AddLocationButton(string value)
        {
            Button btn = new Button();
            btn.Content = "Open";
            btn.Click += FileLocation_Click;
            btn.Width = 30;
            btn.Height = 30;

            FileLocation = value;

            viewElements.Add(btn);

        }

        private void FileLocation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileLocation = "C:\\Users\\dobes\\Downloads";
                Process.Start("explorer.exe", @FileLocation);
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message,"Error",MessageBoxButton.OK);
            }
        }

    }
}
