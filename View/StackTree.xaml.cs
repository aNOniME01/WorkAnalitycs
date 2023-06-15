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
    /// Interaction logic for StackTree.xaml
    /// </summary>
    public partial class StackTree : Page
    {
        public StackTree()
        {
            InitializeComponent();
        }

        public void ClearTreeStack() => treeStack.Children.Clear();

        public void AddToTreeStack(string content, string name)
        {
            Button button = new Button();
            button.Content = content;
            button.Name = name;
            button.Style = treeButton.Style;

            treeStack.Children.Add(button);
        }
    }
}
