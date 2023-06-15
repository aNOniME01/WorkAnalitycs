using MiniExcelLibs;
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
using WorkAnalitycsWPF.Data;
using WorkAnalytics.Data;

namespace WorkAnalitycsWPF.View
{
    /// <summary>
    /// Interaction logic for ModelPage.xaml
    /// </summary>
    public partial class ModelPage : Page
    {
        private List<Model> Models;
        public ModelPage()
        {
            InitializeComponent();
            Models = MiniExcel.Query<Model>("Models.xlsx").ToList();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            grid.ColumnDefinitions.Clear();
            for (int i = 0; i < Math.Floor(ActualWidth / 150); i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            grid.RowDefinitions.Clear();
            for (int i = 0; i < Math.Floor(ActualHeight / 50); i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            int clientNum = 0;
            grid.Children.Clear();

            for (int i = 0; i < grid.RowDefinitions.Count && clientNum < Models.Count(); i++)
            {
                for (int j = 0; j < grid.ColumnDefinitions.Count && clientNum < Models.Count(); j++)
                {
                    Button btn = new Button();

                    btn.Name = $"client_{clientNum}";
                    btn.Content = Models[clientNum].Name;

                    btn.Click += Model_Click;

                    btn.Style = model_0.Style;
                    btn.Width = model_0.Width;
                    btn.Height = model_0.Height;


                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);

                    grid.Children.Add(btn);
                    clientNum++;
                }
            }

            UpdateLayout();
        }

        public void UpdateLayout()
        {
            List<Model> selectedOrders = Models;
            if (Logger.ViewType != 2)
            {
                selectedOrders = Models.Where(x => x.OrderID == Logger.ActiveOrderID).ToList();
            }


            int clientNum = 0;
            grid.Children.Clear();

            for (int i = 0; i < grid.RowDefinitions.Count && clientNum < selectedOrders.Count(); i++)
            {
                for (int j = 0; j < grid.ColumnDefinitions.Count && clientNum < selectedOrders.Count(); j++)
                {
                    Button btn = new Button();

                    btn.Name = $"client_{clientNum}";
                    btn.Content = selectedOrders[clientNum].Name;

                    btn.Click += Model_Click;

                    btn.Style = model_0.Style;
                    btn.Width = model_0.Width;
                    btn.Height = model_0.Height;


                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);

                    grid.Children.Add(btn);
                    clientNum++;
                }
            }
        }

        private void Model_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
