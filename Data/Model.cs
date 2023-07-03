using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAnalytics.Data
{
    public class Model
    {
        public int ModelID { get;  set; }
        public int OrderID { get;  set; }

        public string Name { get;  set; }
        public string? FileLocation { get;  set; }

        public double Price { get;  set; }

        public DateTime? DeliveryDate { get;  set; }
        public double? WorkHours { get;  set; }

        public Model()
        {
            
        }

        public Model(int modelId, int orderId, string name, string? fileLoc, double price,DateTime? deliveryDate,double? workHours)
        {
            ModelID = modelId;
            OrderID = orderId;
            Name = name;
            FileLocation = fileLoc;
            DeliveryDate = deliveryDate;
            WorkHours = workHours;
            ModelID = modelId;
        }
    }
}
