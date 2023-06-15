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

namespace WorkAnalitycsWPF.View
{
    /// <summary>
    /// Interaction logic for StackTree.xaml
    /// </summary>
    public partial class StackTree : Page
    {
        public StackTree()
        {
            InitializeComponent();
        }

        public void ClearTreeStack()
        {
            treeStack.Children.Clear();
            AddToTreeStack(">", "menu");
        }

        public void AddToTreeStack(string content, string name)
        {
            Button button = new Button();

            button.Content = content;
            button.Name = name;
            button.Click += treeButton_Click;

            button.Style = treeButton.Style;
            

            treeStack.Children.Add(button);
        }

        private void treeButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == "client")
            {
                Logger.DeactivateModel();
                Logger.DeactivateOrder();

                Logger.UpdateView();
            }
            else if (btn.Name == "order")
            {
                Logger.DeactivateModel();

                Logger.UpdateView();

            }
            else if (btn.Name == "model")
            {
                Logger.UpdateView();
            }
            else
            {
                Logger.SetViewType(Logger.ViewType);
            }
        }
    }
}
