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
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private static List<Order> Orders;

        public OrderPage()
        {
            InitializeComponent();
            Orders = MiniExcel.Query<Order>("Orders.xlsx").ToList();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            grid.ColumnDefinitions.Clear();
            for (int i = 0; i < Math.Floor(ActualWidth / 150); i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            double rowCount = Math.Ceiling((double)Orders.Count / grid.ColumnDefinitions.Count);
            Height = rowCount * 50;

            grid.RowDefinitions.Clear();
            for (int i = 0; i < rowCount; i++)
            {
                var row = new RowDefinition();
                row.Height = new GridLength(50);
                grid.RowDefinitions.Add(row);
            }

            UpdateLayout();
        }

        public void UpdateLayout()
        {
            List<Order> selectedOrders = Orders;
            if (Logger.ViewType == 0)
            {
                selectedOrders = Orders.Where(x => x.ClientID == Logger.ActiveClientID).ToList();
            }


            int orderNum = 0;
            grid.Children.Clear();

            for (int i = 0; i < grid.RowDefinitions.Count && orderNum < selectedOrders.Count(); i++)
            {
                for (int j = 0; j < grid.ColumnDefinitions.Count && orderNum < selectedOrders.Count(); j++)
                {
                    Button btn = new Button();

                    btn.Name = $"order_{selectedOrders[orderNum].ID}";
                    btn.Content = $"order{selectedOrders[orderNum].ID}\n{selectedOrders[orderNum].DeliveryDate}";

                    btn.Click += Order_Click;

                    btn.Style = order_0.Style;
                    btn.Width = order_0.Width;
                    btn.Height = order_0.Height;


                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);

                    grid.Children.Add(btn);
                    orderNum++;
                }
            }
        }


        private void Order_Click(object sender, RoutedEventArgs e)
        {
            Logger.ActivateOrder(GetOrderID((sender as Button).Name));
        }

        private int GetOrderID(string buttonName) => int.Parse(buttonName.Split('_')[1]);
    }
}
