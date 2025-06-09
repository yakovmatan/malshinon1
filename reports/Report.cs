using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon1.reports
{
    internal class Report
    {
        public int id { get; }
        public int reoprterId { get; }
        public int targetId { get; }
        public string text { get; }
        public string timeStamp { get; }

        public Report(int id, int reoprterId, int targetId, string text, string timeStamp)
        {
            this.id = id;
            this.reoprterId = reoprterId;
            this.targetId = targetId;
            this.text = text;
            this.timeStamp = timeStamp;
        }
    }
}
