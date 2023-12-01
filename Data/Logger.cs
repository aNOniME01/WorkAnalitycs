using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;
using WorkAnalitycsWPF.View;
using WorkAnalytics.Data;

namespace WorkAnalitycsWPF.Data
{
    public enum ViewType
    {
        ByClient,ByOrder,ByModel
    }

    public class Logger
    {
        public static ClientPage clientPage;
        public static ModelPage modelPage;
        public static OrderPage orderPage;
        public static StackTree stackTree;
        public static ViewPage viewPage;
        public static AddClientPage addClientPage;
        public static AddModelPage addModelPage;

        public static Frame MainFrame;
        public static Frame InfoFrame;

        public static Grid MainGrid;

        public static int ActiveClientID;
        public static int ActiveOrderID;
        public static int ActiveModelID;

        public static ViewType ViewType;

        public static void LoadLogger(Frame mainFrame, Frame infoFrame,Grid grid)
        {
            MainFrame = mainFrame;
            InfoFrame = infoFrame;

            MainGrid = grid;

            clientPage = new ClientPage();
            modelPage = new ModelPage();
            orderPage = new OrderPage();
            stackTree = new StackTree();
            viewPage = new ViewPage();
            addClientPage = new AddClientPage();
            addModelPage = new AddModelPage();
            

            SetViewType(0);

            UpdateStackTree();
        }

        private static void UpdateStackTree()
        {
            stackTree.ClearTreeStack();

            if(ActiveClientID != -1) stackTree.AddToTreeStack(clientPage.GetClientbyID(ActiveClientID) + ">", "client");
            if (ActiveOrderID != -1) stackTree.AddToTreeStack($"order{ActiveOrderID}>","order");
        }

        #region View

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

        private static void AddModelInfoToView()
        {
            Model model = modelPage.GetModelByID(ActiveModelID);

            viewPage.AddLabel("Name", model.Name);
            viewPage.AddLabel("Delivery", Convert.ToString(model.DeliveryDate));
            viewPage.AddLabel("HoursWorked", Convert.ToString(model.WorkHours * 24));
            viewPage.AddLabel("Price", Convert.ToString(model.Price));
            if (model.FileLocation != "" || model.FileLocation != null) viewPage.AddLocationButton(model.FileLocation);
        }

        public static void SetViewType(ViewType viewType)
        {
            ViewType = viewType;

            if (ViewType == ViewType.ByClient)
            {
                MainFrame.Content = clientPage;
            }
            else if (ViewType == ViewType.ByOrder)
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

        public static void UpdateViewerHeight()
        {
            int x = 0;
            if (ActiveModelID != -1)
            {
                x = viewPage.rowCount * 30;
            }
            UpdateViewerHeight(x);
        }

        #endregion

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
            viewPage.ClearViewElements();
            ActiveModelID = activeModelID;
            InfoFrame.Content = viewPage;
            AddModelInfoToView();

            UpdateViewerHeight();
            UpdateStackTree();
        }

        public static void DeactivateModel() => ActiveModelID = -1;

        #endregion

        #region Create

        public static void OpenAddView()
        {
            if (ActiveClientID == -1) MainFrame.Content = addClientPage;
            if (ActiveClientID != -1) ;
            if (ActiveOrderID != -1) MainFrame.Content = addModelPage;
        }
        public static void SaveAddedElement()
        {
            if (ActiveClientID == -1)
            {
                string name = addClientPage.clientName.Text;
                if (!clientPage.DoesClientExists(name)) clientPage.AddClient(name);
                clientPage.UpdateLayout();
            }
            if (ActiveClientID != -1)
            {

            }
            if (ActiveOrderID != -1)
            {
                if (!modelPage.DoesModelExist(ActiveOrderID,addModelPage.name.Text)) modelPage.AddToModels(addModelPage.name.Text,addModelPage.location.Text, double.Parse(addModelPage.price.Text), Convert.ToDateTime(addModelPage.deliveryDate.Text), double.Parse(addModelPage.workhours.Text));
                modelPage.UpdateLayout();
            }
        }


        #endregion

        #region GridOperations

        private static void UpdateViewerHeight(int viewHeight)
        {
            viewPage.UpdateView();

            MainGrid.RowDefinitions.Clear();
            AddRowDefinition(20);
            AddRowDefinition(viewHeight);
            AddRowDefinition(-1);
        }

        private static void AddRowDefinition(double height)
        {
            var firts = new RowDefinition();
            if (height > -1) firts.Height = new GridLength(height);
            MainGrid.RowDefinitions.Add(firts);
        }

        #endregion

        public static void SaveTables()
        {
            clientPage.SaveClients();
        }
    }
}
