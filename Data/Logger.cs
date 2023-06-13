using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using WorkAnalitycsWPF.View;

namespace WorkAnalitycsWPF.Data
{
    public class Logger
    {
        public static ClientPage clientPage;
        public static Frame MainFrame;
        public static int ActiveClientID;

        public static void LoadLogger(Frame mainFrame)
        {
            MainFrame = mainFrame;
            clientPage = new ClientPage();
        }

        public static void ActivateClient(int activeClientID) => ActiveClientID = activeClientID;

        public static void DeactivateClient() => ActiveClientID = -1;
    }
}
