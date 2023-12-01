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
    /// Interaction logic for AddModelPage.xaml
    /// </summary>
    public partial class AddModelPage : Page
    {
        public AddModelPage()
        {
            InitializeComponent();
        }

        private void RestrictToDigigts(object sender, TextChangedEventArgs e)
        {
            string originalText = ((TextBox)sender).Text.Trim();
            if (originalText.Length > 0)
            {
                int inputOffset = e.Changes.ElementAt(0).Offset;
                if (inputOffset < originalText.Length)
                {
                    char inputChar = originalText.ElementAt(inputOffset);
                    if (!char.IsDigit(inputChar))
                    {
                        ((TextBox)sender).Text = originalText.Remove(inputOffset, 1);
                    }
                }
            }
        }
    }
}
