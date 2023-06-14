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
        public static ModelPage modelPage;
        public static OrderPage orderPage;

        public static Frame MainFrame;
        public static int ActiveClientID;
        public static int ActiveOrderID;

        public static int ViewType; // 0 : ByClient | 1 : ByOrder

        public static void LoadLogger(Frame mainFrame)
        {
            MainFrame = mainFrame;

            clientPage = new ClientPage();
            modelPage = new ModelPage();
            orderPage = new OrderPage();

            ViewType = 0;
        }

        #region Activate/Deactivate

        public static void ActivateClient(int activeClientID)
        {
            ActiveClientID = activeClientID;
            MainFrame.Content = orderPage;
        }

        public static void DeactivateClient() => ActiveClientID = -1;

        public static void ActivateOrder(int activeOrderID)
        {
            ActiveOrderID = activeOrderID;
            MainFrame.Content = modelPage;
        }

        public static void DeactivateOrder() => ActiveOrderID = -1;

        #endregion

        public static void SetViewType(int viewType)
        {
            ViewType = viewType;

            if (ViewType == 1)
            {
                MainFrame.Content = orderPage;
            }
        }

    }
}
