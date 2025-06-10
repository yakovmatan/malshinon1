using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malshinon1.dal;

namespace malshinon1.manager
{
    internal class Manager
    {
        private HelpManeger Helper;

        public Manager(Dal dal)
        {
            Helper = new HelpManeger(dal);
        }

        public void StartUsing()
        {
            (string firstNameReporter, string lastNameReporter) = Helper.EnterName();
            if (!this.Helper.ExistsInTheSystem(firstNameReporter, lastNameReporter))
            {
                this.Helper.CreateNewPerson(firstNameReporter, lastNameReporter,"reporter");
            }
            else
            {
                this.Helper.UpdateStatusForTarget(firstNameReporter, lastNameReporter);
            }

        }
    }
}
