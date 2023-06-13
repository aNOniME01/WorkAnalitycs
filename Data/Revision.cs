using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAnalytics.Data
{
    internal class Revision
    {
        public int ID { get;  set; }
        public int RevisionCount { get;  set; }
        public string Reason { get;  set; }
        public Revision()
        {
            
        }
        public Revision(int id, int revCount,string reason)
        {
            ID = id;
            RevisionCount = revCount;
            Reason = reason;
        }
    }
}
