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
        public int reporterId { get; }
        public int targetId { get; }
        public string text { get; }
        public string timeStamp { get; }

        public Report(int id, int reporterId, int targetId, string text, string timeStamp)
        {
            this.id = id;
            this.reporterId = reporterId;
            this.targetId = targetId;
            this.text = text;
            this.timeStamp = timeStamp;
        }

        public Report(int reportedId, int targetId, string text)
        {
            this.reporterId = reportedId;
            this.targetId = targetId;
            this.text = text;
        }
    }
}
