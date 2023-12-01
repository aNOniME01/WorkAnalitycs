using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
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
using System.Xml.Linq;
using WorkAnalitycsWPF.Data;
using WorkAnalitycsWPF.View;
using WorkAnalytics.Data;

namespace WorkAnalitycsWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static List<Revision> Revisions;
        private Options options;
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Logger.LoadLogger(frame,infoFrame,grid);

            options = new Options(FileOperatinos.GetOptionsPath());
            LoadOptions();

            Revisions = MiniExcel.Query<Revision>("Revisions.xlsx").ToList();

            stackTreeFrame.Content = Logger.stackTree;

            Logger.UpdateViewerHeight();
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

        private void Windowed_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.ResizeMode = ResizeMode.CanResize;
                this.WindowState = WindowState.Normal;
                this.Topmost = false;
            }
            else
            {
                this.ResizeMode = ResizeMode.NoResize;
                this.WindowState = WindowState.Maximized;
                this.Topmost = true;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (IsLoaded) Logger.UpdateViewerHeight();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (frame.Content == Logger.addClientPage || /* add order */ frame.Content == Logger.addModelPage)
            {
                AddButton.Content = "Add";

                frame.Content = Logger.clientPage;

                Logger.SaveAddedElement();
            }
            else
            {
                AddButton.Content = "Save";

                Logger.OpenAddView();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) => options.ChangeModelDir((sender as TextBox).Text);

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            options.Save();
            Logger.SaveTables();
        }

        #endregion

        #region MenuItems

        private void ViewBy_Click(object sender, RoutedEventArgs e)
        {
            MenuItem hlpr = sender as MenuItem;
            if (hlpr.Header.ToString() == "ClientBased") Logger.SetViewType(ViewType.ByClient);
            else if (hlpr.Header.ToString() == "OrderBased") Logger.SetViewType(ViewType.ByOrder);
            else Logger.SetViewType(ViewType.ByModel);
        }

        private void LoadOptions()
        {
            modelLocText.Text = options.ModelDir;
        }

        #endregion


    }
}
