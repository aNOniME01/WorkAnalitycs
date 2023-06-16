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
        public static StackTree stackTree;
        public static ViewPage viewPage;

        public static Frame MainFrame;
        public static Frame InfoFrame;
        public static int ActiveClientID;
        public static int ActiveOrderID;
        public static int ActiveModelID;

        public static int ViewType; // 0 : ByClient | 1 : ByOrder | 2 : ByModel

        public static void LoadLogger(Frame mainFrame)
        {
            MainFrame = mainFrame;

            clientPage = new ClientPage();
            modelPage = new ModelPage();
            orderPage = new OrderPage();
            stackTree = new StackTree();
            viewPage = new ViewPage();

            SetViewType(0);

            UpdateStackTree();
        }

        private static void UpdateStackTree()
        {
            stackTree.ClearTreeStack();

            if(ActiveClientID != -1) stackTree.AddToTreeStack(clientPage.GetClientbyID(ActiveClientID) + ">", "client");
            if (ActiveOrderID != -1) stackTree.AddToTreeStack($"order{ActiveOrderID}>","order");
            //stackTree.AddToTreeStack("model",clientPage.GetClientbyID(ActiveOrderID));
        }

        public static void UpdateView()
        {
            if (ActiveModelID != -1) 
            {
                ActivateModel(ActiveModelID);
            }
            else if (ActiveOrderID != -1) 
            {
                ActivateOrder(ActiveOrderID);
            }
            else if (ActiveClientID != -1) 
            {
                ActivateClient(ActiveClientID);
            }
            else
            {
                SetViewType(ViewType);
            }

        }

        #region Activate/Deactivate

        public static void ActivateClient(int activeClientID)
        {
            ActiveClientID = activeClientID;
            MainFrame.Content = orderPage;
            orderPage.UpdateLayout();

            UpdateStackTree();
        }

        public static void DeactivateClient() => ActiveClientID = -1;


        public static void ActivateOrder(int activeOrderID)
        {
            ActiveOrderID = activeOrderID;
            MainFrame.Content = modelPage;
            modelPage.UpdateLayout();

            UpdateStackTree();
        }

        public static void DeactivateOrder() => ActiveOrderID = -1;


        public static void ActivateModel(int activeModelID)
        {
            //ActiveOrderID = activeModelID;
            MainFrame.Content = modelPage;
            modelPage.UpdateLayout();

            UpdateStackTree();
        }

        public static void DeactivateModel() => ActiveModelID = -1;

        #endregion

        public static void SetViewType(int viewType)
        {
            ViewType = viewType;

            if (ViewType == 0)
            {
                MainFrame.Content = clientPage;
            }
            else if( ViewType == 1)
            {
                MainFrame.Content = orderPage;
                orderPage.UpdateLayout();
            }
            else
            {
                MainFrame.Content = modelPage;
                modelPage.UpdateLayout();
            }

            DeactivateClient();
            DeactivateOrder();

            UpdateStackTree();

        }

        public static double GetViewerHeight()
        {
            if (ActiveModelID != -1)
            {
                return viewPage.rowCount;
            }
            return 0;
        }

    }
}
