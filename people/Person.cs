using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon1.people
{
    internal class Person
    {
        public int id { get; }
        public string firstName { get; }
        public string lastName { get; }
        public string secretCode { get; }
        public string type { get; }
        public int numReports { get; }
        public int numMentions { get; }

        public Person(int id, string firstName, string lastName, string secretCode, string type, int numReports, int numMentions)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.secretCode = secretCode;
            this.type = type;
            this.numReports = numReports;
            this.numMentions = numMentions;
        }
    }
}
