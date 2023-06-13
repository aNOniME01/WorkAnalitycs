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
using WorkAnalitycsWPF.View;
using WorkAnalytics.Data;

namespace WorkAnalitycsWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static List<Model> Models;
        private static List<Order> Orders;
        private static List<Revision> Revisions;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Models = MiniExcel.Query<Model>("Models.xlsx").ToList();
            Orders = MiniExcel.Query<Order>("Orders.xlsx").ToList();
            Revisions = MiniExcel.Query<Revision>("Revisions.xlsx").ToList();

            ClientPage clientPage = new ClientPage();

            frame.Content = clientPage;
        }



        private void ToTaskbar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

    }
}
