using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon1.alerts
{
    internal class DangerTarget
    {
        public string firstName { get; }
        public string lastName { get; }
        public string alert {  get; }

        public DangerTarget(string firstName, string lastName, string alert)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.alert = alert;
        }
    }
}
