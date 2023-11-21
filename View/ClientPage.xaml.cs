using MiniExcelLibs;
using System;
using System.Collections.Generic;
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
        private List<Client> Clients;

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

            double rowCount = Math.Ceiling((double)Clients.Count / grid.ColumnDefinitions.Count);
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
            int clientNum = 0;
            grid.Children.Clear();

            for (int i = 0; i < grid.RowDefinitions.Count && clientNum < Clients.Count(); i++)
            {
                for (int j = 0; j < grid.ColumnDefinitions.Count && clientNum < Clients.Count(); j++)
                {
                    Button btn = new Button();

                    btn.Name = $"client_{Clients[clientNum].ID}";
                    btn.Content = Clients[clientNum].Name;

                    btn.Click += Client_Click;

                    btn.Style = client_0.Style;
                    btn.Width = client_0.Width;
                    btn.Height = client_0.Height;


                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);

                    grid.Children.Add(btn);
                    clientNum++;
                }
            }

        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            Logger.ActivateClient(GetClientID((sender as Button).Name));
        }

        private int GetClientID(string buttonName) => int.Parse(buttonName.Split('_')[1]);

        public string GetClientbyID(int id) => Clients.FirstOrDefault(x => x.ID == id).Name;

        public void AddClient(string clientName)
        {
            Clients.Add(new Client(Clients.Count, clientName));
        }

        public bool DoesClientExists(string name)
        {
            int i = 0;
            while (i < Clients.Count)
            {
                if (Clients[i].Name == name) return true;
                i++;
            }
            return false;
        }

        public void SaveClients()
        {
            var values = new List<Dictionary<string, object>>();
            for (int i = 0; i < Clients.Count; i++)
            {
                values.Add(new Dictionary<string, object> { { "ID", Clients[i].ID }, { "Name", Clients[i].Name } });
            }

            File.Delete("Clients.xlsx");
            MiniExcel.SaveAs("Clients.xlsx", values);
        }
    }
}
