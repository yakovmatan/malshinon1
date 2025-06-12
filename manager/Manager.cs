using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malshinon1.dal;
using malshinon1.logger;
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
            if (reporter.type == "target")
            {
                this.Dal.UpdateStatus(firstNameReporter, lastNameReporter,"both");
                Logger.Info($"{firstNameReporter} {lastNameReporter} was update from target to both");
            }
            this.Dal.UpdateReportCount(reporter.secretCode);// Update the reporter on the number of reports
            Logger.Info($"report number of {firstNameReporter} {lastNameReporter} increased by 1");
            string report = this.Helper.EnterReport();// Report request
            (string firstNameOfTarget, string lastNameOfTarget) = Helper.ExtractName(report);
            if (!this.Helper.ExistsInTheSystem(firstNameOfTarget, lastNameOfTarget))
            {
                this.Helper.CreateNewPerson(firstNameOfTarget, lastNameOfTarget, "target");
            }
            var target = this.Dal.GetPersonByName(firstNameOfTarget, lastNameOfTarget);
            if (target.type == "reporter")
            {
                this.Dal.UpdateStatus(firstNameOfTarget, lastNameOfTarget,"both");
                Logger.Info($"{firstNameOfTarget} {lastNameOfTarget} was update from reporter to both");
            }
            this.Dal.UpdateMentionCount(target.secretCode);
            Logger.Info($"Mention number of {firstNameOfTarget} {lastNameOfTarget} increased by 1");
            this.Helper.CreateNewReport(firstNameOfTarget,lastNameOfTarget,firstNameReporter,lastNameReporter,report);
            if (this.Helper.PotentialAgent(reporter.id))
            {
                this.Dal.UpdateStatus(reporter.firstName, reporter.lastName, "potential_agent");
                Logger.Info($"{firstNameReporter} {lastNameReporter} was update from target to potential agent");
            }
            if (this.Helper.DangerTarget(target.secretCode))
            {
                this.Helper.createAlert(target.id);
            }
        } 

        public void PrintAllDangerTargets()
        {
            var dangerTarget = this.Dal.GetAllDangerTarget();
            if (dangerTarget.Count > 0)
            {
                Console.WriteLine("\n=========== DANGER TARGETS =============\n");
                foreach (var target in dangerTarget)
                {
                    Console.WriteLine($"🎯 Name  : {target.firstName} {target.lastName}");
                    Console.WriteLine($"🚨 Alert : {target.alert}");
                    Console.WriteLine("----------------------------------------\n");
                }
            }
            else
            {
                Console.WriteLine("\n⚠️ No danger targets found.");
            }
        }

        public void PrintAllPotentialAgent()
        {
            var potentialAgent = this.Dal.GetAllPotentialAgent();
            if (potentialAgent.Count > 0)
            {
                Console.WriteLine("\n=========== POTENTIAL AGENTS ===========\n");
                foreach (var agent in potentialAgent)
                {
                    Console.WriteLine($"👤 Name        : {agent.firstName} {agent.lastName}");
                    Console.WriteLine($"🆔 Secret Code : {agent.secretCode}");
                    Console.WriteLine($"📄 Reports     : {agent.numReports}");
                    Console.WriteLine("----------------------------------------\n");
                }
            }
            else
            {
                Console.WriteLine("\n⚠️ No potential agant found. \n");
            }
        }
        
        
    }
}
