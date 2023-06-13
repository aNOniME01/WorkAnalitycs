using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAnalytics.Data
{
    internal class Order
    {
        public int ID { get;  set; }
        public int ClientID { get;  set; }
        public int GigID { get;  set; }
        public int RevisionID { get;  set; }
        public int DeliveryStatus { get;  set; } // 0 : Active | 1 : Accepted | 2 : Delivered

        public string OrderNumber { get;  set; }

        public double Price { get; private set; }
        public DateTime? ExpectedDeliveryDate { get;  set; }
        public DateTime? DeliveryDate { get;  set; }

        public Order()
        {
            
        }
        public Order(int orderId, int clientId, int gigId, int revisionId, int deliveryStatus, string orderNumber, double price, DateTime? expectedDeliveryDate, DateTime deliveryDate)
        {
            ID = orderId;
            ClientID = clientId;
            GigID = gigId;
            RevisionID = revisionId;
            DeliveryStatus = deliveryStatus;
            OrderNumber = orderNumber;
            Price = price;
            ExpectedDeliveryDate = expectedDeliveryDate;
            DeliveryDate = deliveryDate;
        }
    }
}
