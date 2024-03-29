﻿using MiniExcelLibs;
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

            double rowCount = Math.Ceiling((double)Models.Count / grid.ColumnDefinitions.Count);
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
            List<Model> selectedOrders = Models;
            if (Logger.ViewType != ViewType.ByModel)
            {
                selectedOrders = Models.Where(x => x.OrderID == Logger.ActiveOrderID).ToList();
            }


            int modelNum = 0;
            grid.Children.Clear();

            for (int i = 0; i < grid.RowDefinitions.Count && modelNum < selectedOrders.Count(); i++)
            {
                for (int j = 0; j < grid.ColumnDefinitions.Count && modelNum < selectedOrders.Count(); j++)
                {
                    Button btn = new Button();

                    btn.Name = $"model_{selectedOrders[modelNum].ModelID}";
                    btn.Content = selectedOrders[modelNum].Name;

                    btn.Click += Model_Click;

                    btn.Style = model_0.Style;
                    btn.Width = model_0.Width;
                    btn.Height = model_0.Height;


                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);

                    grid.Children.Add(btn);
                    modelNum++;
                }
            }
        }

        private void Model_Click(object sender, RoutedEventArgs e)
        {
            Logger.ActivateModel(GetModelID((sender as Button).Name));
        }

        public Model GetModelByID(int id) => Models[id];

        private int GetModelID(string buttonName) => int.Parse(buttonName.Split('_')[1]);

        public void AddToModels(string modelName, string? modelLocation, double modelPrice, DateTime? deliveryDate, double workHours)
        {
            Models.Add(new Model(Models.Count,Logger.ActiveOrderID,modelName,modelLocation,modelPrice,deliveryDate,workHours));
        }

        public bool DoesModelExist(int orderID, string modelName)
        {
            foreach (var model in Models.Where(x => x.OrderID == orderID))
            {
                if (model.Name == modelName) return true;
            }
            return false;
        }

        public void SaveModels()
        {
            var values = new List<Dictionary<string, object>>();
            for (int i = 0; i < Models.Count; i++)
            {
                values.Add(new Dictionary<string, object>
                {
                    { "ModelID", Models[i].ModelID },
                    { "OrderID", Models[i].OrderID },
                    { "Name", Models[i].Name },
                    { "FileLocation", TrimNull(Models[i].FileLocation)},
                    { "Price", Models[i].Price },
                    { "DeliveryDate", TrimNull(Models[i].DeliveryDate) },
                    { "WorkHours", TrimNull(Models[i].WorkHours) }
                }) ;
            }

            File.Delete("Models2.xlsx");
            MiniExcel.SaveAs("Models2.xlsx", values);
        }

        private object TrimNull(object? obj) => obj == null ? "" : obj;
    }
}
