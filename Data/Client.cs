using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAnalytics.Data
{
    internal class Client
    {

        public int ID { get;  set; }
        public string? Name { get; set; }

        public Client()
        {
            
        }

        public Client(string name, int id)
        {
            Name = name;
            ID = id;
        }
    }
}
