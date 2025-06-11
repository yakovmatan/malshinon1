using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malshinon1.dal;
using malshinon1.reports;

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
            if (this.PotentialAgent(reporter.id))
            {
                this.Dal.UpdateStatus(reporter.firstName, reporter.lastName,"potential_agent");
            }
            else if (reporter.type == "target")
            {
                this.Dal.UpdateStatus(firstNameReporter, lastNameReporter,"both");
            }
            this.Dal.UpdateReportCount(reporter.secretCode);// Update the reporter on the number of reports
            string report = this.Helper.EnterReport();// Report request
            (string firstNameOfTarget, string lastNameOfTarget) = Helper.ExtractName(report);
            if (!this.Helper.ExistsInTheSystem(firstNameOfTarget, lastNameOfTarget))
            {
                this.Helper.CreateNewPerson(firstNameOfTarget, lastNameOfTarget, "target");
            }
            var target = this.Dal.GetPersonByName(firstNameOfTarget, lastNameOfTarget);
            if (target.type == "reporter")
            {
                this.Dal.UpdateStatus(firstNameReporter, lastNameReporter,"both");
            }
            this.Dal.UpdateMentionCount(target.secretCode);
            this.Helper.CreateNewReport(firstNameOfTarget,lastNameOfTarget,firstNameReporter,lastNameReporter,report);
        }

        public bool PotentialAgent(int reporterId)
        {
            (int count, double avgLength) = this.Dal.GetReporterStats(reporterId);
            if (count >= 20 ||  avgLength >= 100)
            {
                return true;
            }
            return false;
        }
    }
}
