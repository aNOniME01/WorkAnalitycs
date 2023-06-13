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
    /// Interaction logic for ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        private static List<Client> Clients;

        public ClientPage()
        {
            InitializeComponent();
            
            Clients = MiniExcel.Query<Client>("Clients.xlsx").ToList();
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

            for (int i = 0; i < grid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < grid.ColumnDefinitions.Count; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        client_0.Content = Clients[clientNum];
                        clientNum++;

                        j++;
                    }

                    Button btn = new Button();

                    btn.Name = $"client_{clientNum}";
                    btn.Content = Clients[clientNum].Name;

                    btn.Click += Client_Click;

                    btn.Style = client_0.Style;
                    btn.Width = client_0.Width;
                    btn.Height = client_0.Height;
                    
                }
            }
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            Logger.ActivateClient(GetClientID((sender as Button).Name));
        }

        private int GetClientID(string buttonName) => int.Parse(buttonName.Split('_')[1]);
    }
}
