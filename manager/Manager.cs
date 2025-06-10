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
        private Dal Dal;

        public Manager(Dal dal)
        {
            Helper = new HelpManeger(dal);
            Dal = dal;
        }

        public void StartUsing()
        {
            (string firstNameReporter, string lastNameReporter) = Helper.EnterName();//  Request a name from the user
            if (!this.Helper.ExistsInTheSystem(firstNameReporter, lastNameReporter))
            {
                this.Helper.CreateNewPerson(firstNameReporter, lastNameReporter,"reporter");
            }
            var reporter = this.Dal.GetPersonByName(firstNameReporter, lastNameReporter);
            if (reporter.type == "target")
            {
                this.Dal.UpdateStatusToBoth(firstNameReporter, lastNameReporter);
            }
            this.Dal.UpdateReportCount(reporter.secretCode);// Update the reporter on the number of reports
            string report = this.Helper.EnterReport();// Report request
            (string firstNameOfTarget, string lastNameOfTarget) = Helper.ExtractName(report);
            if (!this.Helper.ExistsInTheSystem(firstNameOfTarget, lastNameOfTarget))
            {
                this.Helper.CreateNewPerson(firstNameOfTarget, lastNameOfTarget, "target");
            }
            var target = this.Dal.GetPersonByName(firstNameOfTarget, lastNameOfTarget);
            if (target.type == "target")
            {
                this.Dal.UpdateStatusToBoth(firstNameReporter, lastNameReporter);
            }
            this.Dal.UpdateMentionCount(target.secretCode);
            this.Helper.CreateNewReport(firstNameOfTarget,lastNameOfTarget,firstNameReporter,lastNameReporter,report);






        }
    }
}
