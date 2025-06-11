using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace malshinon1.alerts
{
    internal class Alert
    {
        public int id { get; }
        public int targetId { get; }
        public string alert { get; }

        public Alert(int targetId, string alert)
        {
            this.targetId = targetId;
            this.alert = alert;
        }
    }
}
